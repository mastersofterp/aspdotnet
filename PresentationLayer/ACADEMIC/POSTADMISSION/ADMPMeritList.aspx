<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPMeritList.aspx.cs" Inherits="ACADEMIC_DAIICTPostAdmission_ADMPMeritList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnllvSh .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>


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
                            <%-- <h3 class="box-title">Exam Student Attendance</h3>--%>
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
                                            <label>Test Details</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTestDetails" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2">
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
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Generation Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlGeneration" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlGeneration_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">Branch Wise</asp:ListItem>
<%--                                            <asp:ListItem Value="2">Category Wise</asp:ListItem>
                                            <asp:ListItem Value="3">General</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Application Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlApplicationType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                             <asp:ListItem Value="-1">Please Select </asp:ListItem>
                                            <asp:ListItem Value="0">INDIAN </asp:ListItem>
                                            <asp:ListItem Value="1">NRI</asp:ListItem>
                                            <asp:ListItem Value="2">FN</asp:ListItem>                                            
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="rfvApplicationType" runat="server" ControlToValidate="ddlApplicationType" ValidationGroup="MeritList"
                                             Display="None" InitialValue="-1" ErrorMessage="Please Select Application Type">
                                         </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:Panel runat="server" ID="pnlBranch" Visible="true">
                                            <div class="label-dynamic">
                                                <%--  <sup>* </sup>--%>
                                                <label>Program/Branch </label>
                                            </div>
                                            <%--<asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>--%>
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                        </asp:Panel>

                                        <asp:Panel runat="server" ID="pnlCategory" Visible="true">
                                            <div class="label-dynamic">
                                                <%-- <sup>* </sup>--%>
                                                <label>Category</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" TabIndex="5">
                                            </asp:DropDownList>
                                        </asp:Panel>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:Panel runat="server" ID="pnlSortBy" Visible="false">
                                            <div class="label-dynamic">

                                                <label>Category</label>
                                            </div>
                                            <%--<asp:ListBox ID="lstProgram" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" TabIndex="6" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>--%>
                                            <asp:DropDownList ID="ddlSortBy" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                        </asp:Panel>
                                    </div>

                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Application Cutoff Date</label>
                                        </div>
                                        <asp:TextBox ID="txtRecDate" runat="server"></asp:TextBox>
                                      <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" 
                                            TargetControlID="txtRecDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Generate Merit List" TabIndex="6" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="7" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnLock" runat="server" Text="Lock" TabIndex="9" CssClass="btn btn-danger" OnClick="btnLock_Click" />
                                <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" TabIndex="9" CssClass="btn btn-primary" OnClick="btnExportToExcel_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="vsMeritList" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="MeritList" />
                            </div>


                            <asp:Panel runat="server" ID="pnlCount" Width="100%" Visible="False">
                                <div class="form-group form-inline col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label style="color: red" id="lbltotalstudent">Total Students :- </label>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtTotalCount" Font-Bold="true" Enabled="false" ForeColor="Green" CssClass="ml-2"> </asp:TextBox>
                                    <%-- </div>
                                 <div class="form-group form-inline col-lg-3 col-md-6 col-12">--%>
                                </div>
                            </asp:Panel>

                            <div class="col-md-12">
                                <asp:Panel ID="pnllvSh" runat="server">
                                    <asp:ListView ID="lvMeritList" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Notification Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divadmissionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr No.</th>

                                                        <th>Applicant Name</th>
                                                        <th>E-Mail ID/ User ID</th>
                                                        <th>Application ID</th>
                                                        <th>Program/ Branch</th>
                                                        <th>Category</th>


                                                        <th>Score Obtained</th>
                                                        <th>CRL</th>
                                                        <th>Overall Merit No</th>
                                                        <th>Category Wise Merit No</th>
                                                        <th>Lock Status</th>
                                                         <th>NRI</th>
                                                        <%--<th>
                                                            Form Categort
                                                        </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>

                                                <td><%# Eval("CANDIDATE_NAME") %></td>
                                                <td><%# Eval("USERNAME") %></td>

                                                <td>
                                                    <asp:Label ID="lblApplicationId" runat="server" Text='<%# Eval("REGNO") %>' />
                                                </td>
                                                <td><%# Eval("LONGNAME") %></td>
                                                <td><%# Eval("CATEGORY") %></td>
                                                <td>
                                                    <asp:Label ID="lblScoreObtained" runat="server" Text='<%# Eval("SCORE_OBTAINED") %>' />
                                                </td>
                                                <td><%# Eval("INDIA_RANK") %></td>
                                                <td>
                                                    <asp:Label ID="lblGeneralMeritNo" runat="server" Text='<%# Eval("GeneralMeritNo") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCatMeritNo" runat="server" Text='<%# Eval("RANKNO") %>' />
                                                </td>
                                                  <td><asp:Label ID="lblStatus" runat="server" Text='<%#  Convert.ToString(Eval("LOCK_STATUS"))==" "? "Not Submited" : Convert.ToString(Eval("LOCK_STATUS"))=="1" ? "Lock": "UnLock" %>'                                              
                                                  ForeColor='<%# Convert.ToString(Eval("LOCK_STATUS"))=="0"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'
                                                 Font-Bold="true"></asp:Label>
                                                   <%-- <asp:Label ID="lblStatus" runat="server" 
                                                        Text='<%# Eval("LOCK_STATUS") == null ? "Not Submitted" : (Convert.ToString(Eval("LOCK_STATUS")) == "0" ? "UnLock" : "Lock") %>'
                                                        ForeColor='<%# Eval("LOCK_STATUS") == null ? System.Drawing.Color.Black : (Convert.ToString(Eval("LOCK_STATUS")) == "0" ? System.Drawing.Color.Green : System.Drawing.Color.Red) %>'
                                                        Font-Bold="true">--%>

                                                        <%--<asp:Label ID="Label1" runat="server" Text='<%# Eval("LOCK_STATUS") %>' />--%>




                                                  </td> 
                                                <td>
                                                    <asp:Label ID="lblNRI" runat="server" Text='<%# Eval("NRI") %>' />
                                                </td>

                                                <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />
                                                <asp:HiddenField ID="hdnAdmBatch" Visible="false" runat="server" Value='<%# Eval("ADMBATCH") %>' />
                                                <asp:HiddenField ID="hdnCategoryNo" Visible="false" runat="server" Value='<%# Eval("CATEGORYNO") %>' />
                                                <asp:HiddenField ID="hdnBranchNo" Visible="false" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                <asp:HiddenField ID="hdnIsNRI" Visible="false" runat="server" Value='<%# Eval("IS_NRI") %>' />

                                                <%-- <td>
                                                    <%# Eval("FORM_CATEGORY") %>
                                                </td--%>
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
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
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
