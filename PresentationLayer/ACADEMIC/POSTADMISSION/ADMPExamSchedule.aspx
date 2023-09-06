<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPExamSchedule.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ExamSchedule" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
   
    <asp:UpdatePanel ID="updSchedule" runat="server">
        <ContentTemplate>          
            <asp:HiddenField ID="hfdShowStatus" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnScheduleNo" runat="server" Value="0" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
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
                        <div class="box-header with-border">
                           <%-- <h3 class="box-title">Exam Schedule</h3>--%>
                             <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                          <%--  <label>Admission Batch </label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>    
                                        </div>
                                        <asp:DropDownList ID="ddlAdmissionBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                           <%-- <label>Date </label>--%>
                                            <asp:Label ID="lblDYScheduledDate" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtScheduleDate" runat="server"  TabIndex="2" OnTextChanged="txtScheduleDate_TextChanged"  AutoPostBack="true"  onkeydown="javascript:preventInput(event);"></asp:TextBox>
                                   
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" 
                                            TargetControlID="txtScheduleDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                       <%-- <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtScheduleDate"
                                                Mask="99-99-9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                              <%--<label>Start Time </label>--%>
                                              <asp:Label ID="lblDYStartTime" runat="server" Font-Bold="true"></asp:Label>                                         
                                        </div>
                                        <asp:TextBox ID="txtLoginTime" runat="server" CssClass="form-control" TabIndex="3"  OnTextChanged="txtLoginTime_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="mskDateTime1" runat="server"
                                            Mask="99:99" MaskType="Time" TargetControlID="txtLoginTime"                                        >
                                        </ajaxToolKit:MaskedEditExtender>
                                        <asp:CheckBox ID="chkTime" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chkTime_CheckedChanged"
                                            Text="AM" />
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12" id="divEndTime" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>End Time </label>--%>
                                             <asp:Label ID="lblDYEndTime" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control" TabIndex="3"  AutoPostBack="true" OnTextChanged="txtEndTime_TextChanged"  ></asp:TextBox>
                                         <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                            Mask="99:99" MaskType="Time" TargetControlID="txtEndTime"                                        >
                                        </ajaxToolKit:MaskedEditExtender>
                                        <asp:CheckBox ID="chkEndTime" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chkEndTime_CheckedChanged"
                                            Text="AM" />
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                           <%-- <label>Reporting Time </label>--%>
                                            <asp:Label ID="lblDYReportingTime" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtReportingTime" runat="server" CssClass="form-control" TabIndex="3"  OnTextChanged="txtReportingTime_TextChanged"  AutoPostBack="true" ></asp:TextBox>
                                          <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                            Mask="99:99" MaskType="Time" TargetControlID="txtReportingTime"                                        >
                                        </ajaxToolKit:MaskedEditExtender>
                                        <asp:CheckBox ID="chkReportingTime" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chkReportingTime_CheckedChanged"
                                            Text="AM" />
                                    </div>                                  
                                </div>

                                <asp:Panel ID="pnlDegree" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Degree </label>--%> 	
                                                  <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" 
                                                TabIndex="5" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Program/Branch </label>--%> 
                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6"></asp:ListBox>
                                        </div>                                          
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlCenter" runat="server" Visible="false">
                                     <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Place </label>--%>  	
                                            <asp:Label ID="lblDYPlace" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Address </label>--%> 	
                                              <asp:Label ID="lblDYAddress" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>MODE </label>--%>
                                             <asp:Label ID="lblDYMode" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlMode" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                             <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">OFFLINE</asp:ListItem>
                                            <asp:ListItem Value="1">ONLINE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                      </div>
                                </asp:Panel>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">                                           
                                            <%--<label>Status</label>--%>
                                            <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="7" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return validate();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" CssClass="btn btn-warning"  OnClick="btnCancel_Click"/>
                        </div>

                        <%--   <div class="col-12">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit
                                    </th>
                                    <th>Admission Batch </th>
                                    <th>Degree</th>
                                    <th>Program/Branch</th>
                                    <th>Date</th>
                                    <th>Login Time</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <img src="Images/edit.png" /></td>
                                    <td>2022-2023</td>
                                    <td>BSC</td>
                                    <td>Computer Science, IT</td>
                                    <td>28-10-2022</td>
                                    <td>16:00</td>
                                    <td style="color:green">Active</td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="Images/edit.png" /></td>
                                    <td>2022-2023</td>
                                    <td>BSC</td>
                                    <td>Computer Science, IT</td>
                                    <td>28-10-2022</td>
                                    <td>16:00</td>
                                    <td style="color:green">Active</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>
                        <asp:Panel ID="pnlGV1" runat="server" Visible="false">
                        <div class="col-md-12">
                            <asp:ListView ID="lvSchedule" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <table id="tbllist" class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">Edit</th>
                                                    <th style="width: 10%">Admission Batch</th>
                                                    <th style="width: 10%">Degree</th>
                                                    <th style="width: 20%">Program/Branch</th>
                                                    <th style="width: 15%">Scheduled Date</th>
                                                    <th style="width: 10%">Login Time</th>
                                                    <th style="width: 10%">Reporting Time</th>
                                                    <th style="width: 10%">Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SCHEDULE_NO") %>' ImageUrl="~/Images/edit.png" TabIndex="8" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td><%# Eval("ADMBATCH") %></td>
                                        <td><%# Eval("DEGREENAME") %></td>
                                        <td><%# Eval("BRANCHNAME") %></td>
                                        <td><%# Eval("SCHD_DATE") %></td>
                                        <td><%# Eval("SCHD_TIME") %></td>
                                         <td><%# Eval("REPORTING_TIME") %></td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?"Active":"In Active" %>' ForeColor='<%#Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                            </asp:Panel>

                         <asp:Panel ID="pnlGV2" runat="server" Visible="false">
                        <div class="col-md-12">
                            <asp:ListView ID="lsSchedule2" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <table id="tbllist" class="dataTable table table-bordered table-striped table-hover" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">Edit</th>
                                                    <th style="width: 10%">Admission Batch</th>
                                                    <th style="width: 10%">Place</th>
                                                    <th style="width: 10%">Address</th>
                                                    <th style="width: 10%">Exam Mode</th>                                                
                                                    <th style="width: 10%">Scheduled Date</th>
                                                    <th style="width: 15%">From - To Slot</th>
                                                    <th style="width: 10%">Reporting Time</th>
                                                    <th style="width: 10%">Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("SCHEDULE_NO") %>' ImageUrl="~/Images/edit.png" TabIndex="8" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td><%# Eval("ADMBATCH") %></td>
                                        <td><%# Eval("CENTRE_NAME") %></td>
                                        <td><%# Eval("CENTRE_ADDRESS") %></td>
                                        <td>
                                            <asp:Label ID="lblExamMode" runat="server" Text='<%# Convert.ToInt32(Eval("EXAM_MODE"))==1?"ONLINE":"OFFLINE" %>' ForeColor='<%#Convert.ToInt32(Eval("EXAM_MODE"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>
                                        <td><%# Eval("SCHD_DATE") %></td>
                                        <td><%# Eval("SCHD_TIME") %> - <%# Eval("END_TIME") %></td>
                                        <td><%# Eval("REPORTING_TIME") %></td>
                                        <td>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?"Active":"In Active" %>' ForeColor='<%#Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>
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
    </asp:UpdatePanel>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {
            // alert('Hii');
            $('#hfdShowStatus').val($('#rdActive').prop('checked'));
            //alert($('#hfdShowStatus').val());
            //if (Page_ClientValidate()) {
            //    alert('Inside');
            //    //$('#hfdShowStatus').val($('#rdActive').prop('checked'));
            //    alert($('#hfdShowStatus').val());
            //}
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
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
    <script type="text/javascript">
        function preventInput(evnt) {

            if (evnt.which != 9) evnt.preventDefault();

        }

</script>  
</asp:Content>

