<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="ExamStaffEntry.aspx.cs" Inherits="ACADEMIC_ANSWERSHEET_ExamStaffEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        function ValidateCheckBoxList(sender, args) {
            var checkBoxList = document.getElementById('<%=chkStaffType.ClientID %>');
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }
    </script>


    <p class="page_help_text">
        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
    </p>
    <style>
        #ctl00_ContentPlaceHolder1_ceStartDate_popupDiv, ctl00_ContentPlaceHolder1_CalendarExtender2_popupDiv {
            z-index: 100;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EVALUATION STAFF ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYStaffType" runat="server" Font-Bold="true"></asp:Label>
                                            <%-- <label>Staff Type</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlStaffType" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStaffType" runat="server" ControlToValidate="ddlStaffType"
                                            Display="None" ErrorMessage="Please select Staff Type" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please select Department" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>

                                            <asp:Label ID="lblDYStaffName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlName" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlName" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Name" ControlToValidate="ddlName"
                                            Display="None" ValidationGroup="submit" />

                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2" Visible="false" />
                                        <asp:HiddenField ID="hdnEditedRecord" runat="server" Value="0" />
                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Name" ControlToValidate="txtName"
                                            Display="None" ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYMobileNo" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%--/^(\+\d{1,3}[- ]?)?\d{10}$/--%>
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10" TabIndex="4" />
                                        <%--onblur="ch();"--%>
                                        <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile"
                                            Display="None" ErrorMessage="Please Enter Mobile No." ValidationGroup="submit"
                                            SetFocusOnError="true" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDays" runat="server"
                                            TargetControlID="txtMobile"
                                            FilterType="Custom"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="revMobile" runat="server"
                                            ErrorMessage="Invalid Mobile Number" ControlToValidate="txtMobile" SetFocusOnError="True" ValidationGroup="submit"
                                            ValidationExpression="^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$">
                                        </asp:RegularExpressionValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYEmail" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" TabIndex="5" />
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Email" ControlToValidate="txtEmail"
                                            Display="None" ValidationGroup="submit" />
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                            ErrorMessage="Invalid Email" ControlToValidate="txtEmail" SetFocusOnError="True" ValidationGroup="submit"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" visible="false" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>City</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCity" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                            Display="None" ErrorMessage="Please select City" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYAddress" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" MaxLength="100" TabIndex="6" TextMode="MultiLine" />
                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Address" ControlToValidate="txtAddress"
                                            Display="None" ValidationGroup="submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Type</label>
                                        </div>
                                        <asp:CheckBoxList ID="chkStaffType" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                                        </asp:CheckBoxList>
                                        <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one item."
                                            ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList();" runat="server" />
                                    </div>
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal"
                                            TabIndex="7">
                                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="De-Active" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Status" ControlToValidate="rblStatus"
                                            Display="None" ValidationGroup="submit" />
                                    </div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="switch form-inline" onclick="clickRdActive()">
                                            <input type="checkbox" id="rblStatus" runat="server" name="switch" checked />
                                            <label data-on="Active" data-off="DeActive" for="rblStatus"></label>
                                            <asp:HiddenField ID="hdfStatus" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <label>Other</label>
                                        <asp:CheckBox runat="server" ID="chkOther" OnCheckedChanged="chkOther_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8"
                                    ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false"
                                    ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />--%>
                                <asp:Button ID="btnStaffAdd" runat="server" Text="Staff Address" TabIndex="9" CssClass="btn btn-primary" OnClick="btnStaffAdd_Click" />
                                <asp:Button ID="btnAquitancerpt" runat="server" Text="ACQAINTANCE REPORT" TabIndex="10" CssClass="btn btn-info" OnClick="btnAquitancerpt_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11" CssClass="btn btn-warning"
                                    OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="valSummary" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlSession" runat="server">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <asp:Repeater ID="lvSession" runat="server">
                                            <HeaderTemplate>
                                                <thead>
                                                    <tr>
                                                        <th>Edit </th>
                                                        <th>Department</th>
                                                        <th>Staff Name</th>
                                                        <th>Mobile No</th>
                                                        <th>Email</th>
                                                        <%--<th>Address</th>--%>
                                                        <th>Staff Type </th>
                                                        <th>Status </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("EXAM_STAFF_NO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="11" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STAFF_NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MOBILE_NO")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("EMAIL_ID")%>  

                                                    </td>
                                                    <%-- <td>
                                                        <%# Eval("COLLEGE_ADDRESS")%>
                                                    </td>--%>
                                                    <td>
                                                        <%# Eval("STAFF_TYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACTIVE")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="bg-light-red">
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("EXAM_STAFF_NO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="11" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STAFF_NAME")%>                                                       
                                                    </td>
                                                    <td>
                                                        <%# Eval("MOBILE_NO")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("EMAIL_ID")%>  

                                                    </td>
                                                    <%--<td>
                                                        <%# Eval("COLLEGE_ADDRESS")%>
                                                    </td>--%>
                                                    <td>
                                                        <%# Eval("STAFF_TYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ACTIVE")%>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                          
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMsg" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function ch() {
            debugger;
            var mobileno = document.getElementById('<%=txtMobile.ClientID%>')
            var length = mobileno.textLength;
            if (length != 0) {
                if (length != 10) {
                    alert("Mobile Number Should be 10 Digit.");
                    document.getElementById('<%=txtMobile.ClientID%>').value = "";
                    return true;
                }
            }


        }
    </script>
    <script>
        function SetStatActive(val) {
            var examreg = document.getElementById("rblStatus");
            if (examreg.checked) {
                $('#hdfStatus').val(true);
            }

        }

        function clickRdActive() {
            if ($('#ctl00_ContentPlaceHolder1_rblStatus').is(':checked')) {
                $('#ctl00_ContentPlaceHolder1_rblStatus').prop('checked', false);
            }
            else {
                $('#ctl00_ContentPlaceHolder1_rblStatus').prop('checked', true);
            }
        }
    </script>
    <script>

        //function validateCol() {

        //    var ddlStaffType = $("[id$=ddlStaffType]").attr("id");
        //    var ddlOrgToMap = document.getElementById(ddlStaffType);
        //    if (ddlOrgToMap.value == 0) {
        //        alert('Please Select Staff Type', 'Warning!');
        //        //$(ddlUniversity).css('border-color', 'red');
        //        $(ddlStaffType).focus();
        //        return false;
        //    }

        //}

        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_endRequest(function () {
        //    $(function () {
        //        $('#btnSubmit').click(function () {
        //            validateCol();
        //        });
        //    });
        //});

    </script>

</asp:Content>
