<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkPhdCourseRegistration.aspx.cs" Inherits="Academic_BulkPhdCourseRegistration"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .dataTables_wrapper {
            margin-top: -39px;
        }

        @media only screen and (max-width:767px) {
            .dataTables_wrapper {
                margin-top: 0px;
            }
        }
    </style>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upd1"
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
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>

            <div class="col-sm-12">
                <!-- general form elements -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">STUDENT COURSE REGISTARTION APPROVAL </h3>
                        <div class="notice"><span>Note : * marked fields are Mandatory</span></div>
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />

                    </div>
                    <div class="box-body">

                        <div class="form-group col-sm-12">
                            <div class="row">
                                <div class="col-sm-4 form-group">
                                    <sup>*</sup>
                                    <label>Session</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show" SetFocusOnError="True" />

                                </div>
                                <div class="col-sm-4 form-group">
                                    <sup>*</sup>
                                    <label>Degree:</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlSectionCancel" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                </div>
                                <div class="col-sm-4 form-group">
                                    <label>Department:</label>
                                    <asp:DropDownList ID="ddlDeptName" runat="server" AppendDataBoundItems="true" class="form-control" data-select2-enable="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="text-center form-group col-sm-12 ">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="show" />
                            <asp:Button ID="Show" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btn btn-primary" ValidationGroup="show" />
                            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                            <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                CssClass="btn btn-danger" />
                        </div>


                        <asp:Panel ID="pnllist" runat="server">

                            <div class="form-group col-sm-12 table-responsive">
                                <asp:ListView ID="lvStudent" runat="server" OnSelectedIndexChanged="lvStudent_SelectedIndexChanged">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid">
                                            <div class="titlebar">
                                                <h4>
                                                    <label class="label label-default">List of Students</label></h4>
                                            </div>
                                            <table id="example" class="table table-bordered table-hover table-fixed text-center" style="width: 100%;">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>SR No.
                                                        </th>
                                                        <th>Preview
                                                        </th>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                        </th>
                                                        <th>Roll No.
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Supervisior
                                                        </th>
                                                        <th>Joint Supervisior
                                                        </th>
                                                        <th>Institute Faculty
                                                        </th>
                                                        <th>Drc Member
                                                        </th>
                                                        <th>Drc Chairman
                                                        </th>
                                                        <th>Dean 
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>

                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <%# Container.DataItemIndex + 1 %>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnPreview" Text="Preview" CommandArgument='<%# Eval("ROLLNO") %>' CommandName='<%# Eval("IDNO") %>' ToolTip='<%# Eval("SEMESTER")%>' CssClass="btn btn-warning" OnClick="btnPreview_Click" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' Checked='<%# Eval("DEAN").ToString() == string.Empty ? false : true %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRegNo" runat="server" Text='<%# Eval("ROLLNO") %>' Width="80px" Enabled="false" />
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblsem" runat="server" Text='<%# Eval("SEMESTER")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSupervisior" runat="server" Text='<%# Eval("SUPERVISORSTATUS")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbljointsupervisior" runat="server" Text='<%# Eval("JOINTSUPERVISORSTATUS")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblinsfaculty" runat="server" Text='<%# Eval("INSTITUTEFACULTYSTATUS")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldrc" runat="server" Text='<%# Eval("DRCSTATUS")%>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbldrcchairman" runat="server" Text='<%# Eval("DRCCHAIRMANSTATUS")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldean" runat="server" Text='<%# Eval("DEANSTATUS")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvStudent" />
        </Triggers>
    </asp:UpdatePanel>

    <%--var popUrl = 'maingroup.aspx?obj=' + 'AccountingVouchers';--%>

    <script type="text/javascript" language="javascript">

        function openNewWin(url) {

            var x = window.open(url, 'mynewwin', 'width=950,height=600,toolbar=1');

            x.focus();

        }


        function totAllSubjects(headchk) {
	    <%--var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');	    
	    var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');	 --%>

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

    <div id="divMsg" runat="server">
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#example').DataTable({
                scrollY: '50vh',
                "scrollX": true,
                // scrollCollapse: true,
                "paging": false,
                "ordering": false,

                "info": false
            }
            );
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('#example').DataTable({
                scrollY: '50vh',
                //  scrollCollapse: true,
                "scrollX": true,
                "paging": false,
                "margin-top": '3vh',
                "ordering": false,
                "info": false,

                "columnDefs": [
              { "width": "10%", "targets": 0 }]
            }
            );
        });

    </script>
</asp:Content>
