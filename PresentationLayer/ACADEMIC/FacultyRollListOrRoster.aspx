<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FacultyRollListOrRoster.aspx.cs" Inherits="Administration_FacultyRollListOrRoster" Title="" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>


     <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlSection"
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

    <asp:UpdatePanel ID="updpnlSection" runat="server">
        <ContentTemplate>

            <div class="row" id="divMain" runat="server" visible="true">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Course Creation </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server">Student Roll List Or Roster</asp:Label></h3>
                        </div>

                                <div class="box-body">

                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Selection Criteria</h5>
                                            </div>
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Degree </label>--%>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true">Session</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlSession2" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="Faculty_Div" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Degree </label>--%>
                                                        <asp:Label ID="lblFaculty" runat="server" Font-Bold="true">Faculty</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlFaculty" runat="server" ControlToValidate="ddlFaculty"
                                                        Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="show" />
                                                </div>

                                               
                                             
                                             <%--   <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <asp:Label ID="lblCourseType" runat="server" Font-Bold="true">Course Type</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCourseType" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCourseType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%--<label>Course </label>--%>
                                                        <asp:Label ID="lblCourse" runat="server" Font-Bold="true">Course</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                 
                                                </div>

                                               
                                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%-- <label>Semester </label>--%>
                                                        <asp:Label ID="lblddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                                    </div>
                                                    <%--<asp:ListBox ID="ddlSemester" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control multi-select-demo"></asp:ListBox>--%>

                                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%--<label>Section </label>--%>
                                                        <asp:Label ID="lblSection" runat="server" Font-Bold="true">Section</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivBatch" visible="false">
                                                    <div class="label-dynamic">
                                                        <%--<label>Batch</label>--%>
                                                        <asp:Label ID="lblDYddlBatch" runat="server" Font-Bold="true">Batch</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                 
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivTutorialBatch" visible="false">
                                                    <div class="label-dynamic">
                                                      <%--<label>Tutorial Batch</label>--%>
                                                        <asp:Label ID="lblTutorialBatch" runat="server" Font-Bold="true">Tutorial Batch</asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlTutorialBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                  
                                                </div>


                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg" />
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnShowData" runat="server" Text="Show Data" ValidationGroup="show" OnClick="btnShowData_Click" CssClass="btn btn-primary" />                                           
                                                <asp:Button ID="btnExcelReport" runat="server" CausesValidation="false" Text="Excel Report" ValidationGroup="show" CssClass="btn btn-info " OnClick="btnExcelReport_Click" OnClientClick="return showConfirm2();" />
                                                <asp:Button ID="btnCourseFacultyReport" runat="server" CausesValidation="false" Text="Show Course Faculty List" ValidationGroup="show" CssClass="btn btn-info " OnClick="btnCourseFacultyReport_Click" Visible="false" />
                                             
                                                   <%--<asp:Button ID="btnUnlock" runat="server" CausesValidation="false" Text="Unlock" ValidationGroup="show" CssClass="btn btn-primary" OnClick="btnUnlock_Click" OnClientClick="return showConfirm1();" />--%>
                                                 <asp:Button ID="btnClear" runat="server" CausesValidation="false" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                            </div>
                                        </div>
                                    </asp:Panel>


                                   <%-- <div class="col-12 btn-footer">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg" />
                                    </div>--%>

                                    <asp:Panel ID="pnl_course" runat="server">

                                        <asp:HiddenField ID="hftot" runat="server" />

                                        <div class="col-12">
                                            <asp:Panel ID="pnlPreCorList" runat="server">

                                                   <div id="Div_lvCourse" runat="server" visible="false">
                                                <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Roll List / Roster</h5>
                                                        </div>
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                                <thead class="bg-light-blue">
                                                                    <tr id="trRow">
                                                                       <%-- <th>
                                                                            <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="return SelectAllBulk(this)" ToolTip="Select/Select all" />
                                                                        </th>--%>
                                                                        <%--  <th>
                                                                                            <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label></th>--%>
                                                                        <th>SR</th>
                                                                        <th>REGNO</th>
                                                                        <th>NAME</th>
                                                                         <th>SEMESTER</th>
                                                                         <th>COURSE (CCODE)</th>
                                                                         <th>SECTION</th>
                                                                         <th>BATCH</th>
                                                                         <th>TUT BATCH</th>
                                                                        <th>Mail ID</th>
                                                                        <th>REGISTRATION STATUS</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                           <%-- <td>
                                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("COURSENO")%>' onClick="totCourses(this);" />
                                                                <asp:Label ID="lblCNO" runat="server" Text='<%# Eval("COURSENO")%>' Visible="false" />
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblSRNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblREGNO" runat="server" Text='<%# Eval("REGNO")%>' />
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblSTUDNAME" runat="server" Text='<%# Eval("STUDNAME")%>' />
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="lblSEMESTERNAME" runat="server" Text='<%# Eval("SEMESTER")%>' />
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="lblCCODE" runat="server" Text=' <%#  Eval("COURSENAME") +" ("+ Eval("CCODE") +")" %>' />
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="lblSECTION" runat="server" Text='<%# Eval("SECTION")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBATCHNAME" runat="server" Text='<%# Eval("BATCHNAME")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTH_BATCHNAME" runat="server" Text='<%# Eval("TH_BATCHNAME")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEMAILID" runat="server" Text='<%# Eval("EMAILID")%>' />
                                                            </td>
                                                          <%--  <td>
                                                                <asp:Label ID="lblCOURSE_NAME" runat="server" Text='<%# Eval("COURSE_NAME")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSUBID" runat="server" Text='<%# Eval("SUBID")%>' Visible="false" />
                                                                <asp:Label ID="lblSUBNAME" runat="server" Text='<%# Eval("SUBNAME")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSEMESTERNO" runat="server" Text='<%# Eval("SEMESTERNO")%>' Visible="false" />
                                                                <asp:Label ID="lblSEMESTERNAME" runat="server" Text='<%# Eval("SEMESTERNAME")%>' />
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblSTATUS" runat="server" Text='<%# Eval("APPROVAL")%>' />
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                       </div>

                                                <div id="Div_lvCourseFaculty" runat="server" visible="false">
                                                <asp:ListView ID="lvCourseFaculty" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Course Faculty List</h5>
                                                        </div>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                                <thead class="bg-light-blue">
                                                                    <tr id="trRow">
                                                                        <th>SR </th>
                                                                        <th>COURSE NAME (CCODE)</th>
                                                                        <th>COURSE CREDIT</th>
                                                                         <th>COURSE TYPE</th>
                                                                         <th>ASSIGNED FACULTY</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                           <%-- <td>
                                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("COURSENO")%>' onClick="totCourses(this);" />
                                                                <asp:Label ID="lblCNO" runat="server" Text='<%# Eval("COURSENO")%>' Visible="false" />
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblSRNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblREGNO" runat="server" Text='<%# Eval("COURSE NAME")%>' />
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblSTUDNAME" runat="server" Text='<%# Eval("COURSE CREDIT")%>' />   
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="lblSEMESTERNAME" runat="server" Text='<%# Eval("SUBNAME")%>' />
                                                            </td>
                                                             <td>
                                                                <asp:Label ID="lblCCODE" runat="server" Text=' <%# Eval("UA_FULLNAME") %>' />
                                                            </td>
                                                           
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                    </asp:Panel>



                                </div>

                                <div id="divMsg" runat="server">
                                </div>

                          
                    </div>
                </div>
            </div>

          </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        function check() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtCCode").disabled = true;
            document.getElementById("ctl00_ContentPlaceHolder1_txtCourseName").disabled = true;
        }
    </script>

    

    <script type="text/javascript" language="javascript">

        function ScalingZero(txt) {

            if (txt.value == '') {
                document.getElementById('ctl00_ContentPlaceHolder1_txtScaling').value = 0;
            }

        }

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
        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));

            var ddlDegree = $("[id$=ddlDegree]").attr("id");
            var ddlDegree = document.getElementById(ddlDegree);
            if (ddlDegree.value == 0) {
                alert('Please Select ' + rfvddlDegree1 + ' \n', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(ddlDegree).focus();
                return false;
            }
            var ddlScheme = $("[id$=ddlScheme]").attr("id");
            var ddlScheme = document.getElementById(ddlScheme);
            if (ddlScheme.value == 0) {
                alert('Please Select ' + rfvddlScheme1 + ' \n', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(ddlScheme).focus();
                return false;
            }
            var ddlSemester = $("[id$=ddlSemester]").attr("id");
            var ddlSemester = document.getElementById(ddlSemester);
            if (ddlSemester.value == 0) {
                alert('Please Select ' + rfvddlSemester1 + ' \n', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(ddlSemester).focus();
                return false;
            }


        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnShowData').click(function () {
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

</asp:Content>
