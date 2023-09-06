<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkPhdSemesterPramotion.aspx.cs" Inherits="Academic_BulkPhdSemesterPramotion"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PHD SEMESTER PRAMOTION</h3>
                    <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session </label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show" SetFocusOnError="True" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlSectionCancel" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Branch </label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Scheme </label>
                                </div>
                                <asp:DropDownList ID="ddlScheme" runat="server" AutoPostBack="true" AppendDataBoundItems="true"
                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="show" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Semester </label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="1">I</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="col-12 col-md-12 col-lg-6" runat="server" id="tblupdate" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>UPDATE STUDENT DETAILS</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlupsession" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlupsession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit" SetFocusOnError="True" />
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlupsemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlupsemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="false" ValidationGroup="show" />
                        <asp:Button ID="Show" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btn btn-primary" ValidationGroup="show" />
                        <asp:Button ID="Submit" runat="server" Text="Update" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="submit" />
                        <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnllist" runat="server">

                            <asp:ListView ID="lvStudent" runat="server" OnSelectedIndexChanged="lvStudent_SelectedIndexChanged">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Students</h5>
                                    </div>

                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>SR No.
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                </th>
                                                <th>Roll No.
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>SEMESTER
                                                </th>
                                                <th>COURSENAME
                                                </th>
                                                <th>CREDITS
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
                                            <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                        </td>
                                        <td>

                                            <asp:TextBox ID="txtRegNo" runat="server" Text='<%# Eval("ROLLNO") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("STUDNAME")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCoursename" runat="server" Text='<%# Eval("COURSENAME")%>' ToolTip='<%# Eval("COURSENO") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblcredits" runat="server" Text='<%# Eval("CREDITS")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>

                            </asp:ListView>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" lang="javascript">

        function openNewWin(url) {

            var x = window.open(url, 'mynewwin', 'width=950,height=600,toolbar=1');

            x.focus();

        }

        function totAllSubjects(headchk) {
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
</asp:Content>
