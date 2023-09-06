<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Userwise_AccessLink_Report.aspx.cs" Inherits="ACADEMIC_Userwise_AccessLink_Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
     <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPassedOut"
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

    <asp:UpdatePanel ID="updPassedOut" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">ROOM ALLOTMENT DEPARTMENT WISE</h3>--%>
                            <h3 class="box-title"><b>User Rights Report</b></h3>
                                <%--<asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <label><span style="color:red"></span>User Type</label>
                                            <%--<label>Department</label>--%>
                                          <%--  <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" AppendDataBoundItems="true"  data-select2-enable="true" AutoPostBack="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ControlToValidate="ddlUserType"
                                            Display="None" ErrorMessage="Please Select User Type" ValidationGroup="submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    
                                    <asp:HiddenField ID="hdnCount" runat="server" Value="0" />
                                    <asp:Panel ID="pnlStudentList" runat="server" Visible="false">
                                        <asp:ListView ID="lvStudentList" runat="server" >
                                            <LayoutTemplate>
                                                <div>
                                                   <%-- <h3>Pass Out Student List for Allotment</h3>--%>
                                                </div>
                                                <table class="table table-hover table-bordered table-striped" id="divsessionlist" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <%--<th style="text-align:center;">Edit
                                                    </th>--%>
                                                            <th>
                                                                <asp:CheckBox ID="chkHead" runat="server" onclick="selectAll(this);"/>
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Regno
                                                            </th>
                                                            <th>Degree
                                                            </th>
                                                            <th>Branch
                                                            </th>
                                                            <th>Mobile
                                                            </th>
                                                            <th>Email
                                                            </th>
                                                            <th>
                                                                Passout Session
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%--<td style="text-align:center;">
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                    CommandArgument='<%# Eval("SESSIONNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                      TabIndex="12" />
                                            </td>--%>
                                                    <td>
                                                        <asp:CheckBox ID="chkstud" runat="server" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                        <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("IDNO")%>' ToolTip='<%# Eval("REGNO")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEGREE")%>
                                                        <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENO")%>' ToolTip='<%# Eval("COLLEGE_ID")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BRANCH")%>
                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCHNO")%>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                      <%# Eval("MOBILENO")%>

                                                    </td>
                                                    <td>
                                                          <%# Eval("EMAILID")%>
                                                    </td>
                                                     <td>
                                                          <%# Eval("PASS_SESSION")%>
                                                         <asp:Label ID="lblSession" runat="server" Text='<%# Eval("SESSIONNO")%>' Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                <div class="col-12 btn-footer">
                                    
                                           <asp:Button ID="btnExcelreport" runat="server" Text="User Rights Report(Excel)" CssClass="btn btn-info" ValidationGroup="submit" TabIndex="2" OnClick="btnExcelreport_Click"/>
                                          <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="3" OnClick="btncancel_Click"  />
                                            
                                   
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                  

                             </div>
                            </div>
                        </div>
                   </div>
              </div>
         </div>
  </div>
</ContentTemplate>
        <Triggers >
            <asp:PostBackTrigger ControlID="btnExcelreport" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>