<%@ Page Language="C#" AutoEventWireup="true" CodeFile="External_Member_Form.aspx.cs" Inherits="ACADEMIC_PHD_External_Member_Form" Title="" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <script>
        function Validate(txt) {
            var alerMsg = "";
            var name = document.getElementById('<%=txtName.ClientID%>');
            var institute = document.getElementById('<%=txtInstituteName.ClientID%>');
            var mobile = document.getElementById('<%=txtMobile.ClientID%>');
            var email = document.getElementById('<%=txtEmail.ClientID%>');
            var desig = document.getElementById('<%=txtDesig.ClientID%>');
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
    <asp:UpdatePanel runat="server" ID="udpphd" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1" id="tab1">Internal Member Mapping</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Add External Member</a>
                                </li>
                            </ul>

                            <div class="tab-content" id="my-tab-content">
                                <div class="tab-pane active " id="tab_1">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updInternalMember" DynamicLayout="true" DisplayAfter="0">
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
                                    <asp:UpdatePanel ID="updInternalMember" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                         <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>College</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlInsName" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlInsName_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Department</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>                                                       
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="DivFacuilty" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Internal Faculty</label>
                                                            </div>
                                                            <asp:ListBox ID="lboFacuilty" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Designation</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" SetFocusOnError="true" ValidationGroup="SubmitMapping"
                                                                CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Supervisor</asp:ListItem>
                                                                <asp:ListItem Value="2">DRC Member</asp:ListItem>
                                                                <asp:ListItem Value="3">Supervisor & DRC Member</asp:ListItem>
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation"
                                                                Display="None" ErrorMessage="Please Select Designation" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit"
                                                        CssClass="btn btn-primary" ValidationGroup="SubmitMapping" TabIndex="3" OnClientClick=" return validateForm(this);" OnClick="btnSubmit_Click" />

                                                    <asp:Button ID="btnCancel1" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" OnClick="btnCancel1_Click"
                                                        CssClass="btn btn-warning" TabIndex="4" />
                                                </div>
                                                <div class="col-12 mt-3">

                                                    <div class="table-responsive">
                                                        <asp:Panel ID="PnMapping" runat="server" Visible="false">
                                                            <div class="sub-heading">
                                                                <h5>Internal Member Mapping</h5>
                                                            </div>
                                                            <asp:ListView ID="lvMapping" runat="server">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="lvMapping">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">Edit
                                                                                </th>
                                                                                <th>
                                                                                    Department
                                                                                </th>
                                                                                <th>
                                                                                    College
                                                                                </th>
                                                                                <th>Faculty
                                                                                </th>
                                                                                <th>Designation
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
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click1" TabIndex="1" 
                                                                                 CausesValidation="false" CommandArgument='<%#Eval("DESIG_UANO_ID")%>' />
                                                                        </td>
                                                                    <td>
                                                                        <%# Eval("DEPTNAME")%>
                                                                    </td>
                                                                        <td>
                                                                            <%# Eval("COLLEGE_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("FACULTY_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DESIGNATION")%>
                                                                        </td>
                                                                    </tr>

                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>

                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane " id="tab_2">

                                    <div>
                                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDist" DynamicLayout="true" DisplayAfter="0">
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

                                    <asp:UpdatePanel ID="updDist" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtName" runat="server" ToolTip="Please enter name." TabIndex="1" MaxLength="128" AutoComplete="off" placeholder="Please enter name" CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ajtbeName" runat="server" TargetControlID="txtName" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_-+={[}]:;<,>?/'|\1234567890."></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Institute Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtInstituteName" runat="server" ToolTip="Please enter institute name." TabIndex="1" MaxLength="128" AutoComplete="off" placeholder="Please enter institute name" CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ajtbeInstituteName" runat="server" TargetControlID="txtInstituteName" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_+={[}]:;<,>?'|\"></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Mobile No</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMobile" runat="server" ToolTip="Please enter mobile no." TabIndex="1" MaxLength="10" AutoComplete="off" placeholder="Please enter mobile no." CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMobile" FilterMode="ValidChars" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Email Id</label>
                                                            </div>
                                                            <asp:TextBox ID="txtEmail" runat="server" ToolTip="Please enter email id." TabIndex="1" MaxLength="64" AutoComplete="off" placeholder="Please enter email id" CssClass="form-control" onblur="validateEmail(this);"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtEmail" FilterMode="InvalidChars" InvalidChars="~`!#$%^&*()-+={}][:;'<,>?/|\"></ajaxToolKit:FilteredTextBoxExtender>
                                                            <%--                                                              <asp:RegularExpressionValidator ID="revEmailId1" runat="server" ControlToValidate="txtEmail"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid Email ID." ValidationGroup="Submit"></asp:RegularExpressionValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Designation</label>
                                                            </div>
                                                            <asp:TextBox ID="txtDesig" runat="server" ToolTip="Please enter designation." TabIndex="1" MaxLength="128" AutoComplete="off" placeholder="Please enter designation" CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtDesig" FilterMode="InvalidChars" InvalidChars="~`!#$%^&*()_+={}][:;'<,>?|\"></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ToolTip="Click to save." TabIndex="1" OnClick="btnSave_Click" OnClientClick="return Validate(this);" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click to cancel." OnClick="btnCancel_Click" TabIndex="1" CausesValidation="false" />

                                                    </p>
                                                </div>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlMember" runat="server" Visible="false">
                                                        <asp:ListView ID="lvMember" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>External Member List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center;">Edit
                                                                            </th>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>Institute Name
                                                                            </th>
                                                                            <th>Mobile No
                                                                            </th>
                                                                            <th>Email Id
                                                                            </th>
                                                                            <th>Designation
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
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="1" CommandArgument='<%#Eval("DESIG_NO")%>' CausesValidation="false" />
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("NAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("INSTITIUTE_NAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("MOBILE_NO") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("EMAIL_ID") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("DESIGNATION") %>
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
                                        </ContentTemplate>
                                        <Triggers>
                                            <%--<asp:PostBackTrigger ControlID="btnCancel" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
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
        function validateForm() {
            var alerMsg = "";
            var Dept = document.getElementById('<%=ddlDepartment.ClientID%>');
            var Dept_value = Dept.value;
            var listBox = document.getElementById('<%= lboFacuilty.ClientID %>');
            var selectedValue = listBox.value;
            var college = document.getElementById('<%=ddlInsName.ClientID%>');
            var college_value = college.value;
            var designation = document.getElementById('<%=ddlDesignation.ClientID%>');
             var designation_value = designation.value;
             if (Dept_value == "0")
                 alerMsg = "Please Select Department.\n";
             if (college_value == "0")
                 alerMsg += "Please Select College.\n";
             if (selectedValue === '')
                 alerMsg += "Please Select Atleast One Faculty.\n";
             if (designation_value == "0")
                 alerMsg += "Please Select Designation.\n";

             if (alerMsg != '') {
                 alert(alerMsg);
                 return false;
             }
         }
    </script>
</asp:Content>
