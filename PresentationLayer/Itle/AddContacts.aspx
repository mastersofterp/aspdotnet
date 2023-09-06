<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddContacts.aspx.cs" Inherits="ITLE_AddContacts" %>

<!DOCTYPE html >
<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <%--<title>MasterSoft ERP Sloution RPVT LTD</title>--%>
    <link rel="shortcut icon" href="../Images/nophoto.jpg" type="image/x-icon" id="lnklogo">
    <%-- Added Google Sign In on Date 21/09/2020 by DEEPALI--%>
    <%--As disccused with Umesh sir commnted this google sign in button section on date 30-12-2021. will work on this in second phase--%>
    <%--<meta name="google-signin-client_id" content="756005764126-6b0tt497vp345vfn0nso5reuonq5o11l.apps.googleusercontent.com">--%>
    <%-- End Google Sign In on Date 21/09/2020 by DEEPALI--%>

    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/bootstrap.min.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/fontawesome-free-5.15.4/css/all.min.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/css/newcustom.css")%>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/select2/select2.css")%>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/css/dataTables.bootstrap4.min.css")%>" rel="stylesheet" />
    <link href="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/css/buttons.bootstrap4.min.css")%>" rel="stylesheet" />

    <%-- scripts added by gaurav--%>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/jquery-3.5.1.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/popper.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/newbootstrap/js/bootstrap.min.js")%>"></script>

    <script src="<%=Page.ResolveClientUrl("~/plugins/select2/select2.js")%>"></script>

    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/jquery.dataTables.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/dataTables.bootstrap4.min.js")%>"></script>

    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/dataTables.buttons.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/buttons.bootstrap4.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/jszip.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/pdfmake.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/vfs_fonts.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/buttons.html5.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/buttons.print.min.js")%>"></script>
    <script src="<%#Page.ResolveClientUrl("~/plugins/datatable-responsive/js/buttons.colVis.min.js")%>"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12 mt-3">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Add Contacts</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Show</label>
                                        </div>
                                        <asp:RadioButton ID="rdoStudent" runat="server" Text="Student" GroupName="a" Checked="true"
                                            AutoPostBack="true" OnCheckedChanged="rdoStudent_CheckedChanged" onclick="ShowBranch();" />&nbsp;
                                            <asp:RadioButton ID="rdoFaculty" runat="server" Text="Faculty" GroupName="a" AutoPostBack="true"
                                                OnCheckedChanged="rdoFaculty_CheckedChanged" onclick="ShowBranch();" />

                                        <asp:RadioButton ID="rdoGroupMail" runat="server" Visible="false" Text="Group Mail" GroupName="a" AutoPostBack="true"
                                            OnCheckedChanged="rdoGroupMail_CheckedChanged" onclick="ShowBranch();" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server" style="display: block;">
                                        <div class="label-dynamic">
                                            <label>Belongs to Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divGroupMail" runat="server" style="display: none;">
                                        <div class="label-dynamic">
                                            <label>Belongs to Group</label>
                                        </div>
                                        <asp:DropDownList ID="ddlGroupMail"
                                            runat="server" AutoPostBack="true"
                                            AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlGroupMail_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class=" col-12 btn-footer">
                                <input id="btnAddContacts" type="button" value="Add Contacts" class="btn btn-primary" onclick="javascript: SetContacts();" />
                                <input id="Button1" type="button" value="Close Window" class="btn btn-warning" onclick="javascript: window.close();" />
                            </div>
                            <div class="form-group col-lg-4 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note </h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Press Ctrl+F to find any name. </span></p>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvStudents" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblStudentList">
                                                <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th>
                                                            <input id="chkSelectAll" type="checkbox" onclick="SelectAll(this);" />&nbsp;&nbsp;&nbsp;Enrl. No.
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Branch Short Name
                                                        </th>
                                                        <th>Branch Long Name
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
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkSelectMail" runat="server" />
                                                <asp:HiddenField ID="hidUserName" runat="server" Value='<%# Eval("UA_FULLNAME")%>' />
                                                <asp:HiddenField ID="hidUserId" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SHORTNAME")%> <%--Added on 24-03-2017 by Saket Singh--%>
                                            </td>
                                            <td>
                                                <%# Eval("LONGNAME")%> <%--Added on 24-03-2017 by Saket Singh--%>
                                            </td>
                                            <%-- <td>
                                        <%# Eval("BRANCH")%>
                                    </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <p class="tect-center">
                                            No user contact found.<br />
                                        </p>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvFaculty" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblFacultyList">
                                                <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th>
                                                            <input id="chkSelectAll" type="checkbox" onclick="SelectAll(this);" />
                                                        </th>
                                                        <th>Faculty Name
                                                        </th>
                                                        <th>Department Name 
                                                        </th>
                                                        <th>UG/PG/OT  
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
                                                <asp:CheckBox ID="chkSelectMail" runat="server" />
                                                <asp:HiddenField ID="hidUserName" runat="server" Value='<%# Eval("UA_FULLNAME")%>' />
                                                <asp:HiddenField ID="hidUserId" runat="server" Value='<%# Eval("UA_NO")%>' />
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                            <%--<td>
                                        <%# Eval("DEPT")%>
                                    </td>--%>
                                            <td>
                                                <%# Eval("DEPARTNAME")%>  <%--Added on 24-03-2017 by Saket Singh--%>
                                            </td>
                                            <td>
                                                <%# Eval("UGPGOT")%>  <%--Added on 24-03-2017 by Saket Singh--%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>--%>

    <script>
        $(document).ready(function () {
            $("[data-select2-enable=true]").addClass("select2 select-clik");
        });

        $(document).ready(function () {
            $('.select2').select2({
                dropdownAutoWidth: true,
                width: '100%',
                //allowClear: true
            })
        });

        $(document).ready(function () {
            $(document).on("click", ".select2-search-clear-icon", function () {
                var sel2id = localStorage.getItem("sel2id");
                $('#' + sel2id).select2('close');
                $('#' + sel2id).select2('open');
            });

            $(document).on('click', '.select2', function () {
                debugger
                var key = $(this).parent().find('.select-clik').attr('id');
                localStorage.setItem("sel2id", key);
            });
        });
        </script>


    <script type="text/javascript" language="javascript">
        function SetContacts() {
            var usernames = '';
            var userno = '';
            try {

                var tbl;
                var lvName = '';

                if (document.getElementById('rdoStudent').checked || document.getElementById('rdoGroupMail').checked) {
                    tbl = document.getElementById('tblStudentList');
                    lvName = 'lvStudents';

                }
                else {

                    tbl = document.getElementById('tblFacultyList');
                    lvName = 'lvFaculty';
                }

                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var chk = document.getElementById(lvName + '_ctrl' + (i - 1).toString() + '_chkSelectMail');
                        if (chk != null && chk.checked) {
                            var hidUserNm = document.getElementById(lvName + '_ctrl' + (i - 1).toString() + '_hidUserName');
                            var hidUserNo = document.getElementById(lvName + '_ctrl' + (i - 1).toString() + '_hidUserId');
                            if (hidUserNm != null && hidUserNo != null) {
                                if (usernames.length > 0) { usernames += ', '; }
                                usernames += hidUserNm.value;

                                if (userno.length > 0) { userno += ', '; }
                                userno += hidUserNo.value;
                            }
                        }
                    }
                }
                if (usernames.length > 0 && userno.length > 0) {
                    // set the values into parent (opener form)
                    window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtMailTo").value = usernames;
                    window.opener.document.getElementById("ctl00_ContentPlaceHolder1_hdntxtMailto").value = usernames;
                    window.opener.document.getElementById("ctl00_ContentPlaceHolder1_hidMailTo").value = userno;
                    window.close();
                }
                else {
                    alert('Please select contacts to add.');
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function GetSelectedUsers() {
            var usernames = '';
            try {
                var tbl;
                var lvName = '';

                if (document.getElementById('rdoStudent').checked || document.getElementById('rdoGroupMail').checked) {
                    tbl = document.getElementById('tblStudentList');
                    lvName = 'lvStudents';
                }
                else {
                    tbl = document.getElementById('tblFacultyList');
                    lvName = 'lvFaculty';
                }

                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var chk = document.getElementById(lvName + '_ctrl' + (i - 1).toString() + '_chkSelectMail');
                        if (chk != null && chk.checked) {
                            var hidUserNm = document.getElementById(lvName + '_ctrl' + (i - 1).toString() + '_hidUserName');
                            if (hidUserNm != null) {
                                if (usernames.length > 0) { usernames += ', '; }
                                usernames += hidUserNm.value;
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return usernames;
        }

        function SelectAll(chkAll) {
            try {
                var tbl;
                var lvName = '';

                if (document.getElementById('rdoStudent').checked || document.getElementById('rdoGroupMail').checked) {

                    tbl = document.getElementById('tblStudentList');
                    lvName = 'lvStudents';
                }
                else {

                    tbl = document.getElementById('tblFacultyList');
                    lvName = 'lvFaculty';
                }

                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var chk = document.getElementById(lvName + '_ctrl' + (i - 1).toString() + '_chkSelectMail');
                        if (chk != null) {
                            chk.checked = chkAll.checked;
                        }
                    }
                }
            }
            catch (ex) {
            }
        }

        function ShowBranch() {
            if (document.getElementById('rdoStudent').checked)
                document.getElementById('divBranch').disabled = "disabled";
            else
                document.getElementById('divBranch').disabled = "enabled";
        }

    </script>
</body>
</html>
