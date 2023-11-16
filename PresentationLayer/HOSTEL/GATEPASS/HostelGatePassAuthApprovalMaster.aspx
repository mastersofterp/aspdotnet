<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelGatePassAuthApprovalMaster.aspx.cs" Inherits="HOSTEL_GATEPASS_HostelGatePassAuthApprovalMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
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
                                           </div>
                                            <asp:DropDownList ID="ddlStuType" runat="server" TabIndex="1" ToolTip="Please Select Student Type." AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlStuType_SelectedIndexChanged" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvfStuType" runat="server" ControlToValidate="ddlStuType"
                                                Display="None" ErrorMessage="Please Select Student Type." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br /> 
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblHostel" runat="server" Font-Bold="true">Hostel</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlHostel" runat="server" TabIndex="4" ToolTip="Please Select Hostel." AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                                Display="None" ErrorMessage="Please Select Hostel." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br />
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblApprover1" runat="server" Font-Bold="true" ></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAA1" runat="server" ToolTip="Please Select Approval_1" TabIndex="6" CssClass="form-control"  AppendDataBoundItems="true" 
                                                AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlAA1_SelectedIndexChanged" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="14">Parent</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvapprovalauth1" runat="server" ControlToValidate="ddlAA1"
                                                Display="None" ErrorMessage="Please Select  Approval 1" SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="14">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlAA1parent" runat="server" AppendDataBoundItems="true" TabIndex="7" ToolTip="Please Select Approval 2" CssClass="form-control"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true" Visible="false"  >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="14">Parent</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvapprovalauth1parent" runat="server" ControlToValidate="ddlAA1parent"
                                                Display="None" ErrorMessage="Please Select  Approval 2" SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br />
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblApprover4" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAA4" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="9" ToolTip="Please Select Approval 4"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlAA4_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvapprovalauth4" runat="server" ControlToValidate="ddlAA4"
                                                Display="None" ErrorMessage="Please Select  Approval 4" SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
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
                                            <asp:RequiredFieldValidator ID="rfvApp" runat="server" ControlToValidate="ddlApp"
                                                Display="None" ErrorMessage="Please Select Authority Approval Type." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br />
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true">Department</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddldepartment" runat="server" TabIndex="5" ToolTip="Please Select Approval_1" AppendDataBoundItems="true" CssClass="form-control"
                                                AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvdepartment" runat="server" ControlToValidate="ddldepartment"
                                                Display="None" ErrorMessage="Please Select Department." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br />
                                             <div class="label-dynamic">
                                                 <sup>* </sup>
                                                <asp:Label ID="lblApprover2" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAA2" runat="server" AppendDataBoundItems="true" TabIndex="7" ToolTip="Please Select Approval 2" CssClass="form-control"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlAA2_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvapprovalauth2" runat="server" ControlToValidate="ddlAA2"
                                                Display="None" ErrorMessage="Please Select  Approval 2" SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlAA2Parent" runat="server" AppendDataBoundItems="true" TabIndex="7" ToolTip="Please Select Approval 2" CssClass="form-control" Visible="false"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="14">Parent</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvapprovalauth2parent" runat="server" ControlToValidate="ddlAA2Parent" Visible="false"
                                                Display="None" ErrorMessage="Please Select  Approval 2" SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br />
                                             
                                            
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblApprover5" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAA5" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="10" ToolTip="Please Select Approval 5"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlAA5_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                         <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDays" runat="server" Font-Bold="true">Days Type</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDays" runat="server" ToolTip="Please Select Days Type." TabIndex="3" AppendDataBoundItems="true" 
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDays_SelectedIndexChanged" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDays" runat="server" ControlToValidate="ddlDays"
                                                Display="None" ErrorMessage="Please Select Days Type." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br /> 
                                           <br /><br /><br /> 
                                            <br /><br />
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblApprover3" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAA3" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="8" ToolTip="Please Select Approval 3"
                                                Enabled="false" AutoPostBack="True" data-select2-enable="true" OnSelectedIndexChanged="ddlAA3_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvapprovalauth3" runat="server" ControlToValidate="ddlAA3"
                                                Display="None" ErrorMessage="Please Select  Approval 3" SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <br /><br /><br />
                                             <div class="label-dynamic">
                                                <asp:Label ID="lblApprovalpath" runat="server" Font-Bold="true">Passing Path</asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtAAPath" runat="server" ReadOnly="true" TextMode="MultiLine"
                                                Rows="1" TabIndex="7" ToolTip="Path" Height="67px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                             <asp:Panel ID="pnlauthority" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                       <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblStuTypeAuth" runat="server" Font-Bold="true">Student Type</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlStuTypeAuth" runat="server" ToolTip="Please Select Student Type." AppendDataBoundItems="true" 
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="True" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvfStuTypeAuth" runat="server" ControlToValidate="ddlStuTypeAuth"
                                                Display="None" ErrorMessage="Please Select Student Type." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDaysAuth" runat="server" Font-Bold="true">Days Type</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDaysAuth" runat="server" ToolTip="Please Select Days Type." AppendDataBoundItems="true" 
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="True" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDaysAuth" runat="server" ControlToValidate="ddlDaysAuth"
                                                Display="None" ErrorMessage="Please Select Days Type." SetFocusOnError="true"
                                                ValidationGroup="AAPath" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                             <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup>* </sup>
                                             <label >Approval 1</label>
                                              <asp:TextBox ID="txtauthapproval1" runat="server"  CssClass="form-control"  AutoPostBack="true" />
                                                                    <asp:RequiredFieldValidator ID="rfvapproval1" runat="server" ControlToValidate="txtauthapproval1"
                                                                        Display="None" ErrorMessage="Please Enter Approval 1" ValidationGroup="AAPath" />
                                              </div>

                                              <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup>* </sup>
                                               <label>Approval 2</label>
                                              <asp:TextBox ID="txtauthapproval2" runat="server"  CssClass="form-control"  AutoPostBack="true" />
                                                                    <asp:RequiredFieldValidator ID="rfvapproval2" runat="server" ControlToValidate="txtauthapproval2"
                                                                        Display="None" ErrorMessage="Please Enter Approval 2" ValidationGroup="AAPath" />
                                               </div>

                                              <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup>* </sup>
                                             <label>Approval 3</label>
                                              <asp:TextBox ID="txtauthapproval3" runat="server"  CssClass="form-control"  AutoPostBack="true" />
                                                                    <asp:RequiredFieldValidator ID="rfvapproval3" runat="server" ControlToValidate="txtauthapproval3"
                                                                        Display="None" ErrorMessage="Please Enter Approval 3" ValidationGroup="AAPath" />
                                              </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup>* </sup>
                                             <label>Approval 4</label>
                                              <asp:TextBox ID="txtauthapproval4" runat="server"  CssClass="form-control"  AutoPostBack="true" />
                                                <asp:RequiredFieldValidator ID="rfvapproval4" runat="server" ControlToValidate="txtauthapproval4"
                                                                        Display="None" ErrorMessage="Please Enter Approval 4" ValidationGroup="AAPath" />
                                              </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                               <sup></sup>
                                             <label>Approval 5</label>
                                              <asp:TextBox ID="txtauthapproval5" runat="server"  CssClass="form-control"  AutoPostBack="true" />
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
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Click here to Submit" ValidationGroup="AAPath" 
                                        CssClass="btn btn-primary" Onclick="btnSave_Click" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                        CssClass="btn btn-warning" OnClick="btnCancel_Click" />&nbsp;
                                       <asp:Button ID="btnBack" runat="server" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false" 
                                           CssClass="btn btn-info" Onclick="btnBack_Click"/>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AAPath"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </asp:Panel>

                                  <asp:Panel ID="pnlauthoritybtn" runat="server">
                                    <asp:Button ID="btnauthSave" runat="server" Text="Submit" ToolTip="Click here to Submit" ValidationGroup="AAPath"
                                        CssClass="btn btn-primary" OnClick="btnauthSave_Click" />

                                    <asp:Button ID="btnauthCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                        CssClass="btn btn-warning" OnClick="btnauthCancel_Click" />&nbsp;
                                       <asp:Button ID="btnauthBack" runat="server" Text="Back" ToolTip="Click here to go back to previous" CausesValidation="false"
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
                                                        <th>Authority Type</th>
                                                        <th>Student Type</th>
                                                        <th>Days Type</th>
                                                        <th>Department</th>
                                                        <th>Approval 1</th>
                                                        <th>Approval 2</th>
                                                        <th>Approval 3</th>
                                                        <th>Approval 4</th>
                                                        <th>Approval 5</th>
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
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="17" OnClick="btnauthappEdit_Click" />
                                                    <asp:ImageButton ID="btnDelete" Visible="false" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("APP_NO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("AUTHORITY_TYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENT_TYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DAY_TYPE_NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPTNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("APPROVAL_1_UANO").ToString() == "14" ? "Parents" : (!String.IsNullOrEmpty(Eval("APPROVAL1") as string) ? Eval("APPROVAL1") : "") + "  -  " + (!String.IsNullOrEmpty(Eval("APPROVAL_1_UA_NAME") as string) ? Eval("APPROVAL_1_UA_NAME") : "") %>

                                                    
                                                </td>
                                                <td>
                                                     <%# Eval("APPROVAL_2_UANO").ToString() == "14" ? "Parents" : (!String.IsNullOrEmpty(Eval("APPROVAL2") as string) ? Eval("APPROVAL2") : "") + "  -  " + (!String.IsNullOrEmpty(Eval("APPROVAL_2_UA_NAME") as string) ? Eval("APPROVAL_2_UA_NAME") : "") %>
                                                  
                                                </td>
                                                <td>
                                                    <%# !String.IsNullOrEmpty(Eval("APPROVAL3") as string) ? Eval("APPROVAL3") : "" %>  -  
                                                    <%# !String.IsNullOrEmpty(Eval("APPROVAL_3_UA_NAME") as string) ? Eval("APPROVAL_3_UA_NAME") : "" %>
                                                </td>
                                                <td>
                                                    <%# !String.IsNullOrEmpty(Eval("APPROVAL4") as string) ? Eval("APPROVAL4") : "" %>  -  
                                                    <%# !String.IsNullOrEmpty(Eval("APPROVAL_4_UA_NAME") as string) ? Eval("APPROVAL_4_UA_NAME") : "" %>
                                                </td>
                                                <td>
                                                    <%# !String.IsNullOrEmpty(Eval("APPROVAL5") as string) ? Eval("APPROVAL5") : "" %>  -  
                                                    <%# !String.IsNullOrEmpty(Eval("APPROVAL_5_UA_NAME") as string) ? Eval("APPROVAL_5_UA_NAME") : "" %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div >


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
                                                        <th>Student Type</th>
                                                        <th>Days Type</th>
                                                        <th>Approval 1</th>
                                                        <th>Approval 2</th>
                                                        <th>Approval 3 </th>
                                                        <th>Approval 4 </th>
                                                        <th>Approval 5 </th>
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
                                                    <%# Eval("STUDENT_TYPE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DAYS_TYPE")%>
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
                                                <td>
                                                    <%# Eval("APPROVAL5")%>
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
            <asp:PostBackTrigger ControlID="ddlAA4" />
            <asp:PostBackTrigger ControlID="ddlAA5" />
            <asp:PostBackTrigger ControlID="ddlStuTypeAuth" />
            <asp:PostBackTrigger ControlID="ddlStuType" />
            <asp:PostBackTrigger ControlID="ddlDaysAuth" />
            <asp:PostBackTrigger ControlID="ddlDays" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
