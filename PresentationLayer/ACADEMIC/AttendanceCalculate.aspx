<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" ViewStateEncryptionMode="Always" EnableViewStateMac="true" AutoEventWireup="true" CodeFile="AttendanceCalculate.aspx.cs" Inherits="ACADEMIC_AttendanceCalculate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div5" runat="server"></div>
                <%--  PageHeading--%>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <%--PageBody--%>
                <div class="box-body">
                    <%--Form --%>
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>
                                        <asp:Label ID="Label7" runat="server">Status Name</asp:Label></label>
                                </div>
                                <asp:TextBox ID="txtStatusName" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ReqFV1" runat="server" ControlToValidate="txtStatusName" Display="None" ErrorMessage="Please Enter Status Name " InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularEXV1" runat="server" ErrorMessage="Only alphabates and numbers are allowed" ValidationExpression="^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$" Display="None" InitialValue="" ControlToValidate="txtStatusName"></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>
                                        <asp:Label ID="Label1" runat="server">Is Consider for attendance calculation </asp:Label></label>
                                </div>
                                <asp:CheckBox ID="chkcal" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
        
                <div class="col-12 btn-footer">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CausesValidation="true" CssClass="btn btn-primary" ToolTip="Calculate" />
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false" ToolTip="Cancel" />
                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" runat="server" ForeColor="red" ShowSummary="False" />
                </div>

                <div class="col-12">
                    <asp:Panel ID="pnlDegreeType" runat="server">
                        <asp:ListView ID="lview" runat="server">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Attendance Status List</h5>
                                </div>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divlistType">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th style="text-align: center; width: 2%">Edit</th>
                                            <th style="text-align: center; width: 10%">Status</th>
   
                                            <th style="text-align: center; width: 5%">IS_CALCULATE</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                               <ItemTemplate>
                                   <asp:UpdatePanel runat="server" ID="updDegreeType">
                                       <ContentTemplate>
                                           <tr>
                                               <td style="text-align: center; width: 2%"">
                                                   <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CommandArgument='<%# Eval("STATUS_NO")%>' CausesValidation="false" ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" TabIndex="10" />
                                               </td>

                                               <td style="text-align: center; width: 10%">
                                                   <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                               </td>

                                                    <%--<td style="text-align: center; width: 12%">
                                                            <asp:Label ID="lblcode" runat="server" Text='<%# Eval("CLGCODE") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: center; width: 8%">
                                                            <asp:Label ID="lblorid" runat="server" Text='<%# Eval("ORID")%>'></asp:Label>
                                                        </td>--%>

                                               <td style="text-align: center; width: 5%">
                                                   <asp:Label ID="lblcal" runat="server" Text='<%# Eval("CALCULATE")%>'></asp:Label>
                                               </td>
                                       </ContentTemplate>
                                       <Triggers>
                                           <%--<asp:PostBackTrigger ControlID="btnEditDegType" />--%>
                                       </Triggers>
                                   </asp:UpdatePanel>
                               </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>
            </div>
      
   
                </div>
            </div>
      
</asp:Content>
