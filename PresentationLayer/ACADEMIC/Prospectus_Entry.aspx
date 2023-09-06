<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Prospectus_Entry.aspx.cs" Inherits="ACADEMIC_Prospectus_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../Content/jquery.js"></script>--%>
    <script type="text/javascript">
        function ValidNumeric() {

            var charCode = (event.which) ? event.which : event.keyCode;
            if (charCode >= 48 && charCode <= 57)
            { return true; }
            else
            { return false; }
        }

        function ValidProsNo() {

            var charCode = (event.which) ? event.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || (charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode == 45)
            { return true; }
            else
            { return false; }
        }

    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
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

    <asp:UpdatePanel ID="updBatch" runat="server" ViewStateMode="Disabled" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="temp" runat="server" visible="true">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <%--<h3 class="box-title">Prospectus Entry</h3>--%>
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>

                            <div class="box-body">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">PROSPECTUS ENTRY</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">RE - PRINT RECEIPT</a>
                                        </li>
                                    </ul>

                                    <div class="tab-content" id="my-tab-content">
                                        <div class="tab-pane active" id="tab_1">
                                            <div class="box-body">
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Admission Batch</label>--%>
                                                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <%--<asp:TextBox ID="txtAdmBatch" runat="server" ReadOnly="true" ViewStateMode="Enabled" />--%>
                                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" ToolTip="Please Select Admission Batch" ReadOnly="true" ViewStateMode="Enabled"
                                                                    CssClass="form-control" data-select2-enable="true">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Student Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtStudName" runat="server" TabIndex="1" MaxLength="50" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" ToolTip="Please Enter Student Name" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvStudName" runat="server" ControlToValidate="txtStudName" Display="None"
                                                                    ErrorMessage="Please Enter Student Name" ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteStudent" runat="server" TargetControlID="txtStudName" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="fltname" runat="server" FilterType="UppercaseLetters" TargetControlID="txtStudName" />--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>School/Institute Name</label>--%>
                                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSchool" runat="server" TabIndex="2" AppendDataBoundItems="True" ToolTip="Please Select School/Institute Name" AutoPostBack="true" ViewStateMode="Enabled"
                                                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSchool" runat="server" ControlToValidate="ddlSchool" Display="None"
                                                                    ErrorMessage="Please Select School/Institute Name" ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label>Degree</label>--%>
                                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" AppendDataBoundItems="True" ToolTip="Please Select Degree" ViewStateMode="Enabled"
                                                                    CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None"
                                                                    ErrorMessage="Please Select Degree" ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Programme/Branch</label>--%>
                                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="4" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Please Select Programme/Branch" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ViewStateMode="Enabled">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None"
                                                                    ErrorMessage="Please Select Programme/Branch" ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSpecialisation" visible="false">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <label>Specialisation</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSpecialisation" runat="server" TabIndex="13" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" ViewStateMode="Enabled"
                                                                    ValidationGroup="submit" ToolTip="Please Select Specialisation">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Mobile</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMobile" runat="server" TabIndex="5" AppendDataBoundItems="True" ToolTip="Please Enter Mobile Number" ViewStateMode="Enabled"
                                                                    CausesValidation="True" CssClass="form-control" MaxLength="10" ValidationGroup="submit" onkeypress="return ValidNumeric()">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobile" Display="None"
                                                                    ErrorMessage="Please Enter Mobile Number" ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Email</label>
                                                                </div>
                                                                <asp:TextBox ID="txtEmail" runat="server" TabIndex="6" AppendDataBoundItems="True" ToolTip="Please Enter Email" ValidationGroup="submit" ViewStateMode="Enabled"
                                                                    CausesValidation="True" MaxLength="128" CssClass="form-control">
                                                                </asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="None" ErrorMessage="Please Enter Email"
                                                                    ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>--%>
                                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Invalid Email Address" ControlToValidate="txtEmail"
                                                                    ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" SetFocusOnError="True" Display="None" ValidationGroup="submit" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Prospectus No.</label>
                                                                </div>
                                                                <asp:TextBox ID="txtProspectusno" runat="server" TabIndex="7" MaxLength="20" ToolTip="Please Enter Prospectus Number" onkeypress="return ValidProsNo()" ViewStateMode="Enabled" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvProspectusno" runat="server" ControlToValidate="txtProspectusno" Display="None"
                                                                    ErrorMessage="Please Enter Prospectus Number" ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Receipt No</label>
                                                                </div>
                                                                <asp:TextBox ID="txtReceiptNo" runat="server" TabIndex="8" MaxLength="16" ToolTip="Please Enter Receipt  Number" onkeypress="return ValidNumeric()" ViewStateMode="Enabled" CssClass="form-control" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Total Amount</label>
                                                                </div>
                                                                <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="9" MaxLength="4" ToolTip="Please Enter Total Amount" onkeypress="return ValidNumeric()" ViewStateMode="Enabled" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvTotalAmount" runat="server" ControlToValidate="txtTotalAmount" Display="None"
                                                                    ErrorMessage="Please Enter Total Amount" ValidationGroup="submit" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>
                                                            </div>



                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Remark</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlRemark" runat="server" TabIndex="10" AppendDataBoundItems="True" ToolTip="Please Select School/ Institute Name" CssClass="form-control" data-select2-enable="true" ViewStateMode="Enabled">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                                                    <asp:ListItem Value="2">Paid</asp:ListItem>
                                                                    <asp:ListItem Value="3">Online</asp:ListItem>
                                                                    <asp:ListItem Value="4">Complimentary</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRemark" Display="None"
                                                                    ErrorMessage="Please Select Remark" ValidationGroup="submit" SetFocusOnError="True" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                            CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="11" OnClientClick="validateCaseSensitiveEmail();" />
                                                        <asp:Button ID="btnShowReport" runat="server" OnClick="btnShowReport_Click" TabIndex="12" Text="Prospectus Receipt" ToolTip="Prospectus Receipt"
                                                            CssClass="btn btn-info" Enabled="false" />
                                                        <asp:Button ID="btnExcelReport" runat="server" OnClick="btnExcelReport_Click" TabIndex="13" Text="Prospectus Report(Excel)  " ToolTip="Prospectus Report(Excel)"
                                                            CssClass="btn btn-info" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="14" />&nbsp;
                                                        
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false"
                                                            DisplayMode="List" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_2">
                                            <div>
                                                <asp:UpdateProgress ID="UpdprogReprint" runat="server" AssociatedUpdatePanelID="updReprint"
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
                                            <asp:UpdatePanel ID="updReprint" runat="server">
                                                <ContentTemplate>
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class=" note-div">
                                                                        <h5 class="heading">Note</h5>
                                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Enter Prospectus No.</span>  </p>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>SEARCH</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtprosno" runat="server" MaxLength="10" CssClass="form-control" TabIndex="1" ToolTip="Search by Prospectus No." onkeypress="return ValidNumeric()"></asp:TextBox>
                                                                </div>

                                                                <div class="col-lg-3 col-md-12 col-12 btn-footer mt-lg-4">
                                                                    <asp:Button ID="btnShow" runat="server" Text="Search" TabIndex="2" CssClass="btn btn-primary" OnClick="btnShow_Click" ToolTip="Search by Prospectus No." />
                                                                    <asp:HiddenField ID="TabName" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div id="divdet" runat="server" visible="false">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Student Details</h5>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-lg-6 col-md-6 col-12 pl-md-0 pl-3">
                                                                        <ul class="list-group list-group-unbordered">
                                                                            <li class="list-group-item"><b>Prospectus No :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblprosno" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Name :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblname" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Mobile No. :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblmob" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>E-Mail :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblmail" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>
                                                                                <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                                                :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblschool" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>

                                                                    <div class="col-lg-6 col-md-6 col-12 pl-md-0 pl-3">
                                                                        <ul class="list-group list-group-unbordered">
                                                                            <li class="list-group-item"><b>
                                                                                <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                                :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lbldegree" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                </a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblbranch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>
                                                                                <asp:Label ID="lblDYlvAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                                :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblbatch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Total Amount :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblamt" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>

                                                                    <div class="col-12 btn-footer pt-3">
                                                                        <asp:Button ID="btnreprint" runat="server" OnClick="btnreprint_Click" TabIndex="3" Text="Prospectus Receipt" ToolTip="Prospectus Receipt"
                                                                            CssClass="btn btn-info" Enabled="true" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="divMsg" runat="server"></div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnExcelReport" />
            <%--  <asp:PostBackTrigger ControlID="btnreprint" />--%>
        </Triggers>

    </asp:UpdatePanel>

    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <script type="text/javascript">
        function validateCaseSensitiveEmail(txtEmail) {
            var reg = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;
            if (reg.test(txtEmail)) {
                return true;
            }
            else {
                return false;
            }
        }

        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }
    </script>

    <script type="text/javascript">
        $(function () {
            debugger;
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab1";
            $('#dvtab a[href="#' + tabName + '"]').tab('show');
            $("#dvtab a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });
        $(function () {
            $('#btnreprint').click(function (e) { e.preventDefault(); }).click();
        });
    </script>


</asp:Content>

