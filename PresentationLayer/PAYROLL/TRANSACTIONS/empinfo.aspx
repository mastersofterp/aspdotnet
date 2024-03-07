<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="empinfo.aspx.cs" Inherits="payroll_empinfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--For Image Preview--%>
    <script type="text/javascript">
        //var jq = $.noConflict();

        function ShowpImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    jq('#ctl00_ContentPlaceHolder1_imgEmpPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }


        function ShowpSignPreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    jq('#ctl00_ContentPlaceHolder1_imgEmpSign').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>

    <style>
        .accordion-button {
            background: #eee;
            padding-top: 5px;
            margin-bottom: 10px;
            cursor: pointer;
        }

        .sub-heading {
            padding-bottom: 0px;
        }

            .sub-heading h5 {
                margin-bottom: 5px;
            }

        .more-less {
            float: right;
            color: #053769;
            display: inline-block;
            margin-top: 3px;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EMPLOYEE INFORMATION</h3>
                </div>
                <div class="box-body">
                    <div class="colapse-panel" id="accordion">
                        <asp:Panel ID="pnl" runat="server">
                            <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg"></asp:Label>
                            <asp:Panel ID="pnlEmployeeSearch" runat="server">
                                <div class="col-12 colapse-heading">
                                    <div class="row">
                                        <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divEmployeeSearchModify" aria-expanded="true" aria-controls="collapseOne">
                                            <i class="more-less fas fa-minus"></i>
                                            <div class="sub-heading">
                                                <h5>Search Employee for Modification
                                                </h5>
                                                <%--<asp:Image ID="ImageSearch" runat="server" ImageUrl="~/images/collapse_blue.jpg" alt=""
                                                    onclick="javascript:toggleExpansion(this,'divEmployeeSearchModify')" /></span></h5>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divEmployeeSearchModify" class="collapse show" data-parent="#accordion">
                                    <div id="divdemo2" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Search Criteria</label>
                                                    </div>
                                                    <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" Checked="true" />
                                                    <asp:RadioButton ID="rbDept" runat="server" Text="Department" GroupName="edit" />
                                                    <asp:RadioButton ID="rbDesig" runat="server" Text="Designation" GroupName="edit" />
                                                    <asp:RadioButton ID="rbEmpId" runat="server" Text="Reference No." GroupName="edit" />
                                                    <asp:RadioButton ID="rbRFId" runat="server" Text="BioID" GroupName="edit" />
                                                    <asp:RadioButton ID="rbEmpNo" runat="server" Text="Employee No." GroupName="edit" />
                                                    <asp:RadioButton ID="rbEmpCode" runat="server" Text="Employee Code" GroupName="edit" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Search String</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search For..." TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btnSearch" OnClientClick="return submitPopup(this.name);" TabIndex="2" />
                                                    <asp:Button ID="btnCanceModal" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClientClick="return ClearSearchBox()" TabIndex="3" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 mb-5">
                                            <asp:ListView ID="lvEmp" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Login Details</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Employee No.
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Department
                                                                </th>
                                                                <th>Designation
                                                                </th>
                                                                <th>Reference No.
                                                                </th>
                                                                <th>Staff ID
                                                                </th>
                                                                <th>BioID
                                                                </th>
                                                                <th>Pay Status
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
                                                            <%# Eval("EmployeeId")%>
                                                        </td>

                                                        <td>
                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name")%>' CssClass="Emp-Name-Class" CommandArgument='<%# Eval("IDNo")%>'
                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDEPT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDESIG")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("IDNo")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PFILENO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("RFIDNO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PSTATUS")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:UpdatePanel ID="upKannada" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div class="col-12 colapse-heading">
                                        <div class="row">
                                            <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divGeneralInfo" aria-expanded="true" aria-controls="collapseTwo">
                                                <i class="more-less fas fa-plus"></i>
                                                <div class="sub-heading">
                                                    <h5>Personal Details</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divGeneralInfo" class="collapse collapse show" data-parent="#accordion">
                                        <asp:Label ID="Label1" runat="server" SkinID="Msglbl"></asp:Label>
                                        <div class="col-12">
                                            <%--  <asp:UpdatePanel ID="upKannada" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>--%>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Reference No./IDNO.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtIdNo" runat="server" CssClass="form-control" Enabled="False" />
                                                    <%--<div class="input-group-addon">
                                                            <a href="#" title="Search Employee for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;">
                                                                <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" />
                                                            </a>
                                                        </div>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Staff ID./Employee Code</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPersonalFileNo" runat="server" MaxLength="15" TabIndex="4" CssClass="form-control" Text="0"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPersonalFileNo" runat="server" ControlToValidate="txtPersonalFileNo"
                                                        EnableClientScript="true" Display="None" ErrorMessage="Please Enter Staff ID/Employee Code."
                                                        SetFocusOnError="true" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>BioID./RFID.</label>
                                                       <asp:Button ID="btngetmaxid" runat="server" Text="Get Max RFID" CssClass="btn btn-primary ml-3" OnClick="btngetmaxid_Click" style="padding:3px 8px !important; font-size : 10px !important" Visible="false" />
                                                    </div>
                                                    <asp:TextBox ID="txtRFIDno" runat="server" CssClass="form-control" MaxLength="9" TabIndex="5"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        TargetControlID="txtRFIDno"
                                                        FilterType="Numbers"
                                                        FilterMode="ValidChars"
                                                        ValidChars="1234567890">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvRFID" runat="server" ControlToValidate="txtRFIDno" ValidationGroup="emp"
                                                        ErrorMessage="Please Enter BioID./RFID." SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Title</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control" TabIndex="6" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <%--  <asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTitle" ValidationGroup="emp"
                                                                                ErrorMessage="Please select title" SetFocusOnError="true" InitialValue="Please Select" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>First Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" TabIndex="7" AutoPostBack="true" OnTextChanged="txtFirstName_TextChanged" />
                                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                                        Display="None" ErrorMessage="Please Enter FirstName" SetFocusOnError="true"
                                                        ValidationGroup="emp"></asp:RequiredFieldValidator>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeFirstName" runat="server"
                                                        TargetControlID="txtFirstName"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters"
                                                        FilterMode="ValidChars"
                                                        ValidChars="-_ `">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Middle Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" TabIndex="8" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvMName" runat="server" ControlToValidate="txtMiddleName"
                                                                            Display="None" ErrorMessage="Please Enter MiddleName" SetFocusOnError="true"
                                                                            ValidationGroup="emp"></asp:RequiredFieldValidator>--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        TargetControlID="txtMiddleName"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters"
                                                        FilterMode="ValidChars"
                                                        ValidChars="-_ `">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Last Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" TabIndex="9" AutoPostBack="true" OnTextChanged="txtLastName_TextChanged" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvLName" runat="server" ControlToValidate="txtLastName"
                                                                            Display="None" ErrorMessage="Please Enter LastName" SetFocusOnError="true" ValidationGroup="emp"></asp:RequiredFieldValidator>--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        TargetControlID="txtLastName"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters"
                                                        FilterMode="ValidChars"
                                                        ValidChars="-_ `">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" TabIndex="10" onkeydown="return (event.keyCode!=13);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        TargetControlID="txtFatherName"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters"
                                                        FilterMode="ValidChars"
                                                        ValidChars="-_ `">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <%--<div class="form-group col-md-10">
                                                                            <label>ಫಸ್ಟ್ ನೇಮ್ :</label>
                                                                            <asp:TextBox ID="txtFirstNameKannada" ToolTip="ಎಂಟರ್ ಫಸ್ಟ್ ನೇಮ್ " runat="server" CssClass="form-control"
                                                                                TabIndex="9" onblur="PayHeadKannada()" />
                                                                        </div>
                                                                        <div class="form-group col-md-10">
                                                                            <label>ಮಿಡ್ಲ್ ನೇಮ್  :</label>
                                                                            <asp:TextBox ID="txtMiddleNameKannada" runat="server" TabIndex="10" CssClass="textbox" ToolTip="ಮಿಡ್ಲ್ ನೇಮ್ "
                                                                                onblur="PayHeadKannada()" />
                                                                        </div>
                                                                        <div class="form-group col-md-10">
                                                                            <label>ಲಾಸ್ಟ್ ನೇಮ್ :</label>
                                                                            <asp:TextBox ID="txtLastNameKannada" runat="server" TabIndex="11" CssClass="textbox" ToolTip="ಲಾಸ್ಟ್ ನೇಮ್ "
                                                                                onblur="PayHeadKannada()" />
                                                                        </div>
                                                                        <div class="form-group col-md-10">
                                                                            <label>ಫಾದರ್ ನೇಮ್  :</label>
                                                                            <asp:TextBox ID="txtFatherNameKannada" runat="server" TabIndex="12" CssClass="textbox" ToolTip="ಫಾದರ್ ನೇಮ್ "
                                                                                onblur="PayHeadKannada()" />
                                                                        </div>--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Maiden Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMaidenName" runat="server" CssClass="form-control" TabIndex="11" onkeydown="return (event.keyCode!=13);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                        TargetControlID="txtMaidenName"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters"
                                                        FilterMode="ValidChars"
                                                        ValidChars="-_ `">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Mother Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtmothername" runat="server" CssClass="form-control" TabIndex="12" onkeydown="return (event.keyCode!=13);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                        TargetControlID="txtmothername"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters"
                                                        FilterMode="ValidChars"
                                                        ValidChars="-_ `">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>UAN No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtUANNO" runat="server" CssClass="form-control" TabIndex="13" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Blood Group</label>
                                                    </div>
                                                    <asp:DropDownList ID="DdlBloodGroup" runat="server" CssClass="form-control" TabIndex="14" AppendDataBoundItems="true" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTitle" ValidationGroup="emp"
                                                                                ErrorMessage="Please select title" SetFocusOnError="true" InitialValue="Please Select" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Date of Birth</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalDateOfBirth" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" Enabled="true" ValidationGroup="emp"
                                                            TabIndex="16" AutoPostBack="true" OnTextChanged="txtBirthDate_TextChanged" />
                                                        <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtBirthDate" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                                            EnableViewState="true" OnClientDateSelectionChanged="CheckDateEalier">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtBirthDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeBirthDate"
                                                            ControlToValidate="txtBirthDate" EmptyValueMessage="Please Enter Birth Date"
                                                            InvalidValueMessage="BirthDate is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Birth Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="emp" SetFocusOnError="True" />
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Age</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" TabIndex="15" Text="0" Enabled="false" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Gender</label>
                                                    </div>
                                                    <asp:RadioButton ID="rdbMale" runat="server" Text="Male" GroupName="gpSex" TabIndex="17"
                                                        Checked="True" />&nbsp;
                                                <asp:RadioButton ID="rdbFemale" runat="server" Text="Female" GroupName="gpSex" TabIndex="18" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <%--  <label>Seniority / Sequence No. :<span style="color: Red">*</span></label>--%>
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Department Seniority/Sequence No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSeqNo" runat="server" CssClass="form-control" TabIndex="19" MaxLength="10" onKeyUp="return validateNumeric(this)" Text="0"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSeqNo"
                                                        Display="None" ErrorMessage="Please Enter Department Seniority No." SetFocusOnError="true" ValidationGroup="emp"></asp:RequiredFieldValidator>

                                                    <asp:CompareValidator ID="cvSeqNo" runat="server" ControlToValidate="txtSeqNo" Display="None"
                                                        ErrorMessage="Please Enter Numeric Value" Operator="DataTypeCheck" SetFocusOnError="true"
                                                        Type="Integer" ValidationGroup="emp"></asp:CompareValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Aadhar Card Number</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNationalUniqueIDNo" runat="server" CssClass="form-control" TabIndex="20" Text="0" MaxLength="12"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvNationalUniqueIDNo" runat="server" Enabled="false"
                                                        ControlToValidate="txtNationalUniqueIDNo" EnableClientScript="true" Display="None"
                                                        ErrorMessage="Please Enter Aadhar Card Number" SetFocusOnError="true" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rgeNationalUniqueIDNo" runat="server" ControlToValidate="txtNationalUniqueIDNo" ValidationExpression="^[2-9]{1}[0-9]{11}$"
                                                        ErrorMessage="Invalid Adhar Card No" ValidationGroup="emp" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                        TargetControlID="txtNationalUniqueIDNo"
                                                        FilterType="Numbers"
                                                        FilterMode="ValidChars"
                                                        ValidChars="1234567890">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                                <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                
                                            </div>--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Photo Upload</label>
                                                    </div>
                                                    <asp:Image ID="imgEmpPhoto" runat="server" ImageUrl="~/IMAGES/sign11.jpg" Height="128px"
                                                        Width="128px" /><br />
                                                    <asp:FileUpload ID="fuplEmpPhoto" runat="server" ToolTip="Please Browse Photo"
                                                        TabIndex="22" onchange="ShowpImagePreview(this);" />
                                                </div>
                                                <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                
                                            </div>--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Signature Upload</label>
                                                    </div>
                                                    <asp:Image ID="imgEmpSign" runat="server" ImageUrl="~/IMAGES/sign11.jpg" Height="58px"
                                                        Width="128px" /><br />
                                                    <asp:FileUpload ID="fuplEmpSign" runat="server" ToolTip="Please Browse Signature"
                                                        TabIndex="23" onchange="ShowpSignPreview(this);" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <%-- <label>Husband&#39;s Name :</label>--%>
                                                    <div class="label-dynamic">
                                                        <label>Spouse Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtHusbandName" runat="server" CssClass="form-control" TabIndex="24" onkeydown="return (event.keyCode!=13);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        TargetControlID="txtHusbandName"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters"
                                                        FilterMode="ValidChars"
                                                        ValidChars="-_ `">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Personal Email ID</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" TabIndex="25" onkeydown="return (event.keyCode!=13);" />
                                                    <asp:RegularExpressionValidator ID="rxvEmailId" runat="server" ControlToValidate="txtEmailId"
                                                        Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="emp"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Official Email ID</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAlternateEmailId" runat="server" CssClass="form-control" TabIndex="26" onkeydown="return (event.keyCode!=13);" />
                                                    <asp:RegularExpressionValidator ID="rxvAlternateEmailId" runat="server" ControlToValidate="txtAlternateEmailId"
                                                        Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="emp"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Mobile No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" MaxLength="11" onKeyUp="validateNumeric(this)" TabIndex="27" onkeydown="return (event.keyCode!=13);" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                        ControlToValidate="txtPhoneNumber" EnableClientScript="true" Display="None"
                                                        ErrorMessage="Please Enter Mobile No." SetFocusOnError="true" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Phone No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAlterPhoneNumber" runat="server" CssClass="form-control" MaxLength="11" onKeyUp="validateNumeric(this)" TabIndex="28" onkeydown="return (event.keyCode!=13);" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Permanent Address</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="29" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Local Address</label>
                                                    </div>
                                                    <asp:TextBox ID="txtLocalAddress" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="30" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>User ID</label>
                                                    </div>
                                                    <asp:TextBox ID="txtuserid" runat="server" CssClass="form-control" TabIndex="31" Text="" Enabled="false" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div id="div1" runat="server" style="display: none">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divStatusDetails" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Status Details
                                                        <%--<span>
                                                        <img id="img6" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divStatusDetails')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divStatusDetails" class="collapse" data-parent="#accordion">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Status Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="32">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgCalDateStatus" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtStatusDT" CssClass="form-control" runat="server" Enabled="true" TabIndex="33" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtStatusDT" PopupButtonID="imgCalDateStatus" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStatusDT"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                ControlToValidate="txtStatusDT" EmptyValueMessage="Please Enter Increment Date"
                                                                InvalidValueMessage="Status Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Increment Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="emp" SetFocusOnError="True" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divServiceDetails" runat="server">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divServiceTypeDetails" aria-expanded="false" aria-controls="collapseFour">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Service Type Details 
                                                        <span>
                                                            <%--<img id="img4" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divServiceTypeDetails')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divServiceTypeDetails" class="collapse" data-parent="#accordion">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>College</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                    TabIndex="34" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege" ValidationGroup="emp"
                                                                    ErrorMessage="Please select College" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Designation Nature</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDesigNature" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="35">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Scheme/Staff</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="updStaff" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                    TabIndex="36" AutoPostBack="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaff" ValidationGroup="emp"
                                                                    ErrorMessage="Please select Scheme" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Designation</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="updDesignation" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                                    TabIndex="37">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Leave Category/Staff Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlVacational" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="38">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Main Department</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlmaindeptname" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="39">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <label>PG Course Department</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPGDept" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="40">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Classification</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlClassification" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="41">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Department</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanelddldep" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                    TabIndex="42" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <%--add amol --%>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Division </label>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanelddldiv" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                    TabIndex="43" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Appointment</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanelddlap" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlAppointment" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                    TabIndex="44" AutoPostBack="true" OnSelectedIndexChanged="ddlAppointmnet_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvappoint" runat="server" ControlToValidate="ddlAppointment" ValidationGroup="emp"
                                                                    ErrorMessage="Please select Appointment" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div3" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Nagpur University Designation</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="updNUDesig" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlNuDesig" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                                    TabIndex="45">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div4">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>User Type</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlUserType" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                                    TabIndex="46">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ControlToValidate="ddlUserType" ValidationGroup="emp"
                                                                    ErrorMessage="Please select User Type" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Employee Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="47">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlEmpType" ValidationGroup="emp"
                                                            ErrorMessage="Please select Employee Type" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>


                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Bus Facility</label>
                                                        </div>
                                                        <asp:CheckBox runat="server" ID="isbusfac" Checked="false" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Cab Facility</label>
                                                        </div>
                                                        <asp:CheckBox runat="server" ID="isCabFac" Checked="false" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Is Shift Management</label>
                                                        </div>
                                                        <asp:CheckBox runat="server" ID="isShiftManagement" Checked="false" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <%-- <label>Shift No. :</label>--%>
                                                        <div class="label-dynamic">
                                                            <label>Shift Timing</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="Updateshift" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlshiftno" runat="server" AppendDataBoundItems="true" TabIndex="21" data-select2-enable="true" AutoPostBack="true"
                                                                    CssClass="form-control">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Is BioAuthority Person</label>
                                                        </div>
                                                        <asp:CheckBox runat="server" ID="chkIsBioAuthorityPerson" Checked="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divServiceDateDetails" runat="server">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divServiceDateDetails1" aria-expanded="false" aria-controls="collapseFive">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Service Date Details 
                                                        <%--<span>--%>
                                                            <%--<img id="img3" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divServiceDateDetails1')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divServiceDateDetails1" class="collapse" data-parent="#accordion">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date Of Joining</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgCalJoinDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtJoinDate" CssClass="form-control" runat="server" Enabled="true" TabIndex="48" AutoPostBack="true"
                                                                OnTextChanged="txtJoinDate_TextChanged"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="ceJoinDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtJoinDate" PopupButtonID="imgCalJoinDate" Enabled="true" EnableViewState="true" OnClientDateSelectionChanged="CheckDateEalier">
                                                                <%--  OnClientDateSelectionChanged="CompareDOJ"--%>
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeJoinDate" runat="server" TargetControlID="txtJoinDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevJoinDate" runat="server" ControlExtender="meeJoinDate"
                                                                ControlToValidate="txtJoinDate" EmptyValueMessage="Please Enter Joining Date"
                                                                InvalidValueMessage="Joining Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Joining Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="emp" SetFocusOnError="True" />
                                                        </div>
                                                        <asp:RadioButton ID="rdbFN" runat="server" GroupName="gpANFN" Text="FN" TabIndex="49"
                                                            Checked="True" />
                                                        <%-- <asp:CompareValidator ID="cmpVal1" ControlToCompare="txtBirthDate"
                                                            ControlToValidate="txtJoinDate" Type="Date" Operator="GreaterThanEqual"
                                                            ErrorMessage="Birth Date Should be Less than To Joining Date." ValidationGroup="emp" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>--%>
                                                        <asp:RadioButton ID="rdbAN" runat="server" GroupName="gpANFN" Text="AN" TabIndex="50" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date Of Increment</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgCalDateIncrement" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtIncrDate" CssClass="form-control" runat="server" Enabled="true" TabIndex="51" />
                                                            <ajaxToolKit:CalendarExtender ID="ceIncrDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtIncrDate" PopupButtonID="imgCalDateIncrement" Enabled="true"
                                                                EnableViewState="true" OnClientDateSelectionChanged="CompareDOI">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeIncrDate" runat="server" TargetControlID="txtIncrDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevIncrDate" runat="server" ControlExtender="meeIncrDate"
                                                                ControlToValidate="txtIncrDate" EmptyValueMessage="Please Enter Increment Date"
                                                                InvalidValueMessage="Increment Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Increment Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="emp" SetFocusOnError="True" />
                                                            <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtBirthDate"
                                                                ControlToValidate="txtIncrDate" Type="Date" Operator="GreaterThanEqual"
                                                                ErrorMessage="Birth Date Should be Less than Increment Date." ValidationGroup="emp" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>
                                                            <asp:CompareValidator ID="CompareValidator2" ControlToCompare="txtJoinDate"
                                                                ControlToValidate="txtIncrDate" Type="Date" Operator="GreaterThanEqual"
                                                                ErrorMessage="Increment Date Should be Greater than Joining Date." ValidationGroup="emp" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date Of Retirement</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="updRetire" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i id="imgCalRetireDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtRetireDate" CssClass="form-control" runat="server" TabIndex="50" />
                                                                    <ajaxToolKit:CalendarExtender ID="ceRetireDate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtRetireDate" PopupButtonID="imgCalRetireDate" Enabled="true"
                                                                        EnableViewState="true">
                                                                    </ajaxToolKit:CalendarExtender>
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeRetireDate" runat="server" TargetControlID="txtRetireDate"
                                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                        AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mevRetireDate" runat="server" ControlExtender="meeRetireDate"
                                                                        ControlToValidate="txtRetireDate" EmptyValueMessage="Please Enter Joining Date"
                                                                        InvalidValueMessage="Retirement Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                        TooltipMessage="Please Enter Retirement Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                        ValidationGroup="emp" SetFocusOnError="True" />
                                                                    <asp:CompareValidator ID="CompareValidator3" ControlToCompare="txtBirthDate"
                                                                        ControlToValidate="txtRetireDate" Type="Date" Operator="GreaterThanEqual"
                                                                        ErrorMessage="Retirement Date Should be Greater than Birth Date." ValidationGroup="emp" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>
                                                                    <asp:CompareValidator ID="CompareValidator4" ControlToCompare="txtIncrDate"
                                                                        ControlToValidate="txtRetireDate" Type="Date" Operator="GreaterThanEqual"
                                                                        ErrorMessage="Retirement Date Should be Greater than Increament Date." ValidationGroup="emp" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlStaff" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Date of Relieving</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgRelievingDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtRelievingDate" CssClass="form-control" runat="server" Enabled="True" TabIndex="51" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtRelievingDate" PopupButtonID="imgRelievingDate" Enabled="true"
                                                                EnableViewState="true" OnClientDateSelectionChanged="CompareDOJ">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtRelievingDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeIncrDate"
                                                                ControlToValidate="txtRelievingDate" EmptyValueMessage="Please Enter Relieving Date"
                                                                InvalidValueMessage="Relieving Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Relieving Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="emp" SetFocusOnError="True" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Expire date of Extension</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgExpiryDtExt" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtExpiryDtExt" CssClass="form-control" runat="server" Enabled="true" TabIndex="52" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtExpiryDtExt" PopupButtonID="imgExpiryDtExt" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtExpiryDtExt"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="meeIncrDate"
                                                                ControlToValidate="txtExpiryDtExt" EmptyValueMessage="Please Enter Expiry Date of Ext."
                                                                InvalidValueMessage="Expiry Date of Ext. is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Expiry Date of Ext." EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="emp" SetFocusOnError="True" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="div5" runat="server">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#div6" aria-expanded="false" aria-controls="collapseSix">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Employee Other Information
                                                        <%--<span>--%>
                                                            <%--<img id="img7" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divBasicDetails1')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="div6" class="collapse" data-parent="#accordion">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Marital Status</label>
                                                        </div>
                                                        <%--  <asp:CheckBox ID="chkIsMarried" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkIsMarried_CheckedChanged"  />--%>
                                                        <asp:RadioButton ID="rdoMarried" runat="server" GroupName="gpMar" Text="Married" TabIndex="53" AutoPostBack="true" OnCheckedChanged="chkIsMarried_CheckedChanged" />
                                                        <asp:RadioButton ID="rdounMarried" runat="server" GroupName="gpMar" Text="UnMarried" TabIndex="54" Checked="false" AutoPostBack="true" OnCheckedChanged="chkIsMarried_CheckedChanged" />
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Number of Children</label>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12 form-inline">
                                                                <div class="label-dynamic mr-2">
                                                                    <label>Male</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMaleChild" runat="server" CssClass="form-control" TabIndex="55" Text="0" Enabled="true" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-6 col-md-6 col-12 form-inline">
                                                                <div class="label-dynamic mr-2">
                                                                    <label>Female</label>
                                                                </div>
                                                                <asp:TextBox ID="txtFemaleChild" runat="server" CssClass="form-control" TabIndex="56" Text="0" Enabled="true" onKeyUp="validateNumeric(this)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>College Room No</label>
                                                        </div>
                                                        <asp:TextBox ID="txtcolRoom" runat="server" CssClass="form-control" TabIndex="57"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>College Intercom No</label>
                                                        </div>
                                                        <asp:TextBox ID="txtColIntcomNo" runat="server" CssClass="form-control" TabIndex="58"></asp:TextBox>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Qualification for Display</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDisplayQualification" runat="server" CssClass="form-control" TabIndex="59"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <label>Are you Physically Challenged</label>
                                                        </div>
                                                        <asp:CheckBox ID="chkHandicap" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkHandicap_CheckedChanged" />
                                                        <%-- onchange="ChangePanels1()"   --%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divHandicapList" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label id="lblHandicap" runat="server">Type of Handicap</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlHandicap" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="60">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Learning Disability</asp:ListItem>
                                                            <asp:ListItem Value="2">Hearing Impairment</asp:ListItem>
                                                            <asp:ListItem Value="3">Mental Disability</asp:ListItem>
                                                            <asp:ListItem Value="4">Physical Impairment</asp:ListItem>
                                                            <asp:ListItem Value="5">Visual Impairment</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <label>Employment</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmployement" runat="server" CssClass="form-control" TabIndex="61"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <label>Telugu Minority</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdotelMin" runat="server" GroupName="gptlgmin" Text="Yes" TabIndex="62" />
                                                        <asp:RadioButton ID="rdotelMinNo" runat="server" GroupName="gptlgmin" Text="No" TabIndex="63"
                                                            Checked="True" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Drug Allergy</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdodrug" runat="server" GroupName="gpdrug" Text="Yes" TabIndex="64" />
                                                        <asp:RadioButton ID="rdodrugno" runat="server" GroupName="gpdrug" Text="No" TabIndex="65"
                                                            Checked="True" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divBasicDetails" runat="server">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divBasicDetails1" aria-expanded="false" aria-controls="collapseSeven">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Basic Details
                                                        <%--<span>
                                                        <img id="img1" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divBasicDetails1')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divBasicDetails1" class="collapse" data-parent="#accordion">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>T.A</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdbTAyes" runat="server" GroupName="gpTA" Text="Yes" TabIndex="66"
                                                            Checked="True" />
                                                        <asp:RadioButton ID="rdbTAno" runat="server" GroupName="gpTA" Text="No" TabIndex="67" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Quarter</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdbQtrYes" runat="server" GroupName="gpQtr" Text="Yes" TabIndex="68" />
                                                        <asp:RadioButton ID="rdbQtrNo" runat="server" GroupName="gpQtr" Text="No" TabIndex="69"
                                                            Checked="True" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Quarter Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlQuarter" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="70">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Quarters Allotment Date</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgQuaterAltDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtQuaterAltDate" CssClass="form-control" runat="server" TabIndex="71" />
                                                            <ajaxToolKit:CalendarExtender ID="ceQuaterAltDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtQuaterAltDate" PopupButtonID="imgQuaterAltDate" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meQuaterAltDate" runat="server" TargetControlID="txtQuaterAltDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevQuaterAltDate" runat="server" ControlExtender="meQuaterAltDate"
                                                                ControlToValidate="txtQuaterAltDate" EmptyValueMessage="Please Select/Enter Quarters Allotment Date."
                                                                InvalidValueMessage="Expiry Date of Ext. is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Select/Enter Quarters Allotment Date." EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="emp" SetFocusOnError="True" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Handicap</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdbHpYes" runat="server" GroupName="gpCitizen" Text="Yes" TabIndex="72" />
                                                        <asp:RadioButton ID="rdbHpNo" runat="server" GroupName="gpCitizen" Text="No" TabIndex="73"
                                                            Checked="True" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Senior Citizen</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdbSeniorCitizenYes" runat="server" GroupName="gpSeniorCitizen" Text="Yes" TabIndex="74" />
                                                        <asp:RadioButton ID="rdbSeniorCitizenNo" runat="server" GroupName="gpSeniorCitizen" Text="No" TabIndex="75"
                                                            Checked="True" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Quarter Rent</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdbRentYes" runat="server" GroupName="gpRent" Text="Yes" TabIndex="76" />
                                                        <asp:RadioButton ID="rdbRentNo" runat="server" GroupName="gpRent" Text="No" TabIndex="77"
                                                            Checked="True" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12"  id="divDAHeadID" runat="server" visible="false">
                                                         <div class="label-dynamic">
                                                             <sup>* </sup>
                                                            <label>DA Head Calculation</label>
                                                        </div>
                                                           <asp:DropDownList ID="ddlDAHeadCalculation" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="70">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divHRAHEADID" runat="server"  visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>HRA Head Calculation</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlHRAHeadCalculation" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="70">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divBankDetails" runat="server">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divBankDetails1" aria-expanded="false" aria-controls="collapseEight">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Bank Details
                                                        <%--<span>
                                                        <img id="img2" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divBankDetails1')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divBankDetails1" class="collapse" data-parent="#accordion">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>NEFT Paymode</label>
                                                        </div>
                                                        <asp:CheckBox runat="server" ID="chkNEFT" Checked="false" TabIndex="78" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Bank Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="79">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Bank A/c No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control" TabIndex="80" MaxLength="16" onkeydown="return (event.keyCode!=13);" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbAccNo" runat="server"
                                                            TargetControlID="txtAccNo"
                                                            FilterType="Custom,Numbers"
                                                            FilterMode="ValidChars"
                                                            ValidChars="">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>P.F</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPF" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="81">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>P.F No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPFNo" runat="server" CssClass="form-control" TabIndex="82" AutoPostBack="true" OnTextChanged="txtPFNo_TextChanged"
                                                            onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Employee No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmployeeId" runat="server" CssClass="form-control" TabIndex="83"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>ESIC NO</label>
                                                        </div>
                                                        <asp:TextBox ID="txtESICNo" runat="server" CssClass="form-control" TabIndex="84" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Bank Place</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBankPlace" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                            TabIndex="85">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvBankPlace" runat="server" ControlToValidate="ddlBankPlace"
                                                            Display="None" ErrorMessage="Please Select Bank Place" SetFocusOnError="true"
                                                            ValidationGroup="emp"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>IFSC Code</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" TabIndex="86" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>PAN No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control" TabIndex="87" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>P.P.F No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPPFNo" runat="server" CssClass="form-control" TabIndex="88" onKeyUp="validateNumeric(this)" onkeydown="return (event.keyCode!=13);" />
                                                        <asp:CompareValidator ID="cvPPFNo" runat="server" ControlToValidate="txtPPFNo" Display="None"
                                                            ErrorMessage="Enter Only Numeric Values" Operator="DataTypeCheck" Type="Integer"
                                                            ValidationGroup="emp" SetFocusOnError="true"></asp:CompareValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Extra EPF</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdbExtraEPFYes" runat="server" GroupName="gpExtEPF" Text="Yes" TabIndex="89" />
                                                        <asp:RadioButton ID="rdbExtraEPFNo" runat="server" GroupName="gpExtEPF" Text="No" TabIndex="90"
                                                            Checked="True" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divPayDetails" runat="server">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divPayDetails1" aria-expanded="false" aria-controls="collapseNine">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Pay-Scale Details
                                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divPayDetails1" class="collapse" data-parent="#accordion">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">                                                    
                                                            <label>Rule</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="updpayrule" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlPayRule" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                    TabIndex="91" OnSelectedIndexChanged="ddlPayRule_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPayRule" ValidationGroup="emp"
                                                                    ErrorMessage="Please select Rule" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <asp:UpdatePanel ID="updPayScale" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group col-md-12" runat="server" id="DivScale">
                                                                    <div class="label-dynamic">
                                                                        <label>Scale</label>
                                                                    </div>

                                                                    <asp:DropDownList ID="ddlPayScale" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                        TabIndex="94" AutoPostBack="true" OnSelectedIndexChanged="ddlPayScale_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group col-md-12" runat="server" id="DivPayLevel">

                                                                    <div class="label-dynamic">

                                                                        <label>Pay level </label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlPaylevel" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                        TabIndex="37" OnSelectedIndexChanged="ddlPaylevel_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </ContentTemplate>

                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                            <ContentTemplate>
                                                                <div class="form-group col-md-12" runat="server" id="DivCellNum2">

                                                                    <div class="label-dynamic">

                                                                        <label>Cell Number </label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlCellNo" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCellNo_SelectedIndexChanged" TabIndex="37">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlPaylevel" EventName="SelectedIndexChanged" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>

                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Present Basic</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtBasic" runat="server" CssClass="form-control" MaxLength="6" TabIndex="92"
                                                                    onKeyUp="validateNumeric(this)">0</asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                    ControlToValidate="txtBasic" EnableClientScript="true" Display="None"
                                                                    ErrorMessage="Please Enter Basic Amount" SetFocusOnError="true" ValidationGroup="emp"></asp:RequiredFieldValidator>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlCellNo" EventName="SelectedIndexChanged" />

                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>


                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Pay Status</label>
                                                        </div>
                                                        <asp:RadioButton ID="rdbPayYes" runat="server" GroupName="gpPay" Text="Yes" TabIndex="95"
                                                            Checked="true" />
                                                        <asp:RadioButton ID="rdbPayNo" runat="server" GroupName="gpPay" Text="No" TabIndex="96" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Remark</label>
                                                        </div>
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="93" Rows="1" />
                                                    </div>


                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Grade Pay</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="updgradepay" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtGradePay" runat="server" CssClass="form-control" TabIndex="97" Enabled="false"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="DivConPay" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Consolidate Pay</label>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtConsPay" runat="server" CssClass="form-control" TabIndex="98" MaxLength="6" onKeyUp="validateNumeric(this)" Text="0"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divExitProcess" runat="server" visible="true">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divexitstatus" aria-expanded="false" aria-controls="collapseNine">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Exit Details
                                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divexitstatus" class="collapse" data-parent="#accordion">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Is On Notice</label>
                                                    </div>
                                                    <asp:CheckBox runat="server" ID="chkIsOnNotice" Checked="false" TabIndex="78" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Date of Exit</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalExitDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtdateofExit" CssClass="form-control" runat="server" Enabled="true" TabIndex="48"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtdateofExit" PopupButtonID="imgCalExitDate" Enabled="true" EnableViewState="true">
                                                            <%--  OnClientDateSelectionChanged="CompareDOJ"--%>
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeExitDate" runat="server" TargetControlID="txtdateofExit"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="meeExitDate"
                                                            ControlToValidate="txtdateofExit" EmptyValueMessage="Please Enter Exit Date"
                                                            InvalidValueMessage="Exit Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Exit Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="emp1" SetFocusOnError="True" InitialValue="0" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Date of Resignation</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="i2" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtdateofResigation" CssClass="form-control" runat="server" Enabled="true" TabIndex="48"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtdateofResigation" PopupButtonID="i2" Enabled="true" EnableViewState="true">
                                                            <%--  OnClientDateSelectionChanged="CompareDOJ"--%>
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtdateofResigation"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender5"
                                                            ControlToValidate="txtdateofResigation" EmptyValueMessage="Please Enter Resignation Date"
                                                            InvalidValueMessage="Resignation Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Resignation Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="emp1" SetFocusOnError="True" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Attrition Type</label>
                                                    </div>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlAttritionType" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                TabIndex="94">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlAttritionType"
                                                                Display="None" ErrorMessage="Please Select Attrition Type" SetFocusOnError="true" InitialValue="0"
                                                                ValidationGroup="emp1"></asp:RequiredFieldValidator>--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Exit Reason</label>
                                                    </div>
                                                    <asp:TextBox ID="txtexitreason" runat="server" CssClass="form-control" TabIndex="59"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtexitreason"
                                                        Display="None" ErrorMessage="Please Enter Exit Reason" SetFocusOnError="true"
                                                        ValidationGroup="emp1"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divOtherDetails" runat="server" visible="true">
                                        <div class="col-12 colapse-heading">
                                            <div class="row">
                                                <div class="col-12 collapsed accordion-button" data-toggle="collapse" data-target="#divOtherDetails1" aria-expanded="false" aria-controls="collapseNine">
                                                    <i class="more-less fas fa-plus"></i>
                                                    <div class="sub-heading">
                                                        <h5>Other Details
                                                        <%--<span>
                                                        <img id="img5" src="../../images/collapse_blue.jpg" alt="" onclick="javascript:toggleExpansion(this,'divPayDetails1')" /></span>--%>

                                                        </h5>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divOtherDetails1" class="collapse" data-parent="#accordion">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Entity Type</label>
                                                    </div>
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlentitytype" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                                TabIndex="94">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlentitytype"
                                                                Display="None" ErrorMessage="Please Select Entity Type" SetFocusOnError="true" InitialValue="0"
                                                                ValidationGroup="emp1"></asp:RequiredFieldValidator>--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>State</label>
                                                    </div>
                                                    <asp:TextBox ID="txtstate" runat="server" CssClass="form-control" TabIndex="59"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtstate"
                                                        Display="None" ErrorMessage="Please Enter State" SetFocusOnError="true"
                                                        ValidationGroup="emp1"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Country</label>
                                                    </div>
                                                    <asp:TextBox ID="txtcountry" runat="server" CssClass="form-control" TabIndex="59"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcountry"
                                                        Display="None" ErrorMessage="Please Enter Country" SetFocusOnError="true"
                                                        ValidationGroup="emp1"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>City</label>
                                                    </div>
                                                    <asp:TextBox ID="txtcity" runat="server" CssClass="form-control" TabIndex="59"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtcity"
                                                        Display="None" ErrorMessage="Please Enter City" SetFocusOnError="true"
                                                        ValidationGroup="emp1"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Group of DOJ</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="i3" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtgroupofdoj" CssClass="form-control" runat="server" Enabled="true" TabIndex="48"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtgroupofdoj" PopupButtonID="i3" Enabled="true" EnableViewState="true">
                                                            <%--  OnClientDateSelectionChanged="CompareDOJ"--%>
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtgroupofdoj"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender6"
                                                            ControlToValidate="txtgroupofdoj" EmptyValueMessage="Please Enter Group Of DOJ"
                                                            InvalidValueMessage="Group of DOJ  is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Group Of DOJ" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="emp1" SetFocusOnError="True" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--  </div>--%>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="btnSubmit" />--%>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                    <asp:PostBackTrigger ControlID="btnPrint" />
                                    <%--<asp:AsyncPostBackTrigger ControlID="ddlDepartment" /> --%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="99" OnClick="btnSubmit_Click"
                                ValidationGroup="emp" CssClass="btn btn-primary" />
                            <asp:Button ID="btnPrint" runat="server" Text="Report" CssClass="btn btn-info" TabIndex="100"
                                ValidationGroup="emp" OnClick="btnPrint_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="101"
                                CausesValidation="false" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="vsEmp" runat="server" ValidationGroup="emp" ShowMessageBox="true"
                                ShowSummary="false" DisplayMode="List" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../images/collapse_blue.jpg";
            }
        }

        function submitPopup(btnsearch) {
            debugger;
            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "Name";
            else if (document.getElementById('<%=rbDept.ClientID %>').checked == true)
                rbText = "Department";
            else if (document.getElementById('<%=rbDesig.ClientID %>').checked == true)
                rbText = "Designation";
            else if (document.getElementById('<%=rbEmpId.ClientID %>').checked == true)
                rbText = "Idno";
            else if (document.getElementById('<%=rbRFId.ClientID %>').checked == true)
                rbText = "RFIdno";
            else if (document.getElementById('<%=rbEmpNo.ClientID %>').checked == true)
                rbText = "EmployeeNo";
            else if (document.getElementById('<%=rbEmpCode.ClientID %>').checked == true)
                rbText = "PFILENO";

    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox() {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';;
        }

        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgEmpPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuplEmpPhoto").value;
        }
    </script>

    <script type="text/javascript" language="javascript">

        //date check validations

        function checkDate(sender, args) {
            //alert(sender._selectedDate > new Date());

            //var dateEntered = document.getElementById("txtDOB").value;
            //alert('1');
            //alert(sender._selectedDate);
            var dateToCompare = sender._selectedDate;
            //alert('2');
            var currentDate = new Date();
            //alert(dateToCompare);
            //alert(currentDate);

            if (dateToCompare >= currentDate) {

                // alert(dateToCompare);
                // alert(currentDate);
                alert("You cannot select a day future than today!");

                document.getElementById("<%= txtBirthDate.ClientID %>").value = "";
                return false;
            }

        }

        function checkDate1(txt) {
            if (new Date(txt.value) > new Date()) {
                document.getElementById("<%= txtBirthDate.ClientID %>").value = "";
                return false;
            }
        }

        function CompareDOI(source, arguments) {
            var fdate = document.getElementById('<%=txtBirthDate.ClientID%>');
            var edate = document.getElementById('<%=txtIncrDate.ClientID%>');

            var jdate = document.getElementById('<%=txtJoinDate.ClientID%>');

            var BirthDate = fdate.value.split('/');
            var IcnDate = edate.value.split('/');
            var JoinDate = jdate.value.split('/');
            var val = 'false';
            var valjoin = 'false';

            if (parseInt(BirthDate[2]) < parseInt(IcnDate[2])) {
                val = 'true';
                // return true;
            }
            else if (parseInt(BirthDate[2]) == parseInt(IcnDate[2]) && parseInt(BirthDate[1]) < parseInt(IcnDate[1])) {
                val = 'true';
                // return true;
            }
            else if (parseInt(BirthDate[2]) == parseInt(IcnDate[2])) {
                if (parseInt(BirthDate[0]) < parseInt(IcnDate[0])) {
                    val = 'true';
                    // return true;
                }
                else if (parseInt(BirthDate[0]) == parseInt(IcnDate[0])) {
                    if (parseInt(BirthDate[1]) <= parseInt(IcnDate[1])) {
                        val = 'true';
                        // return true;
                    }
                }
            }

            //to check increment date and joining date
            if (parseInt(JoinDate[2]) < parseInt(IcnDate[2])) {
                valjoin = 'true';
                // return true;
            }
            else if (parseInt(JoinDate[2]) == parseInt(IcnDate[2]) && parseInt(JoinDate[1]) < parseInt(IcnDate[1])) {
                valjoin = 'true';
                //return true;
            }
            else if (parseInt(JoinDate[2]) == parseInt(IcnDate[2])) {
                if (parseInt(JoinDate[0]) < parseInt(IcnDate[0])) {
                    valjoin = 'true';
                    // return true;
                }
                else if (parseInt(JoinDate[0]) == parseInt(IcnDate[0])) {
                    if (parseInt(JoinDate[1]) <= parseInt(IcnDate[1])) {
                        valjoin = 'true';
                        //return true;
                    }
                }
            }

            //end of to check increment date and joining date

            if (val == "true" && valjoin == "false") {
                alert("Increment date should be greater than Joining Date");
                document.getElementById("<%= txtIncrDate.ClientID %>").value = "";
                // document.getElementById("ContentPlaceHolder1_txtDOI").value = "";
                return false;

            }

            else if (val == "true" && valjoin == "true") {
                return true;

            }

            else if (val == "false" && valjoin == "false") {
                alert("Increment date should be greater than Joining Date and Birth Date");
                document.getElementById("<%= txtIncrDate.ClientID %>").value = "";
                //document.getElementById("ContentPlaceHolder1_txtDOI").value = "";
                return false;

            }
            else if (val == "false" && valjoin == "true") {
                alert("Increment date should be greater than Birth Date");
                document.getElementById("<%= txtIncrDate.ClientID %>").value = "";
                // document.getElementById("ContentPlaceHolder1_txtDOI").value = "";
                return false;

            }
}

function CompareDOJ(source, arguments) {
    var fdate = document.getElementById('<%=txtBirthDate.ClientID%>');
    var edate = document.getElementById('<%=txtJoinDate.ClientID%>');
    var dojinc = document.getElementById('<%=txtIncrDate.ClientID%>');
    var FromDate = fdate.value.split('/');
    var EndDate = edate.value.split('/');

    var DojDoiComp = dojinc.value.split('/');
    var val = 'false';
    valjoin == "false";
    if (parseInt(FromDate[2]) < parseInt(EndDate[2])) {
        val = 'true';

        return true;

    }
    else if (parseInt(FromDate[2]) == parseInt(EndDate[2]) && parseInt(FromDate[1]) < parseInt(EndDate[1])) {
        val = 'true';

        return true;
    }
    else if (parseInt(FromDate[2]) == parseInt(EndDate[2])) {
        if (parseInt(FromDate[0]) < parseInt(EndDate[0])) {
            val = 'true';
            return true;
        }
        else if (parseInt(FromDate[0]) == parseInt(EndDate[0])) {
            if (parseInt(FromDate[1]) <= parseInt(EndDate[1])) {

                val = 'true';
                return true;
            }
        }
    }
}
    </script>
    <script type="text/javascript">
        function CheckDateEalier(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Future Date Not Accepted for Date Of Birth.");
                sender._selectedDate = new Date();
                sender._textbox.set_Value("");
            }
        }
    </script>
    <%--  <script type="text/javascript">
           function ChangePanels1() {
               var checkbox = document.getElementById('<%=chkHandicap.ClientID %>');

              if (checkbox.checked == true) {
                  document.getElementById('<%=lblHandicap.ClientID %>').style.visibility = 'visible';
                  document.getElementById('<%=ddlHandicap.ClientID %>').style.visibility = 'visible';
              }
              else {
                  document.getElementById('<%=lblHandicap.ClientID %>').style.visibility = 'hidden';
                  document.getElementById('<%=ddlHandicap.ClientID %>').style.visibility = 'hidden';

              }
          }

        </script>--%>

    <%-- <script type="text/javascript">
        // Load the Google Transliterate API
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function PayHeadKannada() {
            //alert("Hi");


            var options = {
                sourceLanguage:
                google.elements.transliteration.LanguageCode.ENGLISH,
                destinationLanguage:
                [google.elements.transliteration.LanguageCode.KANNADA],
                shortcutKey: 'ctrl+e',
                transliterationEnabled: true
            };

            // Create an instance on TransliterationControl with the required
            // options.
            var control =
            new google.elements.transliteration.TransliterationControl(options);

            // Enable transliteration in the textbox with id
            // 'transliterateTextarea'.  
            control.makeTransliteratable(['ctl00_ContentPlaceHolder1_txtFirstNameKannada', 'ctl00_ContentPlaceHolder1_txtMiddleNameKannada', 'ctl00_ContentPlaceHolder1_txtLastNameKannada', 'ctl00_ContentPlaceHolder1_txtFatherNameKannada']);
        }
        google.setOnLoadCallback(PayHeadKannada);

        // here we make the handlers for after the UpdatePanel update
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);

        function InitializeRequest(sender, args) {
        }

        // this is called to re-init the google after update panel updates.
        function EndRequest(sender, args) {
            PayHeadKannada();

        }

    </script>--%>
    <script>
        function toggleIcon(e) {
            $(e.target)
                .prev('.colapse-heading')
                .find(".more-less")
                .toggleClass('fa-minus fa-plus');
        }
        $('.colapse-panel').on('hide.bs.collapse', toggleIcon);
        $('.colapse-panel').on('show.bs.collapse', toggleIcon);
    </script>
    <script>
        $(document).ready(function () {
            $(document).on("click", ".Emp-Name-Class", function () {
                sessionStorage.setItem("divGeneralInfo-status", "open");
            });

            var xy = sessionStorage.getItem("divGeneralInfo-status");
            if (xy == "open") {
                $(".divGeneralInfo").addClass("d-block");
            }
            else {
                $(".divGeneralInfo").removeClass("d-block");
            }
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $(document).on("click", ".Emp-Name-Class", function () {
                    sessionStorage.setItem("divGeneralInfo-status", "open");

                });

                var xy = sessionStorage.getItem("divGeneralInfo-status");
                if (xy == "open") {
                    $(".divGeneralInfo").addClass("d-block");
                }
                else {
                    $(".divGeneralInfo").removeClass("d-block");
                }
            });
        });
    </script>
    <script type="text/javascript">
        function CheckDateEalier(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Future Date Not Accepted for Date of Joining");
                sender._selectedDate = new Date();
                sender._textbox.set_Value("");
            }
        }
    </script>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
