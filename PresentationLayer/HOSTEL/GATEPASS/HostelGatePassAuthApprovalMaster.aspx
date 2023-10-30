<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelGatePassAuthApprovalMaster.aspx.cs" Inherits="HOSTEL_GATEPASS_HostelGatePassAuthApprovalMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <style>
        #ctl00_ContentPlaceHolder1_pnlAAPaList .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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

    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblStuType" runat="server" Font-Bold="true">Student Type</asp:Label>
                                                <%--  <label>College</label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlStuType" runat="server" TabIndex="1" ToolTip="Please Select Student Type." AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvfStuType" runat="server" ControlToValidate="ddlStuType"
                                                Display="None" ErrorMessage="Please Select Student Type." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br /> 
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblCollege" runat="server" Font-Bold="true">College Name</asp:Label>
                                                <%--  <label>College</label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" ToolTip="Please Select College." AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvfCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br />
                                             <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblApprover1" runat="server" Font-Bold="true" ></asp:Label>
                                                <%-- <label>Approval 1 </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlAA1" runat="server" ToolTip="Please Select Approval_1" CssClass="form-control"  AppendDataBoundItems="true" 
                                                AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlAA1_SelectedIndexChanged" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <br /><br /><br />
                                             <div class="label-dynamic">
                                                <asp:Label ID="lblApprover4" runat="server" Font-Bold="true"></asp:Label>
                                                <%--<label>Approval 3 </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlAA4" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="6" ToolTip="Please Select Approval 4"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblAuthority" runat="server" Font-Bold="true">Authority</asp:Label>
                                                <%-- <label>Authority Approval Type </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlApp" runat="server" TabIndex="2"
                                                ToolTip="Please Select  Authority Approval Type" AppendDataBoundItems="true"
                                                AutoPostBack="true" 
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlApp_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlApp"
                                                Display="None" ErrorMessage="Please Select Authority Approval Type." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br />
                                            <div class="label-dynamic">
                                                <%-- <sup>* </sup>--%>
                                                <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true">Department</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddldepartment" runat="server" TabIndex="4" ToolTip="Please Select Approval_1" AppendDataBoundItems="true" CssClass="form-control"
                                                AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldepartment"
                                                Display="None" ErrorMessage="Please select  Department." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>--%>
                                            <br /><br /><br />
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblApprover2" runat="server" Font-Bold="true"></asp:Label>
                                                <%-- <label>Approval 2</label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlAA2" runat="server" AppendDataBoundItems="true" TabIndex="5" ToolTip="Please Select Approval 2" CssClass="form-control"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlAA2_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <br /><br />
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblApprovalpath" runat="server" Font-Bold="true">Passing Path</asp:Label>
                                                <%-- <label>Approval Path </label>--%>
                                            </div>
                                            <asp:TextBox ID="txtAAPath" runat="server" ReadOnly="true" TextMode="MultiLine"
                                                Rows="1" TabIndex="7" ToolTip="Path" Height="45px"></asp:TextBox>
                                        </div>

                                         <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Days</label>
                                        </div>
                                        <asp:Panel ID="pnlDays" runat="server" TabIndex="3">
                                            <div class="form-group col-md-12 checkbox-list-box">

                                                <asp:CheckBox ID="chkDays" runat="server" AutoPostBack="true" OnCheckedChanged="chkDays_CheckedChanged" />
                                                Select All 
                                                <br />

                                                <asp:CheckBoxList ID="cblstDays" runat="server" RepeatColumns="1"
                                                    RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" ToolTip="Click to Select Days">
                                                </asp:CheckBoxList>
                                            </div>
                                        </asp:Panel>
                                             <br />
                                             <div class="label-dynamic">
                                                <asp:Label ID="lblApprover3" runat="server" Font-Bold="true"></asp:Label>
                                                <%--<label>Approval 3 </label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlAA3" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="6" ToolTip="Please Select Approval 3"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlAA3_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>

                             <asp:Panel ID="pnlauthority" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display:none;" >
                                               <sup>* </sup>
                                             <label>Approval Type</label>
                                            <asp:DropDownList ID="ddlauthorityapproval" runat="server" TabIndex="2"
                                                ToolTip="Please Select  Authority Approval Type" AppendDataBoundItems="true"
                                                AutoPostBack="true" 
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Approval 1</asp:ListItem>
                                                <asp:ListItem Value="2">Approval 2</asp:ListItem>
                                                <asp:ListItem Value="3">Approval 3</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>


                                                
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblStuTypeAuth" runat="server" Font-Bold="true">Student Type</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlStuTypeAuth" runat="server" TabIndex="1" ToolTip="Please Select Student Type." AppendDataBoundItems="true" 
                                                CssClass="form-control" data-select2-enable="true" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvfStuTypeAuth" runat="server" ControlToValidate="ddlStuTypeAuth"
                                                Display="None" ErrorMessage="Please Select Student Type." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                             <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup>* </sup>
                                             <label>Approval 1</label>
                                              <asp:TextBox ID="txtauthapproval1" runat="server"  CssClass="form-control"   />
                                                                    <asp:RequiredFieldValidator ID="rfvapproval1" runat="server" ControlToValidate="txtauthapproval1"
                                                                        Display="None" ErrorMessage="Please Enter Approval 1" ValidationGroup="AAPath" />
                                              </div>

                                              <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup>* </sup>
                                               <label>Approval 2</label>
                                              <asp:TextBox ID="txtauthapproval2" runat="server"  CssClass="form-control"   />
                                                                    <asp:RequiredFieldValidator ID="rfvapproval2" runat="server" ControlToValidate="txtauthapproval2"
                                                                        Display="None" ErrorMessage="Please Enter Approval 2" ValidationGroup="AAPath" />
                                               </div>

                                              <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup>* </sup>
                                             <label>Approval 3</label>
                                              <asp:TextBox ID="txtauthapproval3" runat="server"  CssClass="form-control"   />
                                                                    <asp:RequiredFieldValidator ID="rfvapproval3" runat="server" ControlToValidate="txtauthapproval3"
                                                                        Display="None" ErrorMessage="Please Enter Approval 3" ValidationGroup="AAPath" />
                                              </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup>* </sup>
                                             <label>Approval 4</label>
                                              <asp:TextBox ID="txtauthapproval4" runat="server"  CssClass="form-control"   />
                                              </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 table-responsive">
                                <asp:Panel ID="pnlEmpList" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Employees</div>
                                        <div class="panel-body">
                                            <asp:Panel ID="pnlEmp" runat="server" Height="600px" ScrollBars="Vertical">
                                                <%--<asp:ListView ID="lvEmployees" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl"
                                                            Text="Employee Not Found!" />
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid" class="vista-grid">
                                                            <h4 class="box-title">List of Employees</h4>

                                                            <table class="table table-bordered table-hover" id="tblSearchResults">
                                                                <tr class="bg-light-blue">
                                                                    <th>Sr.No
                                                                    </th>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" TabIndex="8" />
                                                                    </th>
                                                                    <th>Employee Name
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                            <td>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkIdNo" runat="server" ToolTip='<%# Eval("IDNO") %>' TabIndex="9" />

                                                                <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("NAME")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>--%>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Panel ID="pnlList" runat="server">
                                    
                                    <asp:LinkButton ID="btnaddauthority" runat="server" SkinID="LinkAddNew" 
                                        ToolTip="Click Add New To Enter Authority Approval" Text="Add Authority Approval" TabIndex="10" OnClick="btnaddauthority_Click"
                                        CssClass="btn btn-primary"></asp:LinkButton>

                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" 
                                        ToolTip="Click Add New To Enter Authority Approval Path" Text="Add New" TabIndex="11"
                                        CssClass="btn btn-primary" OnClick="btnAdd_Click"></asp:LinkButton>
                                    <asp:Button ID="btnShowReport" TabIndex="12" runat="server" Text="Show Report" CssClass="btn btn-info"
                                        ToolTip="Click here to Show the report" Visible="false" />
                                </asp:Panel>

                                <asp:Panel ID="pnlbtn" runat="server">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" TabIndex="13" ToolTip="Click here to Submit" ValidationGroup="AAPath" 
                                        CssClass="btn btn-primary" Onclick="btnSave_Click" />

                                    <asp:Button ID="btnCancel" runat="server" TabIndex="14" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                        CssClass="btn btn-warning" OnClick="btnCancel_Click" />&nbsp;
                                       <asp:Button ID="btnBack" runat="server" TabIndex="15" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false" 
                                           CssClass="btn btn-info" Onclick="btnBack_Click"/>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AAPath"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </asp:Panel>

                                  <asp:Panel ID="pnlauthoritybtn" runat="server">
                                    <asp:Button ID="btnauthSave" runat="server" Text="Submit" TabIndex="15" ToolTip="Click here to Submit" ValidationGroup="AAPath"
                                        CssClass="btn btn-primary" OnClick="btnauthSave_Click" />

                                    <asp:Button ID="btnauthCancel" runat="server" TabIndex="14" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                        CssClass="btn btn-warning" OnClick="btnauthCancel_Click" />&nbsp;
                                       <asp:Button ID="btnauthBack" runat="server" TabIndex="16" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false"
                                           CssClass="btn btn-info" Onclick="btnauthBack_Click" />
                                    
                                      <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="AAPath"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </asp:Panel>
                            </div>

                            <div class="col-12 table-club">
                                <asp:Panel ID="pnlAAPaList" runat="server">
                                    <asp:ListView ID="lvAAPath" runat="server" EnableModelValidation="True">
                                        <EmptyDataTemplate>
                                            <div>
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Approval Authority Master</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Authority Type </th>
                                                        <th>College/School</th>
                                                      <th><asp:Label ID="lblappproval1" runat="server"></asp:Label></th>
                                                      <th><asp:Label ID="lblappproval2" runat="server"></asp:Label></th>
                                                      <th><asp:Label ID="lblappproval3" runat="server"></asp:Label></th>
                                                      <th>Department</th>
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
                                                    <asp:ImageButton ID="btnauthappEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("APP_NO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="17" Onclick="btnauthappEdit_Click"/>
                                                    <asp:ImageButton ID="btnDelete" Visible="false" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("APP_NO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>

                                                <td>
                                                    <%# Eval("AUTHORITY_TYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVAL_1_UA_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVAL_2_UA_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVAL_3_UA_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPTNAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                              <div class="col-12 table-club">
                                <asp:Panel ID="pnlAuthapprovalList" runat="server">
                                    <asp:ListView ID="lvAuthapprovalList" runat="server" EnableModelValidation="True">
                                        <EmptyDataTemplate>
                                            <div>
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Approval Authority Master</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th>
                                                        <th>Approval 1</th>
                                                        <th>Approval 2</th>
                                                        <th>Approval 3 </th>
                                                        <th>Approval 4 </th>
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
                                                    <asp:ImageButton ID="Edit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("APP_NO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="18" OnClick="Edit_Click"/>
                                                </td>
                                               <td>
                                                    <%# Eval("APPROVAL1")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVAL2")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVAL3")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVAL4")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnaddauthority" />
            <asp:PostBackTrigger ControlID="btnShowReport" />
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnBack" />
            <asp:PostBackTrigger ControlID="ddlAA1" />
            <asp:PostBackTrigger ControlID="ddlAA2" />
            <asp:PostBackTrigger ControlID="ddlAA3" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
