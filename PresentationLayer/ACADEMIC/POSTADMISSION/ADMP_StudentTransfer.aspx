﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMP_StudentTransfer.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMP_StudentTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <asp:UpdatePanel ID="upAttendance" runat="server">
        <ContentTemplate>
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
<%--                                        <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>--%>

                                      <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6"></asp:ListBox>

                                    </div>                               

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student" TabIndex="6" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="7" OnClick="btnSubmit_Click" CssClass="btn btn-primary"  />         
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            </div>
                            <asp:Panel runat="server" ID="pnlCount" Width="100%" Visible="False">
                                <div class="form-group form-inline col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic" >
                                        <label style="color: red" id="lbltotalstudent">Total Students :- </label>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtTotalCount" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                </div>
                            </asp:Panel>
                                
                            <div class="col-md-12">
                               <asp:Panel ID="pnllvSh" runat="server" Visible="false">
                                <asp:ListView ID="lvSchedule" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tbllist">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th><asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" /></th>
                                                            <th>Admission Batch</th>
                                                            <th>Application No</th>
                                                            <th>Student Name</th>
                                                            <th>Degree</th>
                                                            <th>Program/Branch</th>                                                           
                                                            <th>Transfer STATUS</th>

                                                            <%-- <th style="width: 10%">Status</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>                                            
                                               <asp:CheckBox ID="chkRecon" runat="server"  Checked='<%#  Convert.ToInt32(Eval("StudentTransfer"))==1 ? true : false %>' Enabled='<%#  Convert.ToInt32(Eval("StudentTransfer"))==1 ? false : true %>'   />
                                            </td>
                                            <td><%# Eval("ADMBATCH") %></td>
                                            <td><%# Eval("APPLICATION_ID") %></td>
                                            <td><%# Eval("STUDENTNAME") %></td>
                                            <td><%# Eval("DEGREENAME") %></td>
                                            <td><%# Eval("BRANCHNAME") %></td> 
                                            <td> <asp:Label ID="lblStatus" runat="server" Text='<%#  Convert.ToInt32(Eval("StudentTransfer"))==1 ? "Transferred" : "Pending" %>'                                              
                                               ForeColor='<%# Convert.ToInt32(Eval("StudentTransfer"))==1 ?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>

                                            <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />
                                            <asp:HiddenField ID="hdnDegreeNo" Visible="false" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                            <asp:HiddenField ID="hdnBranchNo" Visible="false" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                            
                                        <%--    <asp:HiddenField ID="hdnACNO" Visible="false" runat="server" Value='<%# Eval("ACNO") %>' />
                                            <asp:HiddenField ID="hdnAttendanceNO" Visible="false" runat="server" Value='<%# Eval("ATTENDANCE_NO") %>' />
                                           <td> <asp:Label ID="lblStatus" runat="server" Text='<%#  Convert.ToString(Eval("IsAttend"))==""? "-" : Convert.ToString(Eval("IsAttend"))=="True"? "Present": "Absent" %>'                                              
                                               ForeColor='<%# Convert.ToString(Eval("IsAttend"))=="True"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>    --%>                                     
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
               
            </Triggers>
    </asp:UpdatePanel>
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

