<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Deactive_Student_Users.aspx.cs" Inherits="ACADEMIC_Deactive_Student_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="updFaculty" runat="server">
        <ContentTemplate>
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
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <asp:HiddenField ID="hfdUserNo" runat="server" />
                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Admission Batch</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlAdmBatch" ValidationGroup="Faculty" data-select2-enable="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="Faculty"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Degree</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDegree" ValidationGroup="Faculty" AutoPostBack="true" AppendDataBoundItems="true"
                                        data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" ValidationGroup="Faculty"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Branch</label>
                                    </div>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlBranch" ValidationGroup="Faculty" AppendDataBoundItems="true"
                                        data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" ValidationGroup="Faculty"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Branch">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer" id="buttonSection" runat="server">

                            <asp:LinkButton ID="btnShow" runat="server" ValidationGroup="Faculty" Text="Show" CssClass="btn btn-outline-primary" OnClick="btnShow_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnDeactive" runat="server" Text="Deactivate" CssClass="btn btn-outline-danger" Visible ="false" OnClick="btnDeactive_Click" />
                            <asp:ValidationSummary ID="vsFacultyUsers" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Faculty" />
                        </div>

                        <div class="col-md-12">
                            <asp:Panel ID="pnlStudent" runat="server">
                                <asp:ListView ID="lvStudent" runat="server" Visible="true" OnItemDataBound="lvStudent_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" id="tbltest">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th runat="server" id="thcheck">
                                                        <asp:CheckBox ID="chkAll" onclick="checkAll(this)" Text="" runat="server"  />
                                                    </th>
                                                    <th>User Name
                                                    </th>
                                                    <th>
                                                        Student Name
                                                    </th>
                                                    <th>Faculty Name
                                                    </th>
                                                    <th>Email
                                                    </th>
                                                    <th>Mobile No.
                                                    </th>
                                                    <th>Status
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td id="td1" runat="server">
                                                <asp:CheckBox ID="chkActive" runat="server" TabIndex="1" Checked='<%# Eval("STATUS").ToString() == "INACTIVE" ? true : false %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UA_NAME") %>' Enabled="false" TabIndex="1"></asp:Label>
                                                <asp:HiddenField ID="hfdUaNo" runat="server" Value='<%# Eval("UA_NO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStudentName" runat="server" Text='<%#Eval("STUDENT_NAME") %>' Enabled="false" TabIndex="1"></asp:Label>
                                                <asp:HiddenField ID="hfdIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFacultyName" runat="server" Text='<%#Eval("FACULTY_NAME") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EMAILID") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%#Eval("MOBILENO") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>' Enabled="false" TabIndex="1"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function checkAll(source) {
            var checkboxes = document.querySelectorAll('tbody input[type="checkbox"]');
            for (var i = 0; i < checkboxes.length; i++) {
                if (!checkboxes[i].disabled) {
                    checkboxes[i].checked = source.checked;
                }
            }
        }
    </script>
</asp:Content>

