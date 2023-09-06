<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmailConfiguration.aspx.cs" Inherits="ACADEMIC_EmailConfiguration" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   

    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Email Configuration</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                        <%--    <label>Module Name</label>--%>
                                            <asp:Label ID="lblDYddlModule" runat="server" Font-Bold="true">Module Name</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="True" TabIndex="1" 
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="True" ToolTip="Please Select Module" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlModule" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Module Name" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                          <%--  <label>Page/Link Name</label>--%>
                                            <asp:Label ID="lblDYddlpagelink" runat="server" Font-Bold="true">Page/Link Name</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlpagelink" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlpagelink_SelectedIndexChanged"
                                            AppendDataBoundItems="True" ToolTip="Please Select Page Link">
                                         
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlpagelink" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Page Link" InitialValue="0"  ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Email</label>--%>
                                            <asp:Label ID="lblDYEmail" runat="server" Font-Bold="true">Email</asp:Label>
                                        </div>
                                    <asp:TextBox ID="txtemail" runat="server"  TabIndex="2" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvemail" runat="server" ControlToValidate="txtemail" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Email"  ValidationGroup="show"></asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtemail"
                                                ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" SetFocusOnError="True" Display="None" ValidationGroup="show" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Password</label>--%>
                                            <asp:Label ID="lbltxtpassword" runat="server" Font-Bold="true">Password</asp:Label>
                                        </div>
                                         <asp:TextBox ID="txtpassword" runat="server" type="password" TabIndex="2" CssClass="form-control" MaxLength="16" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="txtpassword" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Password"  ValidationGroup="show"></asp:RequiredFieldValidator>
                                       
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Password must be 8-10 characters long with at least one numeric,one upper case character and one special character." forecolor="Red"  
                                         ValidationExpression="^.*(?=.{8,10})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"  
                                            ControlToValidate="txtpassword" SetFocusOnError="true"
                                            Display="None"   ValidationGroup="show" ></asp:RegularExpressionValidator>  
                                       <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Password"  
                                         ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"  
                                            ControlToValidate="txtpassword" SetFocusOnError="true"
                                            Display="None"   ValidationGroup="show" ></asp:RegularExpressionValidator>  --%>
                                </td>  
                                 </div> 

                                  
                                   
                                  
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                               
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"  TabIndex="8"
                                    ValidationGroup="show" CssClass="btn btn-primary" OnClick="btnSubmit_Click"/>

                            
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"  TabIndex="11"
                                    ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            </div>


                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="show" DisplayMode="List" />


                                   <div class="col-12" >                                   
                                    <asp:ListView ID="lvStudents" runat="server">

                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                             <h5></h5>
                                           </div>
                                             <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="lstTable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                       
                                                        <th>
                                                           Action
                                                        </th>
                                                        <th>Module Name
                                                        </th>
                                                        <th>Page Link Name
                                                            
                                                        </th>
                                                        <th>
                                                           Email
                                                        </th>
                                                     <%--   <th>Password
                                                        
                                                        </th>--%>
                                                        
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
                                               <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("EC_NO")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click"/>
                                                </td>
                                                <td>
                                                  <%# Eval("Module Name")%>
                                                </td>
                                                  <td>
                                                   <%# Eval("Page Link")%>
                                                   </td>
                                                <td>
                                               <%# Eval("EMAIL")%>
                                                </td>
                                                <%--<td>
                                                     <asp:Label ID="lblpassword" runat="server" Text='<%# Eval("PASSWORD") %>' type="password" ></asp:Label>
                                                  <%-- <%# Eval("PASSWORD")%>--%>
                                                <%--</td>--%>
                                            
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                           
                        </div>
                    </div>
                </div>
                 <div id="divMsg" runat="server">
                            </div>
            </div>
        </ContentTemplate>
       <%-- <Triggers>
            <asp:PostBackTrigger  ControlID="btnSubmit" />
          
        </Triggers>--%>
    </asp:UpdatePanel>
    </asp:Content>