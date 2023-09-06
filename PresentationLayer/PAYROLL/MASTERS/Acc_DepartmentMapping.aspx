<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_DepartmentMapping.aspx.cs" Inherits="PAYROLL_MASTERS_Acc_DepartmentMapping_aspx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="UPDLedger" runat="server">
      <ContentTemplate>
         <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAYROLL ACADEMIC DEPARTMENT MAPPING</h3>
                        </div>
                        <div class="box-body">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            <asp:Panel ID="pnlPfMaster" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Department Mapping</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Payroll Department</label>
                                            </div>
                                             <asp:DropDownList ID="ddlPayrollDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true"
                                             TabIndex="1" ValidationGroup="submit" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                             </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvFAGroup" runat="server" ControlToValidate="ddlPayrollDept"
                                            Display="None" ErrorMessage="Please Select Payroll Department" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>                                        
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                           <div class="label-dynamic">
                                               <sup>* </sup>
                                              <label>Academic Department</label>
                                           </div>
                                           <asp:DropDownList ID="ddlAcadDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true"
                                          TabIndex="2" ValidationGroup="submit"  CssClass="form-control">
                                          <asp:ListItem Value="0">Please Select</asp:ListItem>
                                           </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAcadDept"
                                           Display="None" ErrorMessage="Please Select Academic Department" InitialValue="0"
                                            SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>                       
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-6 btn-footer">
                              <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="3" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnSubmit_Click"  />
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4" CssClass="btn btn-warning" OnClick="btnCancel_Click"  />
                             <asp:ValidationSummary ID="accdept" runat="server" DisplayMode="List"
                             ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                            </div>
                            <div class="col-12">
                                  <asp:Panel ID="pnlNewBills" runat="server" >
                                    <div class="col-sm-12 form-group">
                                       <asp:ListView ID="lvDept" runat="server">
                                       <LayoutTemplate>
                                        <%-- <div id="lgv1">--%>
                                         <div class="sub-heading">
                                         <h5>Department Mapping List</h5>
                                         </div>
                                            <table class="table table-striped table-bordered nowrap display">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                 <th id="thdelete" runat="server">Edit</th>
                                                   <th>Payroll Department</th>
                                                    <th>Academic Department</th>
                                                    </tr>
                                                 </thead>
                                                 <tbody>
                                                       <tr id="itemPlaceholder" runat="server" />
                                                       </tbody>
                                                       </table>
                                                     <%--  </div>--%>
                                                      </LayoutTemplate>
                                                        <ItemTemplate>
                                                     <tr>
                                                       <td id="tdelte" runat="server">
                                                      <asp:ImageButton ID="btnEdit" runat="server" TabIndex="6" Text="Select"
                                                         CommandArgument='<%# Eval("DEPTNO") %>' ToolTip="Edit Record" OnClick="btnEdit_Click"
                                                        ImageUrl="~/Images/Edit.png" Width="15" Height="15" />
                                                        </td>
                                                         <td><%#Eval("PAYROLL_DEPTNAME") %></td>
                                                         <td>
                                                         <%#Eval("ACAD_DEPTNAME") %>                                                                    
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

