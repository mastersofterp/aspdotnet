<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DualDegreeRegistrationEntry.aspx.cs" Inherits="ACADEMIC_DualDegreeRegistrationEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../plugins/jQuery/jquery_ui_min/jquery-ui.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery_ui_min/jquery-ui.min.js"></script>
    <script type="text/javascript">
        if (document.layers) {
            //Capture the MouseDown event.
            document.captureEvents(Event.MOUSEDOWN);

            //Disable the OnMouseDown event handler.
            document.onmousedown = function () {
                return false;
            };
        }
        else {
            //Disable the OnMouseUp event handler.
            document.onmouseup = function (e) {
                if (e != null && e.type == "mouseup") {
                    //Check the Mouse Button which is clicked.
                    if (e.which == 2 || e.which == 3) {
                        //If the Button is middle or right then disable.
                        return false;
                    }
                }
            };
        }

        //Disable the Context Menu event.
        document.oncontextmenu = function () {
            return false;
        };
    </script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updStudent"
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

    <div id="dvStudent" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div12" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    </div>
                    <asp:Panel ID="pnlbody" runat="server">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <div class="col-12" id="divsearchbar" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Search Criteria</label>
                                            </div>
                                            <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true"
                                                data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                            <asp:Panel ID="pnltextbox" runat="server">
                                                <div id="divtxt" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlDropdown" runat="server">
                                                <div id="divDropDown" runat="server" style="display: block">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search String</label>
                                                    </div>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="Show" OnClientClick="return submitPopup(this.name);" />
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Visible="false" />
                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlLV" runat="server">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Sr. No.
                                                                        </th>
                                                                        <th>Name
                                                                        </th>
                                                                        <th>Adm. Status
                                                                        </th>
                                                                        <th style="display: none">IdNo
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                        <th>
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
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAdmcan" Font-Bold="true" runat="server" ForeColor='<%# Eval("ADMCANCEL").ToString().Equals("ADMITTED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ADMCANCEL")%>'></asp:Label>
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
                                                        <%# Eval("SEMESTERNO")%>
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

                                <div class="form-group col-lg-6 col-md-12 col-12">
                                    <asp:Label ID="lblNote" Font-Bold="true" Visible="false" Text="Note: Please do not refresh page Or Do not search new student once you processed demand for current student." Style="color: red;" runat="server" SkinID="lblmsg"></asp:Label>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lvStudent" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </asp:Panel>


                    <div class="box-body">
                        <asp:Panel ID="pnlstudetails" runat="server">
                            <asp:UpdatePanel ID="updStudent" runat="server">
                                <ContentTemplate>
                                    <div class="col-12" id="divtypeofStudent" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Type of Student</h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12 ml-2">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Type of Student</label>
                                                </div>
                                                <asp:DropDownList ID="ddlTypeofStudent" runat="server" TabIndex="7" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTypeofStudent_SelectedIndexChanged"
                                                    CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Type of Student">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Dual Degree</asp:ListItem>
                                                    <asp:ListItem Value="2">Higher Education</asp:ListItem>
                                                    <asp:ListItem Value="3">Sibling</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12 ml-2">
                                                 <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:Button ID="Button1" runat="server" Text="Back" CssClass="btn btn-warning" OnClick="btnBack_Click"  />
                                            </div>

                                        </div>
                                    </div>

                                    <div class="box-body" id="divSearchInfo" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12" id="divStudentfullName" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supStudentfullName" runat="server">* </sup>
                                                        <label>Name of the Student <span style="color: red; font: 100; font-size: 11px;">(as per 10<sup>th</sup> marksheet)</span></label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudentfullName" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Please Enter Student Full Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" placeholder="Please Enter Student Full Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteStudent" runat="server" TargetControlID="txtStudentfullName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    <asp:RequiredFieldValidator ID="rfvStudent" runat="server" ControlToValidate="txtStudentfullName"
                                                        Display="None" ErrorMessage="Please Enter Student Full Name" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divFatherName" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supFatherName" runat="server"></sup>
                                                        <label>Father's Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" TabIndex="145" ToolTip="Please Enter Father's Name." onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" placeholder="Please Enter Father's Name." />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />
                                                    <%-- <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ControlToValidate="txtFatherName"
                                                        Display="None" ErrorMessage="Please Enter Father's Name" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divStudMobile" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supStudMobile" runat="server">*</sup>
                                                        <label>Student Mobile No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudMobile" runat="server" CssClass="form-control" TabIndex="4" MaxLength="10" onkeyup="validateNumeric(this);"
                                                        placeholder="Please Enter Student Mobile No."></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="rfvStudMobile" runat="server" ControlToValidate="txtStudMobile"
                                                        Display="None" ErrorMessage="Please Enter Student Mobile No " ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Mobile No. is Invalid" ID="revMobile" ControlToValidate="txtStudMobile"
                                                        ValidationExpression="^[\s\S]{8,}$" Display="Dynamic" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator Display="None" ControlToValidate="txtStudMobile" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{10}$" runat="server" ErrorMessage="Minimum 10 characters required For Mobile No.." ValidationGroup="academic"></asp:RegularExpressionValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtStudMobile" FilterType="Custom" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" />

                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divStudentMoblieNo2" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supStudentMoblieNo2" runat="server"></sup>
                                                        <label>Student Mobile No. 2</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudMobile2" runat="server" CssClass="form-control" TabIndex="5" MaxLength="10" onkeyup="validateNumeric(this);"
                                                        placeholder="Please Enter Student Mobile No.2"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvStudMobile2" runat="server" ControlToValidate="txtStudMobile2"
                                                        Display="None" ErrorMessage="Please Enter Student Mobile No. 2 " ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Mobile No. 2 is Invalid" ID="RegularExpressionValidator1" ControlToValidate="txtStudMobile2"
                                                        ValidationExpression="^[\s\S]{8,}$" Display="Dynamic" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator Display="None" ControlToValidate="txtStudMobile2" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{10,}$" runat="server" ErrorMessage="Minimum 10 characters required For Mobile No. 2" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtStudMobile2" FilterType="Custom" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" />
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divParentMobileNo" runat="server">
                                                    <div class="label-dynamic">

                                                        <sup id="supParentMobileNo" runat="server">*</sup>
                                                        <label>Parent Mobile No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtParentmobno" runat="server" CssClass="form-control" TabIndex="5" MaxLength="10" onkeyup="validateNumeric(this);"
                                                        placeholder="Please Enter Parent Mobile No."></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="rfvparentmobno" runat="server" ControlToValidate="txtParentmobno"
                                                        Display="None" ErrorMessage="Please Enter Parent Mobile No." ValidationGroup="academic"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                                                    <asp:RegularExpressionValidator runat="server" Visible="false" ErrorMessage="Parent Mobile No. is Invalid" ID="RegularExpressionValidator4" ControlToValidate="txtParentmobno"
                                                        ValidationExpression="^[\s\S]{8,}$" Display="Dynamic" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                                    <asp:RegularExpressionValidator Display="None" ControlToValidate="txtParentmobno" ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{10,}$" runat="server" ErrorMessage="Minimum 10 characters required For Parent Mobile No." ValidationGroup="academic"></asp:RegularExpressionValidator>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtParentmobno" FilterType="Custom" FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9" />
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPersonalEmail" runat="server">
                                                    <div class="label-dynamic">
                                                        <%--<sup>*</sup>--%>
                                                        <sup id="supPersonalEmail" runat="server"></sup>
                                                        <label>Personal Email</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudEmail" runat="server" ToolTip="Please Enter Personal Email" CssClass="form-control"
                                                        TabIndex="6" placeholder="Please Enter Student Personal Email." />


                                                    <asp:RegularExpressionValidator ID="revStudEmail" runat="server" ControlToValidate="txtStudEmail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>


                                                    <%-- <asp:RequiredFieldValidator ID="rfvStudEmail" runat="server" ControlToValidate="txtStudEmail"
                                                        Display="None" ErrorMessage="Please Enter Student Personal Email " ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divstate" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supstate" runat="server"></sup>
                                                        <label>State</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlstate" runat="server" TabIndex="7" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select State">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvstate" runat="server" ControlToValidate="ddlstate"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select State"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCity" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supCity" runat="server"></sup>
                                                        <label>City</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCity" runat="server" TabIndex="7" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select City">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select City"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divGender" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supGender" runat="server"></sup>
                                                        <label>Select Gender</label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoMale" runat="server" GroupName="Sex" TabIndex="8" Text="Male" />

                                                    <asp:RadioButton ID="rdoFemale" runat="server" GroupName="Sex" TabIndex="9" Text="Female" />

                                                    <asp:RadioButton ID="rdoTransGender" runat="server" GroupName="Sex" TabIndex="10" Text="Others" />

                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSchool" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supSchool" runat="server"></sup>

                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSchool" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select School Admitted." TabIndex="11" AppendDataBoundItems="True" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvSchool" runat="server" ControlToValidate="ddlSchool"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Institute School"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supDegree" runat="server"></sup>

                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Degree / Program" TabIndex="12" AppendDataBoundItems="True" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Degree / Program"
                                                    ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supBranch" runat="server"></sup>
                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranchShow" runat="server" TabIndex="13" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        ValidationGroup="academic" ToolTip="Please Select Branch" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                                    ErrorMessage="Please Select Branch" Display="None" ValidationGroup="academic"
                                                    SetFocusOnError="true" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvDD_Branch" runat="server" ControlToValidate="ddlBranch"
                                                    ErrorMessage="Please Select Branch" Display="None" ValidationGroup="ddinfo" SetFocusOnError="true"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>--%>
                                                </div>



                                                <%--<div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSpecialisation">
                                                <div class="label-dynamic">
                                                    <sup id="supSpecialisation" runat="server">* </sup>
                                                    <label>Specialisation</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSpecialisation" runat="server" TabIndex="13" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    ValidationGroup="academic" ToolTip="Please Select Specialisation">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSpecialisation" runat="server" ControlToValidate="ddlSpecialisation"
                                                    Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Specialisation"
                                                    ValidationGroup="academic"></asp:RequiredFieldValidator>

                                            </div>--%>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divadmthrough" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supadmthrough" runat="server"></sup>
                                                        <label>Admission Through</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddladmthrough" runat="server" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Type" TabIndex="14">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvadmthrough" runat="server" ControlToValidate="ddladmthrough"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Through"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAdmType" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supAdmType" runat="server"></sup>
                                                        <label>Admission Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnTextChanged="ddlAdmType_TextChanged"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Type" TabIndex="15">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvAdmission" runat="server" ControlToValidate="ddlAdmType"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Type"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divYear" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supYear" runat="server"></sup>
                                                        <label>Admission Year</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlYear" runat="server" TabIndex="16" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Year" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" />
                                                    <%--asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Year"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSemester" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supSemester" runat="server"></sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="17" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" />
                                                    <%-- <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Semester"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divBatch" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supBatch" runat="server"></sup>
                                                        <label>Admission Batch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBatch" runat="server" TabIndex="18" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Batch" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="true" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Batch"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvddino_batch" runat="server" ControlToValidate="ddlBatch"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Batch"
                                                        ValidationGroup="ddinfo"></asp:RequiredFieldValidator>--%>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student First Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudentName" runat="server" TabIndex="16"
                                                        ToolTip="Please Enter Student Name" ValidationGroup="academic" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        TargetControlID="txtStudentName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />

                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student Middle Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudentMiddleName" runat="server" TabIndex="122"
                                                        ToolTip="Please Enter Student Middle Name" ValidationGroup="academic" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        TargetControlID="txtStudentMiddleName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Father Middle Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtFatherMiddleName" runat="server" TabIndex="3123" ToolTip="Please Enter Father Middle Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                        TargetControlID="txtFatherMiddleName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student Last Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudentLastName" runat="server" TabIndex="123"
                                                        ToolTip="Please Enter Student Last Name" ValidationGroup="academic" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        TargetControlID="txtStudentLastName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Father Last Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtFatherLastName" runat="server" TabIndex="21323" ToolTip="Please Enter Father Last Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                        TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Mother's Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMotherName" runat="server" TabIndex="3132" ToolTip="Please Enter Mother's name." onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        TargetControlID="txtMotherName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Date of Birth</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="txtDateOfBirth1">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDateOfBirth" runat="server" TabIndex="19" ValidationGroup="academic" />

                                                        <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDateOfBirth" PopupButtonID="txtDateOfBirth1" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" EmptyValueMessage="Please Enter DateOfBirth"
                                                            ControlExtender="meeDateOfBirth" ControlToValidate="txtDateOfBirth" IsValidEmpty="true"
                                                            InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="academic" SetFocusOnError="true" />

                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Religion</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReligion" runat="server" TabIndex="1325" AppendDataBoundItems="true"
                                                        ToolTip="Please Select Religion" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Nationality</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlNationality" runat="server" TabIndex="1326" AppendDataBoundItems="true"
                                                        ToolTip="Please Select Nationality" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>SR Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Caste Category">
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Claimed Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlClaimedCat" runat="server" TabIndex="5" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Claimed Category" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Entrance Exam</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamNo" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlExamNo_SelectedIndexChanged"
                                                        CssClass="form-control" data-select2-enable="true" TabIndex="14">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divotherentrance" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supotherentrance" runat="server"></sup>
                                                        <label>Other Entrance Exam</label>
                                                    </div>
                                                    <asp:TextBox ID="txtothetentrance" runat="server" TabIndex="8213" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvothetentrance" runat="server" ControlToValidate="txtothetentrance"
                                                        Display="None" ErrorMessage="Please Enter Other Entrance Exam" ValidationGroup="academic"
                                                        SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>CET/COMEDK Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtcetcomdate" runat="server" TabIndex="16" onchange="CheckFutureDate(this);" CssClass="form-control" placeholder="dd/MM/yyyy" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtcetcomdate" PopupButtonID="imgCalDateOfAdmission" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Fees Paid at KEA/COMEDK</label>
                                                    </div>
                                                    <asp:TextBox ID="txtfeepaid" runat="server" TabIndex="12" ToolTip="Please Enter Fees." onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10"
                                                        runat="server" FilterType="Numbers" TargetControlID="txtfeepaid">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Martial Status</label>
                                                    </div>
                                                    <asp:RadioButton ID="rdoMarriedYes" runat="server" GroupName="Married"
                                                        TabIndex="11" />
                                                    Married
                                            <asp:RadioButton ID="rdoMarriedNo" runat="server" GroupName="Married"
                                                TabIndex="12" Checked="true" />
                                                    Unmarried
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Student Indus Email</label>
                                                    </div>
                                                    <asp:TextBox ID="txtIUEmail" runat="server" ToolTip="Please Enter Student Indus Email"
                                                        TabIndex="14" />


                                                    <asp:RegularExpressionValidator ID="revIuEmail" runat="server" ControlToValidate="txtIUEmail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Blood Group</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBloodGrp" runat="server" TabIndex="20" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Blood Group" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Physical Handicap</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPhyHandicap" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                        TabIndex="17">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAllotedCat" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supAllotedCat" runat="server"></sup>
                                                        <label>Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAllotedCat" runat="server" TabIndex="21" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Category" />
                                                    <%-- <asp:RequiredFieldValidator ID="rfvAllotedCategory" runat="server" ControlToValidate="ddlAllotedCat"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Category"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Date of Admission</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDateOfAdmission" runat="server" TabIndex="19" onchange="CheckFutureDate(this);" CssClass="form-control" placeholder="dd/MM/yyyy" Visible="false" />

                                                        <ajaxToolKit:CalendarExtender ID="ceDateOfAdmission" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDateOfAdmission" PopupButtonID="imgCalDateOfAdmission" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>

                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Entrance Roll No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtJeeRollNo" runat="server" TabIndex="1866" MaxLength="15" placeholder="Please Enter Entrance exam Roll No"></asp:TextBox>

                                                </div>



                                                <%--  <div class="form-group col-lg-3 col-md-6 col-12" id="divDateOfReporting" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supDateOfReporting" runat="server">* </sup>
                                                        <label>Date of Entry</label>
                                                    </div>
                                                    <asp:TextBox ID="txtDateOfReporting" runat="server" TabIndex="16" CssClass="form-control" placeholder="dd/MM/yyyy" />

                                                       <asp:RequiredFieldValidator ID="rfvDateOfReporting" runat="server" ControlToValidate="txtDateOfReporting"
                                                        Display="none" ErrorMessage="Please Enter Date of Entry" SetFocusOnError="true"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator> 
                                                   <ajaxToolKit:CalendarExtender ID="ceDateOfReporting" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateOfReporting" PopupButtonID="imgCalDateOfAdmission" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>

                                                </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Rank</label>
                                                    </div>
                                                    <asp:TextBox ID="txtJeeRankNo" runat="server" TabIndex="1055" MaxLength="10"></asp:TextBox>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteRank"
                                                        runat="server" FilterType="Numbers" TargetControlID="txtJeeRankNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>CET/COMEDK Order No</label>
                                                    </div>
                                                    <asp:TextBox ID="txtcetcomorederno" runat="server" TabIndex="1355" ToolTip="Please Enter CET/COMEDK Order No." onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" />

                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPaymentType" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supPaymentType" runat="server"></sup>
                                                        <label>Payment Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPaymentType" runat="server" TabIndex="23" AppendDataBoundItems="true" AutoPostBack="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Category for Payment" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" />

                                                    <%--<asp:RequiredFieldValidator ID="rfvPaymentType" runat="server" ControlToValidate="ddlPaymentType"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Payment Type"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden="hidden">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Section</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSection" runat="server" TabIndex="2145" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Semester" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Aadhaar No.</label>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txtAadhaarNo" TabIndex="24" placeholder="Please Enter Aadhaar no." AutoPostBack="true" MaxLength="12" onkeyup="IsNumeric(this);" OnTextChanged="txtAadhaarNo_TextChanged"></asp:TextBox>

                                                </div>



                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="divapplicationid" runat="server">
                                                <div class="label-dynamic">
                                                    <sup id="supapplicationid" runat="server"></sup>
                                                    <label>Application ID</label>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtapplicationid" TabIndex="25" CssClass="form-control" placeholder="Please Enter Application ID" MaxLength="12" ReadOnly="true"></asp:TextBox>
                                            </div>--%>


                                                <%--<div class="form-group col-lg-3 col-md-6 col-12" id="divmerirtno" runat="server">
                                                <div class="label-dynamic">
                                                    <sup id="supmerirtno" runat="server">* </sup>
                                                    <label>Merit No.</label>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtmerirtno" TabIndex="26" CssClass="form-control" placeholder="Please Enter Merit no." AutoPostBack="true" MaxLength="10" onkeyup="IsNumeric(this);" Enabled="false"></asp:TextBox>

                                                <asp:RequiredFieldValidator ID="rfvmerirtno" runat="server" ControlToValidate="txtmerirtno"
                                                    Display="None" ErrorMessage="Please Enter Merit No." ValidationGroup="academic"
                                                    SetFocusOnError="true" />
                                            </div>--%>



                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="divScore" runat="server">
                                                <div class="label-dynamic">
                                                    <sup id="supScore" runat="server">* </sup>
                                                    <label>Score</label>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtscore" TabIndex="27" CssClass="form-control" placeholder="Please Enter Score" MaxLength="12" onkeyup="IsNumeric(this);" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvscore" runat="server" ControlToValidate="txtscore"
                                                    Display="None" ErrorMessage="Please Enter Score" ValidationGroup="academic"
                                                    SetFocusOnError="true" />
                                            </div>--%>

                                                <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" hidden="hidden" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>College Code</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlcolcode" runat="server" TabIndex="28" AppendDataBoundItems="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select College Code">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        <asp:ListItem Value="1">E021</asp:ListItem>
                                                        <asp:ListItem Value="2">E057</asp:ListItem>
                                                        <asp:ListItem Value="3">B292</asp:ListItem>
                                                        <asp:ListItem Value="4">B292BC</asp:ListItem>
                                                        <asp:ListItem Value="5">B292BR</asp:ListItem>
                                                        <asp:ListItem Value="6">E721 </asp:ListItem>
                                                        <asp:ListItem Value="7">B292BD</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>



                                                <%--<div class="form-group col-lg-3 col-md-6 col-12" id="divAppliedFees" runat="server">
                                                <div class="label-dynamic">
                                                    <sup id="supAppliedFees" runat="server">* </sup>
                                                    <label>Total Applicable Fees</label>
                                                </div>

                                                <div>
                                                    <asp:TextBox runat="server" ID="txtAppliedFees" TabIndex="27" CssClass="form-control" placeholder="Applicable Fees" MaxLength="12"
                                                        Enabled="false"></asp:TextBox>

                                                </div>
                                            </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>State</label>
                                                    </div>
                                                    <asp:TextBox ID="txtState" runat="server" class="tbState" CssClass="form-control" ToolTip="Please Enter State" TabIndex="24" />

                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12" id="trphoto_sign" runat="server" style="display: none">
                                            <div class="label-dynamic" id="trphotosign">
                                                <sup></sup>
                                                <label>Photo And Sign Upload</label>
                                            </div>
                                            <div class="col-12" id="divUpload" runat="server">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-6 col-12">
                                                        <label></label>
                                                        <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px" />
                                                        <asp:FileUpload ID="fuPhotoUpload" runat="server" TabIndex="19" onChange="LoadImage()" />
                                                    </div>
                                                    <div class="col-lg-3 col-md-6 col-12">
                                                        <label>Sign Upload</label>
                                                        <asp:Image ID="ImgSign" runat="server" Width="125px" Height="40px" />
                                                        <asp:FileUpload ID="fuSignUpload" runat="server" onChange="LoadImageSign()" TabIndex="20" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-12 col-12" style="display: none">
                                            <div class=" note-div">
                                                <h5 class="heading">Note (Please Select)</h5>
                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Don't forget to print admission slip after submission of New Student entry.</span>  </p>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-3" id="divAddressAndContactDetails" style="display: block;">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Permenant Address</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Permanent Address</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPermanentAddress" runat="server" TabIndex="21" Rows="1" CssClass="form-control"
                                                        TextMode="MultiLine" ToolTip="Please Enter Permanent Address"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label for="city">City/Village</label>
                                                    </div>
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" ToolTip="Please Enter City"
                                                        TabIndex="14" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label for="city">ZIP/PIN</label>
                                                    </div>

                                                    <asp:TextBox ID="txtPIN" runat="server" TabIndex="15" MaxLength="6" ToolTip="Please Enter PIN" CssClass="form-control" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtPin" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtPIN">
                                                    </ajaxToolKit:FilteredTextBoxExtender>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Mobile No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" ToolTip="Please Enter Mobile No." TabIndex="15" MaxLength="12" CssClass="form-control" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteMobilenum" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMobileNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:CompareValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                        ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                        Type="Integer" ValidationGroup="Academic" Display="None" Visible="False">
                                                    </asp:CompareValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Contact No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtContactNumber" runat="server" TabIndex="16" ValidationGroup="academic" CssClass="form-control"
                                                        MaxLength="20" ToolTip="Please Enter Contact Number" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtContactNum" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtContactNumber">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                            </div>


                                            <div id="divGuardianAddress" class="row mt-3">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Local Address and Contact Details 
                                                        (Copy Address) 
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/copy.png" OnClientClick="copyPermAddr(this)"
                                                            ToolTip="Copy Permanent Address" TabIndex="16" Visible="false"/></label>
                                                        </h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>
                                                            Detailed Address 
                                                        </label>
                                                    </div>
                                                    <asp:TextBox ID="txtPostalAddress" runat="server" TabIndex="16" CssClass="form-control" Rows="1" TextMode="MultiLine"
                                                        ToolTip="Please Enter Postal Address"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>City/Villege</label>
                                                    </div>

                                                    <asp:TextBox ID="txtLocalCity" runat="server" CssClass="form-control" ToolTip="Please Enter Local City"
                                                        TabIndex="16" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>State</label>
                                                    </div>

                                                    <asp:TextBox ID="txtLocalState" runat="server" CssClass="form-control" ToolTip="Please Enter Local State"
                                                        TabIndex="24" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Mobile No.</label>
                                                    </div>

                                                    <asp:TextBox ID="txtGuardianMobile" runat="server" CssClass="form-control" ToolTip="Please Enter Guardian Mobile No."
                                                        TabIndex="16" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtGuardianMobile">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:CompareValidator ID="rfvGuardianMobileNo" runat="server" ControlToValidate="txtGuardianMobile"
                                                        ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                        Type="Integer" ValidationGroup="academic" Display="None"></asp:CompareValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Contact No.</label>
                                                    </div>

                                                    <asp:TextBox ID="txtGuardianPhone" runat="server" CssClass="form-control" ToolTip="Please Enter Guardian Phone No."
                                                        TabIndex="16" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Email Id</label>
                                                    </div>
                                                    <asp:TextBox ID="txtGuardianEmail" runat="server" CssClass="form-control" TabIndex="16" ToolTip="Please Enter Email" />
                                                    <asp:RegularExpressionValidator ID="rfvGuardianEmail" runat="server" ControlToValidate="txtGuardianEmail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>New Admission </h5>
                                                    </div>

                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div3" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="sup1" runat="server">* </sup>
                                                        <label>Admission Batch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="18" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Batch" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAdmBatch"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Batch"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>

                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>College</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeNew" runat="server" ValidationGroup="academic" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select  College" TabIndex="12" AppendDataBoundItems="True" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCollegeNew_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollegeNew"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select College"
                                                        ValidationGroup="academic">
                                                    </asp:RequiredFieldValidator>
                                                </div>



                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegreeNew" runat="server" ValidationGroup="academic" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Degree / Program" TabIndex="12" AppendDataBoundItems="True" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDegreeNew_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegreeNew"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Degree / Program"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" ValidationGroup="academic" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Branch" TabIndex="12" AppendDataBoundItems="True" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Branch"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSpecialisation" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup id="supSpecialisation" runat="server"></sup>
                                                        <label>Specialisation</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSpecialisation" runat="server" TabIndex="13" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Specialisation">
                                                        <%--ValidationGroup="academic"--%>
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- Commeneted By Vinay Mishra on 21/06/2023 As per Required in Ticket 44723--%>
                                                    <%--<asp:RequiredFieldValidator ID="rfvSpecialisation" runat="server" ControlToValidate="ddlSpecialisation"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Specialisation"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="sup2" runat="server">* </sup>
                                                        <label>Semester</label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlSemesterNew" runat="server" TabIndex="17" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlSemesterNew_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemesterNew"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Semester"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div5" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="sup3" runat="server">*</sup>
                                                        <label>Admission Type</label>
                                                    </div>

                                                    <asp:DropDownList ID="ddlAdmtypeNew" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnTextChanged="ddlAdmType_TextChanged"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Type" TabIndex="15">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAdmission" runat="server" ControlToValidate="ddlAdmtypeNew"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Admission Type"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div6" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="sup4" runat="server">*</sup>
                                                        <label>Payment Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPaymentTypeNew" runat="server" TabIndex="23" AppendDataBoundItems="true" AutoPostBack="true"
                                                        CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Payment Type" />

                                                    <asp:RequiredFieldValidator ID="rfvPaymentTypeNew" runat="server" ControlToValidate="ddlPaymentTypeNew"
                                                        Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Payment Type"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">

                                            <asp:Button ID="btnSave" runat="server" Text="Submit" TabIndex="38" ToolTip="Submit Student Information"
                                                ValidationGroup="academic" CssClass="btn btn-primary" OnClick="btnSave_Click" />

                                            <asp:Button ID="btnCancel" runat="server" Text="Reset" TabIndex="40" ToolTip="Cancel Student Information"
                                                CausesValidation="false" CssClass="btn btn-warning" ValidationGroup="academic"
                                                OnClick="btnCancel_Click" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="academic"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ddinfo" />

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script>
        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }
    </script>

    <script type="text/javascript" lang="javascript">

       <%-- $(document).ready(function () {
            debugger
            $("#<%= divpanel.ClientID %>").css("display", "none");
            $("#<%= pnltextbox.ClientID %>").css("display", "none");
            $("#<%= pnlDropdown.ClientID %>").css("display", "none");
        });--%>
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
              var rbText = e.options[e.selectedIndex].text;
              var ddlname = e.options[e.selectedIndex].text;
              if (rbText == "Please Select") {
                  alert('Please select Criteria as you want search...')
              }

              else {


                  if (rbText == "ddl") {
                      var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtStudent.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data you want to search..');
                        document.getElementById('<%=txtStudent.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtStudent.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtStudent.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }







    </script>

    <script>
        function Validate() {
            debugger
            var rbText;
            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;

            if (rbText == "IDNO" || rbText == "Mobile") {
                var char = (event.which) ? event.which : event.keyCode;
                if (char >= 48 && char <= 57)
                    return true;
                else
                    return false;
            }
            else if (rbText == "NAME") {
                var char = (event.which) ? event.which : event.keyCode;
                if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122))
                    return true;
                else
                    return false;
            }
        }

        $(document).ready(function () {
            debugger
            $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
    </script>
</asp:Content>

