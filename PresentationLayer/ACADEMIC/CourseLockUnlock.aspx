<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseLockUnlock.aspx.cs" Inherits="Administration_CourseLockUnlock" Title="" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>


    <asp:UpdatePanel runat="server" ID="UPDROLE" UpdateMode="Conditional">
        <ContentTemplate>

            <script>
                $(document).ready(function () {

                    $('#ctl00_ContentPlaceHolder1_lvCourse_cbHead').on('click', function () {
                        // Get all rows with search applied
                        var rows = table.rows({ 'search': 'applied' }).nodes();
                        // Check/uncheck checkboxes for all rows in the table
                        $('input[type="checkbox"]', rows).prop('checked', this.checked);
                        var count = 0;
                        for (i = 0; i <= rows.length; i++) {
                            if (document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_cbHead').checked == true) {
                                document.getElementById('<%= hfTotCourse.ClientID %>').value = count++;
                            }
                            else {
                                document.getElementById('<%= hfTotCourse.ClientID %>').value = 0;
                            }
                        }

                    });

                    // Handle click on checkbox to set state of "Select all" control
                    $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {
                        // If checkbox is not checked
                        //if (!this.checked) {

                        var el = $('#ctl00_ContentPlaceHolder1_lvCourse_cbHead').get(0);
                        var rows = table.rows({ 'search': 'applied' }).nodes();

                        // Check/uncheck checkboxes for all rows in the table
                        if (el && el.checked && ('indeterminate' in el)) {
                            // Set visual state of "Select all" control
                            // as 'indeterminate'
                            el.indeterminate = true;
                        }
                        var tot = document.getElementById('<%= hfTotCourse.ClientID %>');
                         //alert(this.checked)
                         //for (i = 0; i <= rows.length; i++) {
                         if (this.checked == true) {
                             tot.value = Number(tot.value) + 1;
                         }
                         else {
                             tot.value = Number(tot.value) - 1;
                             //document.getElementById('<%= hfTotCourse.ClientID %>').value = count--;
                        }
                         //}
                         // If "Select all" control is checked and has 'indeterminate' property

                         //}
                     });
                });
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    $(document).ready(function () {

                        $('#ctl00_ContentPlaceHolder1_lvCourse_cbHead').on('click', function () {

                            // Get all rows with search applied
                            var rows = table.rows({ 'search': 'applied' }).nodes();
                            // Check/uncheck checkboxes for all rows in the table
                            $('input[type="checkbox"]', rows).prop('checked', this.checked);


                        });

                        // Handle click on checkbox to set state of "Select all" control
                        $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {

                            // If checkbox is not checked
                            if (!this.checked) {
                                var el = $('#ctl00_ContentPlaceHolder1_lvCourse_cbHead').get(0);
                                // If "Select all" control is checked and has 'indeterminate' property
                                if (el && el.checked && ('indeterminate' in el)) {
                                    // Set visual state of "Select all" control
                                    // as 'indeterminate'
                                    el.indeterminate = true;

                                }
                            }
                        });
                    });
                });
            </script>


            <%-- <asp:HiddenField ID="hidTAB" runat="server" ClientIDMode="Static" />--%>
            <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfTotCourse" runat="server" ClientIDMode="Static" />

            <div class="row" id="divMain" runat="server" visible="true">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Course Creation </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <asp:UpdateProgress ID="UpdCCreation" runat="server" AssociatedUpdatePanelID="UPDCOURSE"
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

                        <asp:UpdatePanel ID="UPDCOURSE" runat="server">
                            <ContentTemplate>

                                <div class="box-body">

                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Selection Criteria</h5>
                                            </div>
                                            <div class="row">

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Degree </label>--%>
                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlDegree1" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit" />
                                                    <asp:RequiredFieldValidator ID="rfvddlDegree2" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show" />
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Path/Scheme </label>--%>
                                                        <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlScheme1" runat="server" ControlToValidate="ddlScheme"
                                                        Display="None" ErrorMessage="Please Select Scheme/Path" InitialValue="0" ValidationGroup="submit" />
                                                    <asp:RequiredFieldValidator ID="rfvddlScheme2" runat="server" ControlToValidate="ddlScheme"
                                                        Display="None" ErrorMessage="Please Select Scheme/Path" InitialValue="0" ValidationGroup="show" />
                                                </div>

                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%-- <label>Semester </label>--%>
                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:ListBox ID="ddlSemester" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control multi-select-demo"></asp:ListBox>

                                                    <asp:RequiredFieldValidator ID="ddlSemester1" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit" />
                                                    <asp:RequiredFieldValidator ID="ddlSemester2" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show" />
                                                </div>

                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg" />
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnShowData" runat="server" Text="Show Data" ValidationGroup="show" OnClick="btnShowData_Click" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnClear" runat="server" CausesValidation="false" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-info" />
                                                <asp:Button ID="btnLock" runat="server" CausesValidation="false" Text="Lock" ValidationGroup="show" CssClass="btn btn-warning" OnClick="btnLock_Click" OnClientClick="return showConfirm();" />
                                                <asp:Button ID="btnUnlock" runat="server" CausesValidation="false" Text="Unlock" ValidationGroup="show" CssClass="btn btn-primary" OnClick="btnUnlock_Click" OnClientClick="return showConfirm1();" />

                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="show" />
                                            </div>
                                        </div>
                                    </asp:Panel>


                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg" />
                                    </div>

                                    <asp:Panel ID="pnl_course" runat="server">

                                        <asp:HiddenField ID="hftot" runat="server" />

                                        <div class="col-12">
                                            <asp:Panel ID="pnlPreCorList" runat="server">
                                                <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Course List</h5>
                                                        </div>
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                                <thead class="bg-light-blue">
                                                                    <tr id="trRow">
                                                                        <th>
                                                                            <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="return SelectAllBulk(this)" ToolTip="Select/Select all" />
                                                                        </th>
                                                                        <%--  <th>
                                                                                            <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label></th>--%>
                                                                        <th>CCode </th>
                                                                        <th>Course Name</th>
                                                                        <th>Course Type</th>
                                                                        <th>Semester</th>
                                                                        <th>Status</th>

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
                                                            <td>
                                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("COURSENO")%>' onClick="totCourses(this);" />
                                                                <asp:Label ID="lblCNO" runat="server" Text='<%# Eval("COURSENO")%>' Visible="false" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCode")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCOURSE_NAME" runat="server" Text='<%# Eval("COURSE_NAME")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSUBID" runat="server" Text='<%# Eval("SUBID")%>' Visible="false" />
                                                                <asp:Label ID="lblSUBNAME" runat="server" Text='<%# Eval("SUBNAME")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSEMESTERNO" runat="server" Text='<%# Eval("SEMESTERNO")%>' Visible="false" />
                                                                <asp:Label ID="lblSEMESTERNAME" runat="server" Text='<%# Eval("SEMESTERNAME")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSTATUS" runat="server" Text='<%# Eval("STATUS")%>' />
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </asp:Panel>



                                </div>

                                <div id="divMsg" runat="server">
                                </div>

                            </ContentTemplate>
                            <Triggers>
                                <%-- <asp:PostBackTrigger ControlID="btnShowData" />
                                <asp:PostBackTrigger ControlID="btnClear" />
                                <asp:PostBackTrigger ControlID="btnLock" />
                                 <asp:PostBackTrigger ControlID="btnCheckListReport" />
                                <asp:PostBackTrigger ControlID="btnUnlock" />--%>
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script>
        function check() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtCCode").disabled = true;
            document.getElementById("ctl00_ContentPlaceHolder1_txtCourseName").disabled = true;
        }
    </script>

    <script type="text/javascript" language="javascript">

        function SelectAllBulk(headchk) {
            var i = 0;
            var txtTot = document.getElementById('<%= hfTotCourse.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }

            if (headchk.checked == true) {
                document.getElementById('<%= hfTotCourse.ClientID %>').value = count;
             }
             else {
                 document.getElementById('<%= hfTotCourse.ClientID %>').value = 0;
             }

         }

         function validateAssign() {
             var txtTot = document.getElementById('<%= hfTotCourse.ClientID %>').value;
            // alert(offer);
            if (txtTot == 0) {
                alert('Please Check atleast one course ');
                return false;
            }

        }

        function totCourses(chk) {
            var txtTot = document.getElementById('<%= hfTotCourse.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
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
        function showConfirm() {
            var txtTot = document.getElementById('<%= hfTotCourse.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please select atleast one course!');
                return false;

            }
            else {
                var ret = confirm('Do you Really want to Confirm/Lock this Course?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
        }

        function showConfirm1() {
            var txtTot = document.getElementById('<%= hfTotCourse.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please select atleast one course!');
                return false;

            }
            else {
                var ret = confirm('Do you Really want to Confirm/UnLock this Course?');
                if (ret == true)
                    return true;
                else
                    return false;
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
