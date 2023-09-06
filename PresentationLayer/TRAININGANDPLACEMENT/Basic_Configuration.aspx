<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Basic_Configuration.aspx.cs" Inherits="Basic_Configuration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfCurrency" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfJobType" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfJobSector" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfCarrerArea" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfAssociationfor" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfPlacementStatus" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfJobRole" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfRounds" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfIntervals" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="hfSkills" runat="server" ClientIDMode="Static" />
      <asp:HiddenField ID="hfLevel" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="hflanguage" runat="server" ClientIDMode="Static" />
       <asp:HiddenField ID="hfProficiency" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfWorkArea" runat="server" ClientIDMode="Static" />
      <asp:HiddenField ID="hfCategory" runat="server" ClientIDMode="Static" />
      <asp:HiddenField ID="hfExam" runat="server" ClientIDMode="Static" />

    <asp:UpdatePanel ID="upnlmain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }
        .badge-warning {
            color: #fff !important;
        }
    </style>

       
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Basic Configuration</h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1">Currency</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">Job Sector</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3">Job Type</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_4">Job Role</a>
                            </li>
                            <%--<li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_5">Career Areas</a>
                            </li>--%>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_6">Placement Mode</a>
                            </li>
                            <%--<li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_7">Designation</a>
                            </li>--%>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_8">Association for</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_9">Rounds</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_10">Intervals</a>
                            </li>
                           <%-- //-----start--//--%>

                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_11">Skill</a>
                            </li>
                             <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_12">Level</a>
                            </li>
                             <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_13">Language</a>
                            </li>
                             <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_14">Proficiency</a>
                            </li>
                             <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_15">Work Area</a>
                            </li>
                              <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_16">Category</a>
                            </li>
                             <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_17">Exam</a>
                            </li>
                            <%-- //-----end--//--%>
                        </ul>

                        <div class="tab-content">

                            <div class="tab-pane active" id="tab_1">
                                <asp:UpdatePanel ID="upnlCurrency" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label><span style="color:red;">*</span>Currency</label>
                                                    </div>
                                                    <asp:TextBox ID="txtCurrency" runat="server" CssClass="form-control" placeholder="Currency Name" MaxLength="28" onkeypress="return alphaOnly(event);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                TargetControlID="txtCurrency" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                </div>
                                                
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                      <%-- <asp:CheckBox ID="chkStatusCurrency" runat="server" Checked="true" />--%>
                                                         <input type="checkbox" id="chkStatusCurrency" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkStatusCurrency"></label>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmitCurrency" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmit_ClickCurrency" OnClientClick=" return validate();" ValidationGroup="currency">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelCurrency" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_ClickCurrency">Cancel</asp:LinkButton>
                                                    <asp:LinkButton ID="btnShowCurrency" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowCurrency_Click">Show</asp:LinkButton>
                                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="currency" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <asp:ListView ID="lvCurrency" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Currency List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Currency No
                                                                </th>
                                                                <th>Currency Name
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("CUR_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record" CausesValidation="false" 
                                                                OnClick="btnEdit_ClickCurrency" ></asp:ImageButton><%--CssClass="fa fa-pencil-square-o"   CausesValidation="false"--%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("CUR_NO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("CUR_NAME")%>
                                                        </td>
                                                     <td> <%-- <td class="text-center"><span class="badge badge-success"><%# Eval("STATUS")%>--%>
                                                           <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                           <%-- <%# Eval("STATUS")%>--%>
                                                        </td>
                                                        
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("CUR_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEdit_ClickCurrency" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("CUR_NO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("CUR_NAME")%>
                                                        </td>
                                                       <td ><%--<span class="badge badge-success"> <%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                           
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnSubmitCurrency" />
                                        <asp:PostBackTrigger ControlID="lvCurrency" />
                                        <asp:PostBackTrigger ControlID="btnCancelCurrency" />--%>
                                        <%--<asp:PostBackTrigger ControlID="chkStatusCurrency" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>


                            <div class="tab-pane fade" id="tab_2">
                                <asp:UpdatePanel ID="upnlJobSector" runat="server">
                                    <ContentTemplate>
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Job Sector</label>
                                            </div>
                                            <asp:TextBox ID="txtJobSector" runat="server" CssClass="form-control" placeholder="Sector Name"  MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                TargetControlID="txtJobSector" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkStatusJobSector" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkStatusJobSector"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitJobSector" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitJobSector_Click" OnClientClick="return validateJobSector();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelJobSector" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelJobSector_Click">Cancel</asp:LinkButton>
                                             <asp:LinkButton ID="btnShowJobSector" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowJobSector_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-12">
                                            <asp:ListView ID="lvJobSector" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Job Sector List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Job Sector No
                                                                </th>
                                                                <th>Job Sector
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditJobSector" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("JOBSECNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditJobSector_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBSECNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBSECTOR")%>
                                                        </td>
                                                      <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                          <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("JOBSECNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditJobSector_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBSECNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBSECTOR")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                             <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        </ContentTemplate>
                                    <Triggers>
                                      <%--  <asp:PostBackTrigger ControlID="btnSubmitJobSector"/>
                                        <asp:PostBackTrigger ControlID="lvJobSector" />--%>
                                    </Triggers>
                                    </asp:UpdatePanel>
                            </div>


                            <div class="tab-pane fade" id="tab_3">
                                <asp:UpdatePanel ID="upnlJobType" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label><span style="color:red;">*</span>Job Type</label>
                                                    </div>
                                                    <asp:TextBox ID="txtJobType" runat="server" CssClass="form-control" placeholder="Job Type" onkeypress="return alphaOnly(event);" MaxLength="28" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                TargetControlID="txtJobType" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                </div>
                                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkStatusJobType" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkStatusJobType"></label>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmitJobType" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmit_ClickJobType" OnClientClick="return validateJobType();">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelJobType" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelJobType_Click">Cancel</asp:LinkButton>
                                                    <asp:LinkButton ID="btnShowJobType" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowJobType_Click">Show</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12" >
                                            <asp:ListView ID="lvJobType" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Job Type List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowarap display" style="width:100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th >Edit
                                                                </th>
                                                                <th >Job Type No.
                                                                </th>
                                                                <th>Job Type
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                        <td >
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("JOBNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td >
                                                            <%# Eval("JOBNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBTYPE")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"> <%# Eval("STATUS")%></span>--%>
                                                           <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("JOBNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBTYPE")%>
                                                        </td>
                                                        <td class="text-center"><%--<span class="badge badge-success"> <%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnSubmitJobType" />
                                        <asp:PostBackTrigger ControlID="btnCancelJobType" />
                                        <asp:PostBackTrigger ControlID="lvJobType" />--%>

                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>


                            <div class="tab-pane fade" id="tab_4">
                                <asp:UpdatePanel ID="upnlJobRole" runat="server">
                                    <ContentTemplate>
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Job Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlJobTypes" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Job Role Name</label>
                                            </div>
                                            <asp:TextBox ID="txtJobRoleName" runat="server" CssClass="form-control" placeholder="Job Role" MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                TargetControlID="txtJobRoleName" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkJobRoleStatus" name="switch" class="switch" checked /> 
                                                        <label data-on="Active" data-off="Inactive" for="chkJobRoleStatus"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitJobRole" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitJobRole_Click" ToolTip="Submit Button" OnClientClick="return validateJobRole();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelJobRole" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelJobRole_Click">Cancel</asp:LinkButton>
                                            <asp:LinkButton ID="btnShowJobRole" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowJobRole_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                         <div class="col-12">
                                            <asp:ListView ID="lvJobRole" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Job Role List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Job Type
                                                                </th>
                                                                <th>Job Role
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditJobRole" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("ROLENO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditJobRole_Click"/>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBTYPE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBROLETYPE")%>
                                                        </td>
                                                        <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditJobRole" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("ROLENO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditJobRole_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBTYPE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("JOBROLETYPE")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                              <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>

                                        </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnSubmitJobRole" />
                                        <asp:PostBackTrigger ControlID="lvJobRole" />
                                        <asp:PostBackTrigger ControlID="btnCancelJobRole" />--%>
                                    </Triggers>
                                    </asp:UpdatePanel>
                            </div>


                            <div class="tab-pane fade" id="tab_5" style="display:none">
                                <div class="col-12 mt-3">
                                    <asp:UpdatePanel ID="upnlCarrerArea" runat="server">
                                        <ContentTemplate>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Career Areas</label>
                                            </div>
                                            <asp:TextBox ID="txtCareerAreas" runat="server" CssClass="form-control" placeholder="Career Area Name" MaxLength="28"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                                TargetControlID="txtCareerAreas" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />

                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkStatusCarrerArea" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkStatusCarrerArea"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                             <asp:LinkButton ID="btnSubmitCarrerArea" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitCarrerArea_Click" OnClientClick="return validateCarrerArea();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelCarrerArea" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelCarrerArea_Click">Cancel</asp:LinkButton>
                                        </div>
                                    </div>
                                            <div class="col-12">
                                            <asp:ListView ID="lvCarrerArea" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Career Area List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Career Area No
                                                                </th>
                                                                <th>Career Area
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:LinkButton ID="btnEditCarrerArea" runat="server" CausesValidation="false" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("WORKAREANO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditCarrerArea_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("WORKAREANO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("WORKAREANAME")%>
                                                        </td>
                                                        <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="btnEditCarrerArea" runat="server" CausesValidation="false"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("WORKAREANO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditCarrerArea_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("WORKAREANO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("WORKAREANAME")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                             <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                            </ContentTemplate>
                                        <Triggers>
                                            <%--<asp:PostBackTrigger ControlID="btnSubmitCarrerArea" />
                                            <asp:PostBackTrigger ControlID="lvCarrerArea" />--%>
                                        </Triggers>
                                        </asp:UpdatePanel>
                                </div>
                            </div>


                            <div class="tab-pane fade" id="tab_6">
                                <asp:UpdatePanel ID="upnlPlacementStatus" runat="server">
                                    <ContentTemplate>
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Placement Mode</label>
                                            </div>
                                            <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" placeholder="Placement Status" MaxLength="28" onkeypress="return alphaOnly(event);" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                                TargetControlID="txtStatus" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkStatusPlacement" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkStatusPlacement"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitPlacementStatus" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitPlacementStatus_Click" OnClientClick="return validatePlacementSatus();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelPlacementStatus" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelPlacementStatus_Click">Cancel</asp:LinkButton>
                                             <asp:LinkButton ID="btnShowPlacementStatus" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowPlacementStatus_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                        <div class="col-12">
                                            <asp:ListView ID="lvPlacementStatus" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Placement Status List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Status No
                                                                </th>
                                                                <th>Placement Mode
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditPlacementStatus" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("STATUS_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditPlacementStatus_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("STATUS_NO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PLACED_STATUS")%>
                                                        </td>
                                                        <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                          <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditPlacementStatus" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("STATUS_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditPlacementStatus_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("STATUS_NO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PLACED_STATUS")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                              <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        </ContentTemplate>
                                    <Triggers>
                                       <%-- <asp:PostBackTrigger ControlID="btnSubmitPlacementStatus" />
                                        <asp:PostBackTrigger ControlID="lvPlacementStatus" />--%>
                                    </Triggers>
                                    </asp:UpdatePanel>
                            </div>


                           <%-- <div class="tab-pane fade" id="tab_7">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Designation</label>
                                            </div>
                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" placeholder="Designation Name" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkStatusDesignation" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmit6" runat="server" CssClass="btn btn-outline-primary">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel6" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>


                            <div class="tab-pane fade" id="tab_8">
                                <asp:UpdatePanel ID="upnlAssociationfor" runat="server">
                                    <ContentTemplate>
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Association for</label>
                                            </div>
                                            <asp:TextBox ID="txtAssociation" runat="server" CssClass="form-control" placeholder="Association Name" MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                TargetControlID="txtAssociation" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkAssociationFor" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkAssociationFor"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                             <asp:LinkButton ID="btnSubmitAssociationfor" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitAssociationfor_Click" OnClientClick="return validateAssociationFor();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelAssociationFor" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelAssociationFor_Click">Cancel</asp:LinkButton>
                                            <asp:LinkButton ID="btnShowAssociationFor" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowAssociationFor_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                        <div class="col-12">
                                            <asp:ListView ID="lvAssocationfor" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Association For List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Association No
                                                                </th>
                                                                <th>Association for
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditAssociationfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("ASSOCIATIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditAssociationfor_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("ASSOCIATIONNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ASSOCIATION_FOR")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditAssociationfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("ASSOCIATIONNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditAssociationfor_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("ASSOCIATIONNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ASSOCIATION_FOR")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                              <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnSubmitAssociationfor" />
                                        <asp:PostBackTrigger ControlID="lvAssocationfor" />--%>
                                    </Triggers>
                                    </asp:UpdatePanel>
                            </div>






                              <div class="tab-pane fade" id="tab_9">
                                  <asp:UpdatePanel ID="upnlRound" runat="server">
                                      <ContentTemplate>

                                      
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Rounds</label>
                                            </div>
                                            <asp:TextBox ID="txtRound" runat="server" CssClass="form-control" placeholder="Round Name" MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                                TargetControlID="txtRound" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkStatusround" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkStatusround"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitround" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return validateRound();" OnClick="btnSubmit_ClickRound">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelround" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_ClickRound">Cancel</asp:LinkButton>
                                            <asp:LinkButton ID="btnShowRound" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowRound_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="lvRound" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Round List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">

                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Round No
                                                                </th>
                                                                <th>Rounds
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditAssociationfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("SELECTNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_ClickRounds" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("SELECTNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SELECTNAME")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditAssociationfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("SELECTNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEdit_ClickRounds" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("SELECTNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SELECTNAME")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                        <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                           
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>




                            <div class="tab-pane fade" id="tab_10">
                                  <asp:UpdatePanel ID="upnlInterval" runat="server">
                                      <ContentTemplate>

                                      
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Intervals</label>
                                            </div>
                                            <asp:TextBox ID="txtIntervals" runat="server" CssClass="form-control" placeholder="Intervals " MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                                TargetControlID="txtIntervals" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkStatusIntervals" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkStatusIntervals"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitIntervals" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return validateinterval();" OnClick="btnSubmit_ClickIntervals">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelIntervals" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_ClickIntervals">Cancel</asp:LinkButton>
                                             <asp:LinkButton ID="btnShowIntervals" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowIntervals_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="lvIntervals" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Intervals List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Int No
                                                                </th>
                                                                <th>Intervals
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditAssociationfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("INTNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_ClickIntervals" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("INTNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("INTERVALS")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditIntervals" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("INTNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEdit_ClickIntervals" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("INTNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("INTERVALS")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                        <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>



                        <%--    //-----start-----//--%>

                                <div class="tab-pane fade" id="tab_11">
                                  <asp:UpdatePanel ID="UpdateSkill" runat="server">
                                      <ContentTemplate>

                                      
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Skills</label>
                                            </div>
                                            <asp:TextBox ID="txtSkills" runat="server" CssClass="form-control" placeholder="Please Enter Skills " MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                           <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                                TargetControlID="txtSkills" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />--%>
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkStatusSkills" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chkStatusSkills"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSkill" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return validateskill();" OnClick="btnSkill_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="BtnSkillCacel" runat="server" CssClass="btn btn-outline-danger" OnClick="BtnSkillCacel_Click">Cancel</asp:LinkButton>
                                            <asp:LinkButton ID="btnShowSkills" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowSkills_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="lvskill" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Skills List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Skil No
                                                                </th>
                                                                <th>Skills
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditSkillfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("SKILNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSkillfor_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("SKILNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SKILLS")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditSkillfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("SKILNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditSkillfor_Click"/>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SKILNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SKILLS")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                        <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>

                            <%--    //-----end-----//--%>

                              <%--    //-----start-----//--%>

                                <div class="tab-pane fade" id="tab_12">
                                  <asp:UpdatePanel ID="UpdatePanellevel" runat="server">
                                      <ContentTemplate>
  
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Level</label>
                                            </div>
                                            <asp:TextBox ID="txtlevel" runat="server" CssClass="form-control" placeholder="Please Enter Level " MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"
                                                                TargetControlID="txtlevel" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="checklevel" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="checklevel"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnlevelSubmit" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return validatelevel();" OnClick="btnlevelSubmit_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnlevelCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnlevelCancel_Click">Cancel</asp:LinkButton>
                                            <asp:LinkButton ID="btnShowlevel" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowlevel_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="lsLevel" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Level List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Level No
                                                                </th>
                                                                <th>Level
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditlevelfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("LEVELNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditlevelfor_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("LEVELNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LEVELS")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditSkillfor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("LEVELNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditlevelfor_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("LEVELNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LEVELS")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                        <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>

                            <%--    //-----end-----//--%>

                            
                              <%--    //-----start-----//--%>

                                <div class="tab-pane fade" id="tab_13">
                                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                      <ContentTemplate>
  
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Language</label>
                                            </div>
                                            <asp:TextBox ID="txtlanguage" runat="server" CssClass="form-control" placeholder="Please Enter Level " MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server"
                                                                TargetControlID="txtlanguage" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="checklanguage" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="checklanguage"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnlanguage" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return validatelanguage();" OnClick="btnlanguage_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btncanlang" runat="server" CssClass="btn btn-outline-danger" OnClick="btncanlang_Click">Cancel</asp:LinkButton>
                                             <asp:LinkButton ID="btnShowlanguage" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowlanguage_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="lvlanguage" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Language List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Language No
                                                                </th>
                                                                <th>Languages
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditlanguagefor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("LANGUAGENO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditlanguagefor_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("LANGUAGENO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LANGUAGES")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditlanguagefor" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("LANGUAGENO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditlanguagefor_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("LANGUAGENO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("LANGUAGES")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                        <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>

                            <%--    //-----end-----//--%>


                                <%--    //-----start-----//--%>

                                <div class="tab-pane fade" id="tab_14">
                                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                      <ContentTemplate>
  
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Proficiency</label>
                                            </div>
                                            <asp:TextBox ID="txtProficiency" runat="server" CssClass="form-control" placeholder="Please Enter Proficiency " MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                                TargetControlID="txtProficiency" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chProficiency" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chProficiency"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnProficiencySubmit" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return validateProficiency();" OnClick="btnProficiencySubmit_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                             <asp:LinkButton ID="btnShowProficiency" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowProficiency_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="LvProficiency" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Proficiency List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Proficiency No
                                                                </th>
                                                                <th>Proficiency
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditProficiency" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("PROFNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditProficiency_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("PROFNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PROFICIENCY")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditProficiency" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("PROFNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditProficiency_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("PROFNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PROFICIENCY")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                        <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>

                            <%--    //-----end-----//--%>

                             <%--    //-----start-----//--%>

                                <div class="tab-pane fade" id="tab_15">
                                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                      <ContentTemplate>
  
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Work Area</label>
                                            </div>
                                            <asp:TextBox ID="txtWorkArea" runat="server" CssClass="form-control" placeholder="Please Enter Work Area " MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                                TargetControlID="txtWorkArea" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chworkarea" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chworkarea"></label>

                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnWorkAresSubmit" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return validateWorkArea();" OnClick="btnWorkAresSubmit_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnworkareacancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnworkareacancel_Click">Cancel</asp:LinkButton>
                                             <asp:LinkButton ID="BtnWorkAreaShow" runat="server" CssClass="btn btn-outline-primary" OnClick="BtnWorkAreaShow_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="lvWorkArea" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Work Area List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Work Area No
                                                                </th>
                                                                <th>Work Area
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditWorkArea" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("WORKAREANO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditWorkArea_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("WORKAREANO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("WORKAREANAME")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditProficiency" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"  CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("WORKAREANO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEditWorkArea_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("WORKAREANO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("WORKAREANAME")%>
                                                        </td>
                                                          <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                        <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                            
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>

                            <%--    //-----end-----//--%>

                               <%--    //-----start-----//--%>

                                <div class="tab-pane fade" id="tab_16">
                                  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                      <ContentTemplate>
  
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Category</label>
                                            </div>
                                            <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control" placeholder="Please Enter Category " MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                                                TargetControlID="txtCategory" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chCategory" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chCategory"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitCategory" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return  validateCategory();" OnClick="btnSubmitCategory_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelCategory" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelCategory_Click">Cancel</asp:LinkButton>
                                             <asp:LinkButton ID="btnShowCategory" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShowCategory_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="lvCategory" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Category List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Category No
                                                                </th>
                                                                <th>Category
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditCategory" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("CATEGORYNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditCategory_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("CATEGORYNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("CATEGORYS")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditCategory" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("CATEGORYNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditCategory_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("CATEGORYNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("CATEGORYS")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>

                            <%--    //-----end

                                 <%--    //-----start-----//--%>

                                <div class="tab-pane fade" id="tab_17">
                                  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                      <ContentTemplate>
  
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label><span style="color:red;">*</span>Exam</label>
                                            </div>
                                            <asp:TextBox ID="txtExam" runat="server" CssClass="form-control" placeholder="Please Enter Exam " MaxLength="28" onkeypress="return alphaOnly(event);"/>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                                                TargetControlID="txtExam" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chExam" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="chExam"></label>
                                                    </div>
                                                </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnExamSubmit" runat="server" CssClass="btn btn-outline-primary" OnClientClick="return  validateExam();" OnClick="btnExamSubmit_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnExamCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnExamCancel_Click">Cancel</asp:LinkButton>
                                             <asp:LinkButton ID="btnExamShow" runat="server" CssClass="btn btn-outline-primary" OnClick="btnExamShow_Click">Show</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                   <div class="col-12">
                                            <asp:ListView ID="lvExam" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Exam List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Exam No
                                                                </th>
                                                                <th>Exam
                                                                </th>
                                                                <th class="text-center">Active Status
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
                                                            <asp:ImageButton ID="btnEditExam" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("EXAMNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditExam_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("EXAMNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("EXAMS")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEditExam" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" CssClass="fa fa-pencil-square-o"
                                                                CommandArgument='<%# Eval("EXAMNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditExam_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("EXAMNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("EXAMS")%>
                                                        </td>
                                                         <td class="text-center"><%--<span class="badge badge-success"><%# Eval("STATUS")%></span>--%>
                                                            <asp:Label ID="lblstatus"   CssClass="badge"  runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </div>
                                          </ContentTemplate>
                                  </asp:UpdatePanel>
                            </div>

                            <%--    //-----end-----//--%>


                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

             </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Currency Script Starts from Here--%>
    <script>
        function SetStat(val) {
            $('#chkStatusCurrency').prop('checked', val);
        }

        function validate() {

            $('#hfCurrency').val($('#chkStatusCurrency').prop('checked'));

            var txtCurrency = $("[id$=txtCurrency]").attr("id");
            var txtCurrency = document.getElementById(txtCurrency);
         
            if (txtCurrency.value.length == 0) {
                alert('Please Enter Currency ', 'Warning!');
             //   $(txtCurrency).css('border-color', 'red');
                $(txtCurrency).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitCurrency').click(function () {
                    validate();
                });
            });
        });
        </script>

    <%-- Currency Script Ends Here--%>

    <%-- Job Type Script Starts from Here--%>
    <script>
        function SetJobType(val) {
            $('#chkStatusJobType').prop('checked', val);
        }

        function validateJobType() {

            $('#hfJobType').val($('#chkStatusJobType').prop('checked'));

            var txtCurrency = $("[id$=txtJobType]").attr("id");
            var txtCurrency = document.getElementById(txtCurrency);

            if (txtCurrency.value.length == 0) {
                alert('Please Enter Job Type ', 'Warning!');

                //     $(txtCurrency).css('border-color', 'red');
                $(txtCurrency).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitJobType').click(function () {
                    validate();
                });
            });
        });
        </script>
    <%-- Job Type Script Ends Here--%>

    <%-- Job Sector Script Starts from Here--%>
     <script>
         function SetJobSector(val) {
             $('#chkStatusJobSector').prop('checked', val);
         }

         function validateJobSector() {

             $('#hfJobSector').val($('#chkStatusJobSector').prop('checked'));

             var txtCurrency = $("[id$=txtJobSector]").attr("id");
             var txtCurrency = document.getElementById(txtCurrency);

             if (txtCurrency.value.length == 0) {
                 alert('Please Enter Job Sector ', 'Warning!');
                // $(txtCurrency).css('border-color', 'red');
                 $(txtCurrency).focus();
                 return false;
             }
         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitJobSector').click(function () {
                     validate();
                 });
             });
         });
        </script>
    <%-- Job Sector Script Ends Here--%>

    <%--Carrer Area Script Starts from Here--%>
     <script>
         function SetCarrerArea(val) {
             $('#chkStatusCarrerArea').prop('checked', val);
         }

         function validateCarrerArea() {

             $('#hfCarrerArea').val($('#chkStatusCarrerArea').prop('checked'));

             var txtCurrency = $("[id$=txtCareerAreas]").attr("id");
             var txtCurrency = document.getElementById(txtCurrency);

             if (txtCurrency.value.length == 0) {
                 alert('Please Enter Career Area Name', 'Warning!');
                // $(txtCurrency).css('border-color', 'red');
                 $(txtCurrency).focus();
                 return false;
             }
         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitCarrerArea').click(function () {
                     validate();
                 });
             });
         });
        </script>
    
    <%--Carrer Area Script End Here--%>

    <%-- Association for Script Starts from Here--%>
     <script>   
         function SetAssociationfor(val) {
             $('#chkAssociationFor').prop('checked', val);
         }

         function validateAssociationFor() {

             $('#hfAssociationfor').val($('#chkAssociationFor').prop('checked'));

             var txtCurrency = $("[id$=txtAssociation]").attr("id");
             var txtCurrency = document.getElementById(txtCurrency);

             if (txtCurrency.value.length == 0) {
                 alert('Please Enter Association Name', 'Warning!');
               //  $(txtCurrency).css('border-color', 'red');
                 $(txtCurrency).focus();
                 return false;
             }
         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitAssociationfor').click(function () {
                     validate();
                 });
             });
         });
        </script>
    <%-- Association for Script Ends Here--%>

    <%-- Placment Status Script Starts from Here--%>
     <script>
         function SetPlacementStatus(val) {
             $('#chkStatusPlacement').prop('checked', val);
         }

         function validatePlacementSatus() {

             $('#hfPlacementStatus').val($('#chkStatusPlacement').prop('checked'));

             var txtCurrency = $("[id$=txtStatus]").attr("id");
             var txtCurrency = document.getElementById(txtCurrency);

             if (txtCurrency.value.length == 0) {
                 alert('Please Enter Placement Mode', 'Warning!');
                 //$(txtCurrency).css('border-color', 'red');
                 $(txtCurrency).focus();
                 return false;
             }
         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitPlacementStatus').click(function () {
                     validate();
                 });
             });
         });
        </script>
    <%-- Placment Status Script Ends Here--%>

    <%-- Job Role Script Starts from Here--%>
     <script>
         function SetJobRole(val) {
             $('#chkJobRoleStatus').prop('checked', val);
         }

         function validateJobRole() {

             $('#hfJobRole').val($('#chkJobRoleStatus').prop('checked'));
             //alert('helo');
             var ddljobtype = $("[id$=ddlJobTypes]").attr("id");
             //var ddljobtype = document.getElementById(ddljobtype);
             //var ddljobtype = document.getElementById('ddlJobTypes');
             //var ddljobtype = document.getElementById('<%=ddlJobTypes.ClientID%>').value;
             var ss = document.getElementById('<%=ddlJobTypes.ClientID%>').value;
            
             if (ss== '0') {

                 alert('Please Select Job Type ', 'Warning!');

                 $(ddljobtype).focus();
                 return false;
             }
           
             var txtCurrency = $("[id$=txtJobRoleName]").attr("id");
             var txtCurrency = document.getElementById(txtCurrency);
             if (txtCurrency.value.length == 0) {
                 
                 alert('Please Enter Job Role Name ', 'Warning!');
                 
                 $(txtCurrency).focus();
                 return false;
             }  
            
         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitJobRole').click(function () {
                     validateJobRole();
                 });
             });
         });



         //$(function () {
         //    debugger
         //    $("[id*=btnSubmitJobRole]").click(function () {
         //        var ddlFruits = $("[id*=ddlJobTypes]");
         //        if (ddlFruits.val() == 0) {
         //            //If the "Please Select" option is selected display error.
         //            alert("Please select Job Type!");
         //            return false;
         //        }
         //        return true;
         //    });
         //});

        </script>
    <%-- Job Role Script Ends Here--%>
       
     <script>
         function SetRound(val) {
             $('#chkStatusround').prop('checked', val);
         }

         function validateRound() {

             $('#hfRounds').val($('#chkStatusround').prop('checked'));

             var txtRound = $("[id$=txtRound]").attr("id");
             var txtRound = document.getElementById(txtRound);

             if (txtRound.value.length == 0) {
                 alert('Please Enter Rounds.', 'Warning!');
                 //   $(txtCurrency).css('border-color', 'red');
                 $(txtRound).focus();
                 return false;
             }
         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitround').click(function () {
                     validate();
                 });
             });
         });
        </script>


     <script>
         function SetIntervals(val) {
             $('#chkStatusIntervals').prop('checked', val);
         }

         function validateinterval() {

             $('#hfIntervals').val($('#chkStatusIntervals').prop('checked'));

             var txtIntervals = $("[id$=txtIntervals]").attr("id");
             var txtIntervals = document.getElementById(txtIntervals);

             if (txtIntervals.value.length == 0) {
                 alert('Please Enter Intervals ', 'Warning!');
                 //   $(txtCurrency).css('border-color', 'red');
                 $(txtIntervals).focus();
                 return false;
             }
         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSubmitIntervals').click(function () {
                     validateint();
                 });
             });
         });
        </script>
   <%-- //-------------start-----------//--%>

    <script>
        function SetSkills(val) {
            $('#chkStatusSkills').prop('checked', val);
        }

        function validateskill() {

            $('#hfSkills').val($('#chkStatusSkills').prop('checked'));

            var txtSkill = $("[id$=txtSkills]").attr("id");
            var txtSkill = document.getElementById(txtSkill);

            if (txtSkill.value.length == 0) {
                alert('Please Enter Skills ', 'Warning!');
                //   $(txtCurrency).css('border-color', 'red');
                $(txtSkill).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSkill').click(function () {
                    validateint();
                });
            });
        });
        </script>

    <%--//-------------end----------------//--%>

     <%-- //-------------start-LEVEL----------//--%>

    <script>
        function SetLevels(val) {
            $('#checklevel').prop('checked', val);
        }

        function validatelevel() {

            $('#hfLevel').val($('#checklevel').prop('checked'));

            var txtlevel = $("[id$=txtlevel]").attr("id");
            var txtlevel = document.getElementById(txtlevel);

            if (txtlevel.value.length == 0) {
                alert('Please Enter Level ', 'Warning!');
                //   $(txtCurrency).css('border-color', 'red');
                $(txtlevel).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnlevelSubmit').click(function () {
                    validateint();
                });
            });
        });
        </script>

    <%--//-------------end----------------//--%>

     <%-- //-------------start-language----------//--%>

    <script>
        function SetLanguage(val) {
            $('#checklanguage').prop('checked', val);
        }

        function validatelanguage() {

            $('#hflanguage').val($('#checklanguage').prop('checked'));

            var txtlanguage = $("[id$=txtlanguage]").attr("id");
            var txtlanguage = document.getElementById(txtlanguage);

            if (txtlanguage.value.length == 0) {
                alert('Please Enter Language ', 'Warning!');
                //   $(txtCurrency).css('border-color', 'red');
                $(txtlanguage).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnlanguage').click(function () {
                    validateint();
                });
            });
        });
        </script>

    <%--//-------------end----------------//--%>

     <%-- //-------------start-Proficiency----------//--%>

    <script>
        function SetProficiency(val) {
            $('#chProficiency').prop('checked', val);
        }

        function validateProficiency() {

            $('#hfProficiency').val($('#chProficiency').prop('checked'));

            var txtProficiency = $("[id$=txtProficiency]").attr("id");
            var txtProficiency = document.getElementById(txtProficiency);

            if (txtProficiency.value.length == 0) {
                alert('Please Enter Proficiency ', 'Warning!');
                //   $(txtCurrency).css('border-color', 'red');
                $(txtProficiency).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnProficiencySubmit').click(function () {
                    validateint();
                });
            });
        });
        </script>

    <%--//-------------end----------------//--%>

     <%-- //-------------start-Work Area----------//--%>


     <script>
         function SetWorkArea(val) {
             $('#chworkarea').prop('checked', val);
         }

         function validateWorkArea() {

             $('#hfWorkArea').val($('#chworkarea').prop('checked'));

             var txtWorkArea = $("[id$=txtWorkArea]").attr("id");
             var txtWorkArea = document.getElementById(txtWorkArea);

             if (txtWorkArea.value.length == 0) {
                 alert('Please Enter Work Area ', 'Warning!');
                 //   $(txtCurrency).css('border-color', 'red');
                 $(txtWorkArea).focus();
                 return false;
             }
         }

         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnWorkAresSubmit').click(function () {
                     validateint();
                 });
             });
         });
        </script>

   <%-- <script>
        function SetWorkArea(val) {
            $('#chworkarea').prop('checked', val);
        }

        function validateWorkArea() {

            $('#hfWorkArea').val($('#chworkarea').prop('checked'));

            var txtWorkArea = $("[id$=txtWorkArea]").attr("id");
            var txtWorkArea = document.getElementById(txtWorkArea);

            if (txtWorkArea.value.length == 0) {
                alert('Please Enter Work Area ', 'Warning!');
                //   $(txtCurrency).css('border-color', 'red');
                $(txtWorkArea).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnWorkAresSubmit').click(function () {
                    validateint();
                });
            });
        });
        </script>--%>

    <%--//-------------end----------------//--%>

      <%-- //-------------start-Category----------//--%>

    <script>
        function SetCategory(val) {
            $('#chCategory').prop('checked', val);
        }

        function validateCategory() {

            $('#hfCategory').val($('#chCategory').prop('checked'));

            var txtCategory = $("[id$=txtCategory]").attr("id");
            var txtCategory = document.getElementById(txtCategory);

            if (txtCategory.value.length == 0) {
                alert('Please Enter Category ', 'Warning!');
                //   $(txtCurrency).css('border-color', 'red');
                $(txtCategory).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitCategory').click(function () {
                    validateint();
                });
            });
        });
        </script>

    <%--//-------------end----------------//--%>

     <%-- //-------------start-Exam----------//--%>

    <script>
        function SetExam(val) {
            $('#chExam').prop('checked', val);
        }

        function validateExam() {

            $('#hfExam').val($('#chExam').prop('checked'));

            var txtExam = $("[id$=txtExam]").attr("id");
            var txtExam = document.getElementById(txtExam);

            if (txtExam.value.length == 0) {
                alert('Please Enter Exam ', 'Warning!');
                //   $(txtCurrency).css('border-color', 'red');
                $(txtExam).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnExamSubmit').click(function () {
                    validateint();
                });
            });
        });
        </script>

    <%--//-------------end----------------//--%>

    <script>
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>
    <script>
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>
    
</asp:Content>

