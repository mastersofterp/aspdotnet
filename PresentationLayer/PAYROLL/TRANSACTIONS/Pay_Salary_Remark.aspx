<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Salary_Remark.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Salary_Remark" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SALARY REMARK</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Enter Salary Remark</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Month & Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMonthYear" AppendDataBoundItems="true" runat="server" TabIndex="1" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Enter Month Year" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" ControlToValidate="ddlMonthYear"
                                                Display="None" SetFocusOnError="true" ErrorMessage="Please Select Month & year" ValidationGroup="PayRoll"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server"
                                                CssClass="form-control" ToolTip="Select College" TabIndex="2" AutoPostBack="True" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvddlCollege" runat="server"
                                                ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="show"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trMonthRemark" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Month Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtMonthRemark" runat="server" TextMode="MultiLine" CssClass="form-control" ToolTip="Enter Month Remark" TabIndex="3"></asp:TextBox>                                           
                                              <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvmonthremark" runat="server"
                                                ControlToValidate="txtMonthRemark"
                                                Display="None" ErrorMessage="Enter Month Remark" ValidationGroup="show"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                            
                                             </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                             <asp:Button ID="butSubmitCommonRemark" runat="server" Text="Submit Month Remark"
                                                    ValidationGroup="PayRoll" OnClick="butSubmitCommonRemark_Click" CssClass="btn btn-primary" ToolTip="Click To submit" TabIndex="4" />
                                                <asp:ValidationSummary ID="vsSubmitCommomReport" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="PayRoll" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                               <%-- <label>Staff</label>--%>
                                                 <label>Scheme/Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select Scheme/Staff" AutoPostBack="True" TabIndex="5"
                                                OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvStaff" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Please Select Scheme/Staff" ValidationGroup="show"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="ShowEmployeesForCommonRemark" runat="server" ValidationGroup="show"
                                        Text="Show Employees For CommonRemark(or)Remark" ToolTip="Click to Show" OnClick="ShowEmployeesForCommonRemark_Click" CssClass="btn btn-primary" TabIndex="6" />
                                    <asp:ValidationSummary ID="vsShowEmployees" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="show" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlSalaryRemark" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvSalaryRemark" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" CssClass="text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Salary Remark</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Common Remark
                                                        </th>
                                                        <th>Remark
                                                        </th>
                                                    </tr>
                                                    <thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("Name")%>
                                                    <asp:HiddenField ID="hididno" Value='<%#Eval("IDNO")%>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlvCommonRemark" runat="server" Text='<%#Eval("COMMREM")%>' TextMode="MultiLine"
                                                        CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtlvRemark" runat="server" Text='<%#Eval("remark")%>' TextMode="MultiLine"
                                                        CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                         
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="butSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="PayRoll"
                                        OnClick="butSubmit_Click" TabIndex="7"/>
                                    <asp:Button ID="butCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ValidationGroup="PayRoll"
                                        OnClick="butCancel_Click" TabIndex="8"/>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
