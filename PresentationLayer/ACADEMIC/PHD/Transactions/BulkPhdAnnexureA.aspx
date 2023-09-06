<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkPhdAnnexureA.aspx.cs" Inherits="Academic_BulkPhdAnnexureA"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div2" runat="server">
    </div>

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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT ANNEXURE A APPROVAL</h3>
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="show" SetFocusOnError="True" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSectionCancel" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Branch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDeptName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="false" ValidationGroup="show" />
                                <asp:Button ID="Show" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btn btn-primary" ValidationGroup="show" />
                                <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                            </div>

                            <asp:Panel ID="pnllist" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvStudent" runat="server" OnSelectedIndexChanged="lvStudent_SelectedIndexChanged">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>List of Students</h5>
                                            </div>
                                            <table id="id1" class="table table-striped table-bordered nowrap">
                                                <thead class="bg-light-blue">
                                                    <tr>
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
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnPreview" Text="Preview" CommandArgument='<%# Eval("ROLLNO") %>' CommandName='<%# Eval("IDNO") %>' OnClick="btnPreview_Click" CssClass="btn btn-primary" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' /><%-- Checked='<%# Eval("DEAN").ToString() == string.Empty ? false : true %>' />--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtRegNo" runat="server" Text='<%# Eval("ROLLNO") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvStudent" />
        </Triggers>
    </asp:UpdatePanel>

    <%--var popUrl = 'maingroup.aspx?obj=' + 'AccountingVouchers';--%>

    <script type="text/javascript" lang="javascript">

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

		<%--if (headchk.checked == true)
		    txtTot.value = hdfTot.value;
		else
		    txtTot.value = 0;
		
		validateAssign();  --%>
        }
    </script>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#id1').dataTable({
                paging: false,
                searching: true,
                ordering: true,
                info: false,
                bDestroy: true
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('#id1').dataTable({
                paging: false,
                searching: true,
                ordering: true,
                info: false,
                bDestroy: true
            });
        });
    </script>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>
