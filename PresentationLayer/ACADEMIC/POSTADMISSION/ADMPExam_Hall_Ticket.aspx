<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPExam_Hall_Ticket.aspx.cs" Inherits="Exam_Hall_Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <asp:updatepanel id="upExamTimeTable" runat="server">
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
                            <h3 class="box-title">Exam Hall Ticket</h3>
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
                                        <%-- <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                                        <asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                    </div>
                                    <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Exam Slot </label>
                                </div>
                                <asp:DropDownList ID="ddlExamSlot" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                   <asp:ListItem Value="1"> Phase1</asp:ListItem>                                  
                                </asp:DropDownList>
                            </div>--%>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show Student List" TabIndex="7" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Schedule </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamSchedule" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="6" Enabled="false" OnSelectedIndexChanged="ddlExamSchedule_SelectedIndexChanged" AutoPostBack="true">
                                            <%-- <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">28-10-20222 09:00 AM</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12 btn-footer mt-3">
                                        <asp:Button ID="btnAdmitCard" runat="server" Text="Generate Admit Card" TabIndex="8" CssClass="btn btn-primary" OnClick="btnAdmitCard_Click" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print Admit Card"  TabIndex="9" CssClass="btn btn-primary" OnClick="btnPrint_Click"  />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>

                            <%-- <div class="col-12 mt-3">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit
                                    </th>
                                    <th>Admission Batch </th>
                                    <th>Degree</th>
                                    <th>Program/Branch</th>
                                    <th>Student Name</th>
                                    <th>Application ID</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <img src="Images/edit.png" /></td>
                                    <td>2022-2023</td>
                                    <td>BSC</td>
                                    <td>Computer Science, IT</td>
                                    <td>Rahul Roy</td>
                                    <td>ID123456789</td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="Images/edit.png" /></td>
                                    <td>2022-2023</td>
                                    <td>BSC</td>
                                    <td>Computer Science, IT</td>
                                    <td>Rahul Roy</td>
                                    <td>ID123456789</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>
                            <asp:Panel runat="server" ID="pnlCount" Width="100%" Visible="False">
                                 <div class="col-12">
                                <div class="row">
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
                                    </div>
                                     </div>
                            </asp:Panel>
                              <asp:Panel runat="server" ID="pnllistView"  Width="100%"    Visible="False">
                            <div class="col-md-12">
                                <asp:ListView ID="lvSchedule" runat="server" Visible="true">
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
                                                            <th>Shedule Slot</th>
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
                                                <asp:CheckBox ID="chkRecon" runat="server" Checked='<%#(Convert.ToString(Eval("GENERATE_STATUS"))=="1" ? true : false) %>' Enabled='<%#(Convert.ToString(Eval("GENERATE_STATUS"))=="1" ? false : true) %>' ToolTip='<%# Eval("USERNO")%>' />
                                            </td>
                                            <td><%# Eval("ADMBATCH") %></td>
                                            <td><%# Eval("APPLICATION_ID") %></td>
                                            <%--<td><%# Eval("STUDENTNAME") %></td>--%>
                                            <td> <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDENTNAME") %>'></asp:Label></td>
                                            <td><%# Eval("DEGREENAME") %></td>
                                            <td><%# Eval("BRANCHNAME") %></td>                                           
                                            <td><%# Eval("ExamSchedule") %></td>
                                            <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />
                                             <asp:HiddenField ID="hdnEmailId" Visible="false" runat="server" Value='<%# Eval("EmailId") %>' />
                                            
                                                <%--   <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?"Active":"In Active" %>' ForeColor='<%#Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label></td>--%>
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
            <asp:PostBackTrigger ControlID="btnPrint" />
        </Triggers>
    </asp:updatepanel>
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
                            // SumTotal();
                            // var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                            //// alert(j);
                            // sum += parseFloat(j);
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

                        // x = sum.toString();
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

