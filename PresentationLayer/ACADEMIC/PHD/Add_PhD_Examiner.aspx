<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Add_PhD_Examiner.aspx.cs" Inherits="ACADEMIC_PHD_Add_PhD_Examiner" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>

    <%--<script>
        function Validate(txt) {
            var alerMsg = "";
            var name = document.getElementById('<%=txtName.ClientID%>');
            var institute = document.getElementById('<%=txtInstituteName.ClientID%>');
            var mobile = document.getElementById('<%=txtMobile.ClientID%>');
            var email = document.getElementById('<%=txtEmail.ClientID%>');
            if (name.value == "" || institute.value == "" || mobile.value == "" || email.value == "" || desig.value == "") {
                if (name.value == "") {
                    alerMsg = "Please enter name.\n";
                }
                if (institute.value == "") {
                    alerMsg += "Please enter institute name.\n";
                }
                if (mobile.value == "") {
                    alerMsg += "Please enter mobile no.\n";
                }
                if (email.value == "") {
                    alerMsg += "Please enter email id.\n";
                }

                if (desig.value == "") {
                    alerMsg += "Please enter designation.\n";
                }
                alert(alerMsg);
                return false;
            }
        }
    </script>--%>
    <script>
        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            var txtmail = document.getElementById('<%= txtEmail.ClientID %>')
            if (reg.test(emailField.value) == false) {
                alert('Invalid Email Address');
                txtmail.value = '';
                return false;
            }

            return true;
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>

                    </h3>

                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Add Examiner</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Examiner Mapping</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UPDPROG" runat="server" AssociatedUpdatePanelID="updExaminer"
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
                                <asp:UpdatePanel runat="server" ID="updExaminer">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtName" runat="server" ToolTip="Please enter name." TabIndex="3" MaxLength="128" AutoComplete="off" placeholder="Please enter name" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajtbeName" runat="server" TargetControlID="txtName" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_-+={[}]:;<,>?/'|\1234567890."></ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" SetFocusOnError="True"
                                                            ErrorMessage="Please Enter Examiner Name." ControlToValidate="txtName"
                                                            Display="None" ValidationGroup="submit" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Institute Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtInstituteName" runat="server" ToolTip="Please enter institute name." TabIndex="4" MaxLength="128" AutoComplete="off" placeholder="Please enter institute name" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajtbeInstituteName" runat="server" TargetControlID="txtInstituteName" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_+={[}]:;<,>?'|\"></ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True"
                                                            ErrorMessage="Please Enter Institute Name." ControlToValidate="txtInstituteName"
                                                            Display="None" ValidationGroup="submit" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mobile No</label>
                                                        </div>
                                                        <asp:TextBox ID="txtMobile" runat="server" ToolTip="Please enter mobile no." TabIndex="5" MaxLength="10" AutoComplete="off" placeholder="Please enter mobile no." CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMobile" FilterMode="ValidChars" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" SetFocusOnError="True"
                                                            ErrorMessage="Please Enter Mobile No." ControlToValidate="txtMobile"
                                                            Display="None" ValidationGroup="submit" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ControlToValidate="txtMobile" ErrorMessage="Please Enter valid Mobile No." Display="None"
                                                            ValidationExpression="[0-9]{10}" ValidationGroup="submit"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmail" runat="server" ToolTip="Please enter email id." TabIndex="6" MaxLength="64" AutoComplete="off" placeholder="Please enter email id" CssClass="form-control" onblur="validateEmail(this);"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtEmail" FilterMode="InvalidChars" InvalidChars="~`!#$%^&*()-+={}][:;'<,>?/|\"></ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="True"
                                                            ErrorMessage="Please Enter Email Id." ControlToValidate="txtEmail"
                                                            Display="None" ValidationGroup="submit" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Examiner Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlExaminerType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Examiner Type"
                                                            TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlExaminerType_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Indian</asp:ListItem>
                                                            <asp:ListItem Value="2">Foreign</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="submit" ControlToValidate="ddlExaminerType" Display="None"
                                                            ErrorMessage="Please Select Examiner Tpye." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divState" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>State</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select State" TabIndex="7">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="submit" ControlToValidate="ddlState" Display="None"
                                                            ErrorMessage="Please Select State." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCountry" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Country</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Country" TabIndex="8">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="submit" ControlToValidate="ddlCountry" Display="None"
                                                            ErrorMessage="Please Select Country." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Click to save." TabIndex="9" OnClick="btnSave_Click" ValidationGroup="submit" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click to cancel." OnClick="btnCancel_Click" TabIndex="10" CausesValidation="false" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlExaminer" runat="server" Visible="false">
                                                    <asp:ListView ID="lvExaminer" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Examiner List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">Edit
                                                                        </th>
                                                                        <th>Examiner Name
                                                                        </th>
                                                                        <th>Institute Name
                                                                        </th>
                                                                        <th>Mobile No
                                                                        </th>
                                                                        <th>Email Id
                                                                        </th>
                                                                        <th>Examiner Type
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
                                                                <td style="text-align: center;">
                                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png"
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="11" CommandArgument='<%#Eval("EXAMINER_ID")%>' CausesValidation="false" />
                                                                </td>
                                                                <td>
                                                                    <%#Eval("EXAMINER_NAME") %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("INSTITUTE_NAME") %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("MOBILE_NO") %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("EMAILID") %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("EXAMINER_TYPE") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updMapping"
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
                                <asp:UpdatePanel ID="updMapping" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div id="div2" runat="server"></div>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAdmBatch" runat="server">
                                                        <span style="color: red;">*</span><label>Admission Batch</label>
                                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" ToolTip="Please Select Admission Batch" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                            Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="show" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="submitMap" ControlToValidate="ddlAdmBatch" Display="None"
                                                            ErrorMessage="Please Select Admission Batch" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divExaminer" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Examiner</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlExaminerMap" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Examiner"
                                                            TabIndex="3">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="submitMap" ControlToValidate="ddlExaminerMap" Display="None"
                                                            ErrorMessage="Please Select Examiner." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
<%--                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Student</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Student" TabIndex="4">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="submitMap" ControlToValidate="ddlStudent" Display="None"
                                                            ErrorMessage="Please Select Student." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>--%>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-primary" ToolTip="Click to Show." TabIndex="5" ValidationGroup="show" OnClick="btnshow_Click" />
                                                <asp:Button ID="btnSubmitMap" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Click to Submit." Visible="false" TabIndex="5" ValidationGroup="submitMap" OnClick="btnSubmitMap_Click" />
                                                <asp:Button ID="btnCancelMap" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click to Cancel." TabIndex="6" CausesValidation="false" OnClick="btnCancelMap_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="show" />
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submitMap" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="Panellistview" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: left; padding-left: 3px">
                                                                                    <asp:CheckBox ID="chkBoxFeesTransfer" runat="server" onclick="totAllSubjects(this)" />
                                                                                </th>
                                                                                <th>Name
                                                                                </th>
                                                                                <th style="display: none">IdNo
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th><%--Branch--%>
                                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th><%--Semester--%>
                                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Father Name
                                                                                </th>
                                                                                <th>Mother Name
                                                                                </th>
                                                                                <th>Mobile No.
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="chkloan" runat="server" ToolTip='<%#Eval("idno") %>' AutoPostBack="true" />
                                                                </td>
                                                                <td><%# Eval("Name") %>
                                                                </td>
                                                                <td style="display: none">
                                                                    <%# Eval("idno")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTERNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FATHERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MOTHERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("STUDENTMOBILE") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
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
    <script>
        function SetMapping(val) {

            $('#chkMapping').prop('checked', val);
        }


    </script>
    <script>
        function validateEmail(emailField) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            var txtmail = document.getElementById('<%= txtEmail.ClientID %>')
            if (reg.test(emailField.value) == false) {
                alert('Invalid Email Address');
                txtmail.value = '';
                return false;
            }

            return true;
        }
    </script>
    <script>
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            var j = 0;
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                debugger;
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (j != 0) {
                            e.checked = true;
                        }
                        j++;
                    }
                    else
                        e.checked = false;
                }
            }

        }
    </script>

</asp:Content>

