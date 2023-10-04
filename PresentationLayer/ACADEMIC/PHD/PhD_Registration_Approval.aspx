<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PhD_Registration_Approval.aspx.cs" Inherits="ACADEMIC_PHD_PhD_Registration_Approval" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div>
        <asp:UpdateProgress ID="UpdPhd" runat="server"
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
        <asp:UpdatePanel ID="updSection" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                      <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="1" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" >
                                           <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Admission Batch"
                                                        ControlToValidate="ddlAdmBatch" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                         <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School Applied for</label>
                                        </div>
                                          <asp:DropDownList ID="ddlSchool" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" ToolTip="Please select school applied for">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>                                               
                                                    </asp:DropDownList>
                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Please select school applied for."
                                                  ControlToValidate="ddlSchool" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                                </div>

                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                          <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>PhD/Programme Applied For</label>
                                        </div>
                                           <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ErrorMessage="Please Select PhD/MPhil Programme Applied For"
                                                    ControlToValidate="ddlDepartment" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />--%>
                                         </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                          <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Mode of pursuing PhD</label>
                                               </div>
                                         <asp:DropDownList ID="ddlPhDMode" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" TabIndex="3" ToolTip="Please Select Mode of pursuing PhD" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPhDMode_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Full Time</asp:ListItem>
                                                        <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator44" InitialValue="0" runat="server" ErrorMessage="Please Select Mode of pursuing PhD"
                                                           ControlToValidate="ddlPhDMode" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />            --%>                            </div>
                                    </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-info" TabIndex="4" OnClick="btnShow_Click" ValidationGroup="regsubmit" ToolTip="Show Student Registration list" />
                                     <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    class="btn btn-warning" TabIndex="5" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="regsubmit" Style="text-align: center" />
                                    </div>
                                <asp:UpdatePanel ID="updPhdlistview" runat="server">
                                    <ContentTemplate>
                                <div class="col-12">
                                            <asp:Panel ID="Panel3" runat="server" Visible="false">
                                                <asp:ListView ID="lvPhd" runat="server" EnableModelValidation="True">
                                                    <EmptyDataTemplate>
                                                        <div>
                                                            -- No Student Record Found --
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                             <h5>PhD Student List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        SR.NO.
                                                                    </th>
                                                                    <th>
                                                                        Application Id
                                                                    </th>
                                                                    <th>
                                                                        Student Name 
                                                                    </th>
                                                                    <th>
                                                                        View 
                                                                    </th>
                                                                    <th>
                                                                        Approval
                                                                    </th>
                                                                    <th>
                                                                        Status
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
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("USERNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("STUDENT_NAME")%>
                                                            </td>
                                                            <td>
                                                            <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandArgument='<%# Eval("USERNO") %>' CssClass="btn btn-info"
                                                                OnClick="lnkView_Click"></asp:LinkButton>
                                                                </td>
                                                            <td>
                                                               <asp:LinkButton ID="lnkApproved" runat="server" Text="Approve" CommandArgument='<%# Eval("USERNO") %>' CssClass="btn btn-info" Enabled='<%#Eval("APPROVED_STATUS").ToString().Equals("Pending")?true :false %>' OnClick="lnkApproved_Click"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                 <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("APPROVED_STATUS")%>' ForeColor='<%#Eval("APPROVED_STATUS").ToString().Equals("Pending")?System.Drawing.Color.Red : System.Drawing.Color.Green %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                         </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvPhd" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            
            </asp:UpdatePanel>
    
</asp:Content>

