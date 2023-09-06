<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPGenerateCallLetter.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPGenerateCallLetter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <asp:UpdatePanel ID="updGenerateCallLetter" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfdShowStatus" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnScheduleNo" runat="server" Value="0" />
            <div class="row">
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
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Generate Call Letter</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
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
                                        <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged">
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
                                        <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged"></asp:ListBox>
                                    </div>                                                        
                                </div>
                            </div>
                        </div>

                           <div class="col-12 btn-footer">
                                 <asp:Button ID="btnShow" runat="server" Text="Show Student List" TabIndex="5" CssClass="btn btn-primary" OnClick="btnShow_Click" />  
                                 <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="7" CssClass="btn btn-primary" OnClientClick="return validate();" OnClick="btnSubmit_Click"/>
                                 <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" TabIndex="7" CssClass="btn btn-primary" OnClick="btnSendMail_Click" Visible="false" />
                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" CssClass="btn btn-warning"  OnClick="btnCancel_Click"/>                                                   
                            </div>

                         <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date </label>
                                        </div>
                                        <asp:TextBox ID="txtScheduleDate" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                            TargetControlID="txtScheduleDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Time </label>
                                        </div>
                                        <asp:TextBox ID="txtLoginTime" runat="server" CssClass="form-control" TabIndex="3" OnTextChanged="txtLoginTime_TextChanged" AutoPostBack="true"></asp:TextBox>
                                         <ajaxToolKit:MaskedEditExtender ID="mskDateTime1" runat="server"
                                            Mask="99:99" MaskType="Time" TargetControlID="txtLoginTime"                                        >
                                        </ajaxToolKit:MaskedEditExtender>
                                        <asp:CheckBox ID="chkTime" runat="server" AutoPostBack="True" Checked="true" OnCheckedChanged="chkTime_CheckedChanged" 
                                            Text="AM" />
                                    </div>  
                                    </div>
                                </div>
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
                                                    <th>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" />
                                                    </th>
                                                    <th style="width: 5%">Admission Batch</th>
                                                    <th style="width: 10%">Student Name</th>
                                                    <th style="width: 10%">Email Id</th>
                                                     <th style="width: 20%">Degree Name</th>
                                                    <th style="width: 20%">Program/Branch</th>
                                                    <th style="width: 15%">Date</th>
                                                    <th style="width: 10%">Time</th>
                                                  <%--  <th style="width: 10%">Print</th>--%>
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
                                        <asp:CheckBox ID="chkCallLrt" runat="server"  CssClass="check__box"/>
                                            <asp:HiddenField ID="hdnAdmBatch" runat="server"  Value='<%#Eval("BATCHNO")%>' />
                                            <asp:HiddenField ID="hdnDegreeNo" runat="server"  Value='<%# Eval("DEGREENO") %>' />
                                            <asp:HiddenField ID="hdnBranchNo" runat="server"  Value='<%# Eval("BRANCHNO") %>' />
                                            <asp:HiddenField ID="hdnUserNo" runat="server"  Value='<%# Eval("USERNO") %>'/>
                                        </td>                          
                                        <td><%# Eval("ADMBATCH") %></td>
                                        <td><%# Eval("STUDENTNAME") %></td>
                                        <td><%# Eval("EMAILID") %></td>
                                        <td><%# Eval("DEGREENAME") %></td>
                                        <td><%# Eval("BRANCHNAME") %></td>

                                        <td><%# Eval("CALLDATE") %></td>
                                        <td><%# Eval("CallTime") %></td>         
                                        <%--<td>
                                            <asp:LinkButton ID="lnkPrint" runat="server">Print</asp:LinkButton>
                                        </td>--%>                               
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
        function totAllSubjects(headchk) {

            if (headchk.checked == true) {
                //$("[id$=chkCallLrt]").attr('checked', this.checked);
                //$("[id*=lvSchedule] [id*=chkCallLrt]").attr("checked", "checked");

                $("#tbllist").find("input:checkbox").attr("checked", "checked");
          
            }
            else {
                $("#tbllist").find("input:checkbox").attr("checked", false);
            }

            //var sum = 0;
            //var frm = document.forms[0]
            //try {
            //    for (i = 0; i < document.forms[0].elements.length; i++) {
            //        var e = frm.elements[i];
            //        if (e.name == 'ctl00$ContentPlaceHolder1$lvSchedule$ctrl1$chkCallLrt') {
            //            if (headchk.checked == true) {                            
            //                if (e.disabled == false) {
            //                    e.checked = true;
            //                }
            //            }
            //            else {
            //                if (e.disabled == false) {
            //                    e.checked = false;
            //                    headchk.checked = false;
            //                }
            //            }

            //        }

            //    }
            //}
            //catch (err) {
            //    alert("Error : " + err.message);
            //}
        }
    </script>
</asp:Content>

