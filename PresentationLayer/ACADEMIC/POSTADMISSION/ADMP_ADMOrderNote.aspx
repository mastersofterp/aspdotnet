<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMP_ADMOrderNote.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMP_ADMOrderNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_btnShow {
            z-index: 0;
        }

        .switch-field {
            font-family: "Lucida Grande", Tahoma, Verdana, sans-serif;
            padding: 10px;
            overflow: hidden;
        }

        .switch-title {
            margin-bottom: 6px;
        }

        .switch-field input {
            position: absolute !important;
            clip: rect(0, 0, 0, 0);
            height: 1px;
            width: 1px;
            border: 0;
            overflow: hidden;
        }

        .switch-field label {
            float: left;
        }

        .switch-field label {
            display: inline-block;
            width: 80px;
            background-color: #e4e4e4;
            color: rgba(0, 0, 0, 0.6);
            font-size: 14px;
            font-weight: normal;
            text-align: center;
            text-shadow: none;
            padding: 6px 14px;
            border: 1px solid rgba(0, 0, 0, 0.2);
            -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.3), 0 1px rgba(255, 255, 255, 0.1);
            box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.3), 0 1px rgba(255, 255, 255, 0.1);
            -webkit-transition: all 0.1s ease-in-out;
            -moz-transition: all 0.1s ease-in-out;
            -ms-transition: all 0.1s ease-in-out;
            -o-transition: all 0.1s ease-in-out;
            transition: all 0.1s ease-in-out;
        }

            .switch-field label:hover {
                cursor: pointer;
            }

        .switch-field input:checked + label {
            background-color: #A5DC86;
            -webkit-box-shadow: none;
            box-shadow: none;
        }

        .switch-field label:first-of-type {
            border-radius: 4px 0 0 4px;
        }

        .switch-field label:last-of-type {
            border-radius: 0 4px 4px 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title" style="text-transform: uppercase">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                             <li class="nav-item"><a class="nav-link active" href="#tab_1" data-toggle="tab">Admission Note</a></li>
                            <li class="nav-item"><a class="nav-link" href="#tab_3" data-toggle="tab">Admission Order</a></li>
                            <%--  <li><a href="#tab_2" data-toggle="tab">Bulk Student</a></li>--%>
                           

                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane" id="tab_3">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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

                                <asp:UpdatePanel ID="upAdmOrder" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">                                   
                                            <div class="col-12 mt-4">
                                                <div class="row" id="SingleStudOD" runat="server">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                           <sup>* </sup>
                                            <label>Admission Batch </label>
                                                        </div>

                                               <asp:DropDownList ID="ddlAdmissionBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                               <sup>* </sup>
                                            <label>Program Type </label>
                                                        </div>
                                                         <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">UG</asp:ListItem>
                                            <asp:ListItem Value="2">PG</asp:ListItem>
                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                               <sup>* </sup>
                                            <label>Degree </label>
                                                        </div>
                                                       <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                            <label>Program/Branch </label>
                                                        </div>
                                                         <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" ></asp:ListBox>
                                    </div>
                                      <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student List" TabIndex="7" CssClass="btn btn-primary"  OnClick="btnShow_Click" />
                                            <asp:Button ID="btnOrderGen" runat="server" Text="Generate Admission Order" TabIndex="7" OnClientClick="return Navigate()" CssClass="btn btn-primary" OnClick="btnOrderGen_Click"  />
                            </div>
                                                </div>
                                                  <asp:Panel runat="server" ID="pnlCount" Width="100%" Visible="False">
                                 <div class="col-12">
                                <%--<div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">                                            
                                            <label style="color: red">Total Students </label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtTotalCount" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                    </div>
                            
                                <div class="form-group form-inline col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label style="color: red">Generated Admit Card </label>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtGeneratedAdmitCard" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                </div>
                                <div class="form-group form-inline col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label style="color: red">Not generated Admit Card </label>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtNotGeneratedAdmitCard" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                </div>
                                    </div>--%>
                                     </div>
                            </asp:Panel>
                              <asp:Panel runat="server" ID="pnllistView"  Width="100%"    Visible="False">
                            <div class="col-md-12">
                                <asp:ListView ID="lvSchedule" runat="server" Visible="true">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <%--<table id="tbllist" class="dataTable table table-bordered table-striped table-hover" style="width: 100%">--%>
                                            <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table id="tblADMOrder" class="table table-striped table-bordered nowrap" style="width: 100%;" id="tbllist">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th><asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" /></th>
                                                            <th>Admission Batch</th>
                                                            <th>Application NO</th>
                                                            <th>Student Name</th>
                                                            <th>Degree</th>
                                                            <th>Program/Branch</th>
                                                            <th>Email Id</th>
                                                            <%-- <th style="width: 10%">Status</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkRecon" runat="server" ToolTip='<%# Eval("USERNO")%>' />
                                            </td>
                                            <td><%# Eval("ADMBATCH") %></td>
                                              <td> <asp:Label ID="lblApplicationNo" runat="server" Text='<%# Eval("APPLICATION_ID") %>'></asp:Label></td>
                                         <%--   <td><%# Eval("APPLICATION_ID") %></td>--%>
                                            <%--<td><%# Eval("STUDENTNAME") %></td>--%>
                                            <td> <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDENTNAME") %>'></asp:Label></td>
                                            <td><%# Eval("DEGREENAME") %></td>
                                            <td><%# Eval("BRANCHNAME") %></td>                                           
                                           <td> <asp:Label ID="lblMail" runat="server" Text='<%# Eval("EmailId") %>'></asp:Label></td>
                                            <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />

                                            
                                           
                                            
                                                <%--   <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?"Active":"In Active" %>' ForeColor='<%#Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            </asp:Panel> 
                                            </div>
                                        </div>
                                    </ContentTemplate>                                    
                                </asp:UpdatePanel>
                            </div>
                            <div id="divMsg" runat="Server"></div>

                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">                                   
                                            <div class="col-12 mt-4">
                                                <div class="row" id="Div2" runat="server">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                           <sup>* </sup>
                                            <label>Admission Batch </label>
                                                        </div>

                                               <asp:DropDownList ID="ddlAdmBatchNote" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                               <sup>* </sup>
                                            <label>Program Type </label>
                                                        </div>
                                                         <asp:DropDownList ID="ddlProgramTypeNote" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlProgramTypeNote_SelectedIndexChanged" AutoPostBack="true" >
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">UG</asp:ListItem>
                                            <asp:ListItem Value="2">PG</asp:ListItem>
                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                               <sup>* </sup>
                                            <label>Degree </label>
                                                        </div>
                                                       <asp:DropDownList ID="ddlDegreeNote" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlDegreeNote_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                            <label>Program/Branch </label>
                                                        </div>
                                                         <asp:ListBox ID="lstProgramNote" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" ></asp:ListBox>
                                    </div>
                                      <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowNote" runat="server" Text="Show Student List" TabIndex="7" CssClass="btn btn-primary"  OnClick="btnShowNote_Click" />


<%--                                          <asp:UpdatePanel ID="updtBtnPrin" runat="server">
                                              <ContentTemplate>--%>
                                         <asp:Button ID="btnAdmNote" runat="server" Text="Print Admission Note" TabIndex="7" CssClass="btn btn-primary" OnClick="btnAdmNote_Click"  />
                                         <%--     </ContentTemplate>
                                              <Triggers>
                                                  <asp:PostBackTrigger ControlID="btnAdmNote" />
                                              </Triggers>
                                                  </asp:UpdatePanel>--%>
                                          
                            </div>
                                                </div>
                                                  <asp:Panel runat="server" ID="Panel1" Width="100%" Visible="False">
                                 <div class="col-12">                              
                                     </div>
                            </asp:Panel>
                              <asp:Panel runat="server" ID="pnllistViewNote"  Width="100%"    Visible="False">
                            <div class="col-md-12">
                                <asp:ListView ID="lvScheduleNote" runat="server" Visible="true">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <%--<table id="tbllist" class="dataTable table table-bordered table-striped table-hover" style="width: 100%">--%>
                                            <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tbllist">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th><asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" /></th>
                                                            <th>Admission Batch</th>
                                                            <th>Application NO</th>
                                                            <th>Student Name</th>
                                                            <th>Degree</th>
                                                            <th>Program/Branch</th>
                                                            <th>Email Id</th>
                                                            <%-- <th style="width: 10%">Status</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkRecon" runat="server" ToolTip='<%# Eval("USERNO")%>' />
                                            </td>
                                            <td><%# Eval("ADMBATCH") %></td>
                                            <td><%# Eval("APPLICATION_ID") %></td>
                                            <%--<td><%# Eval("STUDENTNAME") %></td>--%>
                                            <td> <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDENTNAME") %>'></asp:Label></td>
                                            <td><%# Eval("DEGREENAME") %></td>
                                            <td><%# Eval("BRANCHNAME") %></td>                                           
                                           <td> <asp:Label ID="lblMail" runat="server" Text='<%# Eval("EmailId") %>'></asp:Label></td>
                                            <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />

                                            
                                           
                                            
                                                <%--   <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?"Active":"In Active" %>' ForeColor='<%#Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            </asp:Panel> 
                                            </div>
                                        </div>
                                    </ContentTemplate>    
                                     <Triggers>
                                                  <asp:PostBackTrigger ControlID="btnAdmNote" />
                                              </Triggers>                                
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal1" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">

                    <h4 class="modal-title">Leave Details <i class="fa fa-info-circle"></i>
                    </h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
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

                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Regno :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRegno" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Leave Type:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblLeaveType" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Leave Status :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="leaveStatus" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudname" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Leave From Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblfromDate" runat="server" Font-Bold="True" />
                                            </a>

                                        </li>
                                        <li class="list-group-item"><b>Leave Details :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblLeaveDetail" runat="server" Text=""></asp:Label>
                                                <asp:HiddenField runat="server" ID="hdnLeaveNo" Value="0" />
                                                <asp:HiddenField runat="server" ID="hdnODType" Value="0" Visible="false" />
                                            </a>
                                        </li>

                                    </ul>
                                </div>

                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>OD Type :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblODType" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Leave to Date:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblToDate" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Slots:</b>
                                            <a class="sub-label">
                                                <asp:CheckBoxList ID="chkSelectdSlots"  CssClass="col-lg-12" RepeatDirection="Horizontal" RepeatColumns="3" runat="server">
                                                </asp:CheckBoxList>
                                            </a>
                                        </li>

                                    </ul>
                                </div>


                                <div class="col-12">
                                    <div class="switch-field">

                                        <div class="switch-title">Mark Leave as :</div>
                                        <input type="radio" id="switch_left" name="Status" value="1" />
                                        <label for="switch_left">Approve</label>
                                        <input type="radio" id="switch_right" name="Status" value="2" />
                                        <label for="switch_right">Reject</label>
                                    </div>
                                    <asp:Label ID="ODCountFlag" runat="server" Visible="false" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmitStatus" runat="server" Text="Submit"  CssClass="btn btn-primary" />
                                <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-warning"
                                    data-dismiss="modal" />

                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <div class="modal fade" id="myModal2" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="box-body modal-warning">
                            <div class="form-group" style="text-align: center">
                                <asp:Label ID="lblmessageShow" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-warning"
                                        data-dismiss="modal" />
                                </p>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>



    <script type="text/javascript">
        function selectfromdate(sender, args) {
        }
        function ConfirmSubmit() {
            var ret = confirm('Are you sure to delete this Leave entry?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function totAllSubjects(headchk) {
            debugger;
            var sum = 0;
            var frm = document.forms[0]
            try {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            if (e.disabled == false) {
                                e.checked = true;
                            }
                        }
                        else {
                            if (e.disabled == false) {
                                e.checked = false;
                                headchk.checked = false;
                            }
                        }
                    }

                }
            }
            catch (err) {
                alert("Error : " + err.message);
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function SelectAll(chk) {
            for (i = 0; i < hftot.value; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_chkSelect');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }
        function SetCheckBox(value) {
            $("input:radio[value=" + value + "]").attr("checked", true);
        }

    </script>
     <script type="text/javascript">
         function Navigate() {
             debugger

             var value = 0;
     

             $("#tblADMOrder input[type=checkbox]:checked").each(function () {
                 value++;
             });

             if (value == 0) {
                 alert("Please select at least one record.");
                return false;
             }
             else {

                 return true;
             }

             //$('#tblADMOrder tr').each(function () {
             //    $(this).find('td').each(function () {

             //        var checked = $("#" + rowId + " input:checkbox")[0].checked;
             //        //do your stuff, you can use $(this) to get current cell
             //    })
             //})

             //for (var i = 1; i < totalcolumns - 1; i++) {
             //    if()
             //    cellvalue = parseInt(row.find("td:eq(" + i + ") input[type='checkbox']").checked);

             //    if (isNaN(cellvalue)) { cellvalue = 0; }
             //    _value = parseInt(_value) + cellvalue;
             //}
             //row.find("td:eq(" + (totalcolumns - 1) + ") input[type='text']").val(_value);


          
             //var count_checked = $("#tbllist").find("input:checkbox").attr("checked", "checked").length; // count the checked rows
             //if (count_checked == 0) {

             //    alert("Please select at least one recored.");
             //    return false;
             //}
             //else {
             //    return true;
             //}
             //if (count_checked == 1) {
             //    alert("Record Selected:" + count_checked);

             //} else {
             //    alert("Record Selected:" + count_checked);
             //    $("#tbllist").find("input:checkbox").attr("checked", "checked");
             //}
         }

    </script>

    <script>
        function OpenPreview() {
            //$("#pnlPopup1").hide();
            //$('#pnlPopup1').modal('toggle');
            //alert('hi');
            $('#myModal1').modal('show');
        }
    </script>
     <script type="text/javascript">
         $(document).ready(function () {
             $('.multi-select-demo').multiselect({
                 includeSelectAllOption: true,
                 maxHeight: 200,
                 enableFiltering: true,
                 filterPlaceholder: 'Search',
                 enableCaseInsensitiveFiltering: true,
             });
         });
         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $(document).ready(function () {
                 $('.multi-select-demo').multiselect({
                     includeSelectAllOption: true,
                     maxHeight: 200,
                     enableFiltering: true,
                     filterPlaceholder: 'Search',
                     enableCaseInsensitiveFiltering: true,
                 });
             });
         });
    </script>
</asp:Content>

