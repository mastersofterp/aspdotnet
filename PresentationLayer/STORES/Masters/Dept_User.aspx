<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Dept_User.aspx.cs" Inherits="Stores_Masters_Dept_User" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DEPARTMENT USER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add/Edit DEPARTMENT USER</h5>
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                   <sup>*</sup>
                                                <label>Department Name</label>
                                             
                                            </div>

                                            <asp:DropDownList ID="ddlDepartment" CssClass="form-control" data-select2-enable="true" TabIndex="1" ToolTip="Select Department" AppendDataBoundItems="true"
                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="ddlDepartment"
                                                Display="None" ErrorMessage="Please Select Department Name" ValidationGroup="store"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                 <sup>*</sup>
                                                <label>User Name</label>
                                               
                                            </div>
                                            <asp:DropDownList ID="ddlUser" AppendDataBoundItems="true" TabIndex="2" ToolTip="Select User" data-select2-enable="true" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlUser" runat="server" ControlToValidate="ddlUser"
                                                Display="None" ErrorMessage="Please Select User Name" ValidationGroup="store"
                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Approval Level</label>
                                                
                                            </div>
                                            <asp:DropDownList ID="ddlApprovalLevel" AppendDataBoundItems="true" runat="server"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="3" ToolTip="Select Approval Level">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlApprovalLevel" runat="server" ControlToValidate="ddlApprovalLevel"
                                                Display="None" ErrorMessage="Please Select Approval Level" ValidationGroup="store"
                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSelfApprove" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Is Self Approval?</label>
                                                <sup></sup>
                                                <br />
                                                <asp:RadioButtonList ID="rdoBtnListIsApproval" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="NO" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                        </div>
                                    </div>

                                </asp:Panel>

                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butSubmit" ValidationGroup="store" Text="Submit" runat="server" CssClass="btn btn-primary"
                                    OnClick="butSubmit_Click" TabIndex="6" ToolTip="Click To Submit" />
                                <asp:Button ID="btnshowrpt" Text="Report" runat="server" TabIndex="8" ToolTip="Click To Show Report" CssClass="btn btn-info" OnClick="btnshowrpt_Click" />
                                <asp:Button ID="butCancel" Text="Cancel" runat="server" TabIndex="7" ToolTip="Click To Reset" CssClass="btn btn-warning" OnClick="butCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="store"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                       

                            <asp:Panel ID="pnlDeptUser" runat="server">
                            <div class="col-12">
                                <asp:ListView ID="lvDeptUser" runat="server">
                                    <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>

                                        <div class="sub-heading">
                                            <h5>Department Users</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Department Name
                                                    </th>
                                                    <th>User
                                                    </th>
                                                    <th>User Name
                                                    </th>
                                                    <th>Approval Level
                                                    </th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>

                                        <%--                                        <div class="listview-container">
                                            <div id="Div1" class="">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="Table1">
                                                    
                                                </table>
                                            </div>
                                        </div>--%>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DUNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                            </td>
                                            <td>
                                                <%# Eval("MDNAME")%>
                                            </td>

                                            <td>
                                                <%# Eval("UA_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("APLT")%>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlDepartment" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>
