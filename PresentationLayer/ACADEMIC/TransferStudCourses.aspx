<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TransferStudCourses.aspx.cs" Inherits="ACADEMIC_TransferStudCourses" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Student Subject Equivalence</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Equivalence</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEquivalence" runat="server" AppendDataBoundItems="True"
                                            CssClass="form-control" AutoPostBack="true"
                                            TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlEquivalence_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlEquivalence"
                                            Display="None" ErrorMessage="Please Select Equivalence" InitialValue="0" ValidationGroup="search"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Univ. Reg. No. / Admission No.</label>
                                        </div>
                                        <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control" ToolTip="Enter Admission No." TabIndex="1"
                                            placeholder="Univ. Reg. No./ Admission No." />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:Button ID="btnShow" runat="server" Text="Search" TabIndex="1" CssClass="btn btn-primary mt-3" OnClick="btnShow_Click" ValidationGroup="search" />
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtStudent" Display="None"
                                            ErrorMessage="Please Enter the Univ. Reg. No. / Admission No." SetFocusOnError="true" ValidationGroup="search" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                            ValidationGroup="search" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divdata" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Univ. Reg. No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True" /></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-7 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Regulation :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Admission No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEnrollno" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="divlv" runat="server" visible="false">
                                <div class="d-flex justify-content-between">
                                    <div class="sub-heading">
                                        <h5>Subject Details</h5>
                                    </div>
                                    <div class="mb-2">
                                        <asp:LinkButton ID="ButtonAdd" runat="server" ToolTip="Add Row" OnClick="ButtonAdd_Click1" CssClass="btn btn-primary" TabIndex="1"
                                            Enabled="true" Text=" Add Row"><i class="fa fa-plus"></i> ADD </asp:LinkButton>
                                    </div>
                                </div>

                                <asp:ListView ID="lvCourse" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                                    <LayoutTemplate>
                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblCanGradeCard" runat="server">
                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th>Sr. No.</th>
                                                        <th>Subject</th>
                                                        <th>Equivalent Subject Code</th>
                                                        <th>Equivalent Subject Name</th>
                                                        <th>Grade</th>
                                                        <th>Exam Type</th>
                                                        <th>New Grade</th>
                                                        <th>Cancel</th>
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
                                                <asp:Label ID="RANK" runat="server" Text='<%# Container.DataItemIndex + 1%>'></asp:Label>
                                                <%--  <asp:Label ID="RANK" runat="server" Text='<%#Eval("RANK") %>' />--%>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlNewCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlNewCourse_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlNewCourse" SetFocusOnError="true"
                                                    Display="None" ErrorMessage="Please Select Subject" InitialValue="0" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOldCCode" CssClass="form-control" runat="server" AutoComplete="False" MaxLength="9" TabIndex="1"
                                                    OnTextChanged="txtOldCCode_TextChanged">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOldCCode" Display="None"
                                                    ErrorMessage="Please Enter Equivalent Subject Code" SetFocusOnError="true" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOldCourse" runat="server" CssClass="form-control" AutoComplete="False" TabIndex="1"
                                                    OnTextChanged="txtOldCourse_TextChanged">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOldCourse" Display="None"
                                                    ErrorMessage="Please Enter Equivalent Subject Name" SetFocusOnError="true" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOldGrade" runat="server" onkeypress="return validateDecGrade(event)" MaxLength="2" CssClass="form-control"
                                                    AutoComplete="False" TabIndex="1" OnTextChanged="txtOldGrade_TextChanged">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtOldGrade" Display="None"
                                                    ErrorMessage="Please Enter Grade" SetFocusOnError="true" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                            </td>

                                            <td>
                                                <asp:DropDownList ID="ddlExamType" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true"
                                                    TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="0">Regular</asp:ListItem>
                                                    <asp:ListItem Value="1">BackLog</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlExamType" SetFocusOnError="true"
                                                    Display="None" ErrorMessage="Please Select Exam Type" InitialValue="-1" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                            </td>

                                            <td>
                                                <asp:DropDownList ID="ddlNewGrade" runat="server" CssClass="form-control" AutoPostBack="true" AppendDataBoundItems="true"
                                                    TabIndex="1" data-select2-enable="true"
                                                    OnTextChanged="ddlNewGrade_TextChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlNewGrade" SetFocusOnError="true"
                                                    Display="None" ErrorMessage="Please Select New Grade" InitialValue="0" ValidationGroup="Submit">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <%--<td style="width: 148px">
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>--%>
                                            <td>
                                                <asp:ImageButton ID="lnkRemove" runat="server" OnClick="lnkRemove_Click" ImageUrl="~/Images/Delete.png"
                                                    AlternateText="Remove Row" OnClientClick="return ConfirmCancel();" TabIndex="1"
                                                    CommandArgument='<%# Container.DataItemIndex + 1%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="1" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="1" Enabled="false" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="1" Text="Cancel" />


                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="Submit" />
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg" Style="color: green"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnReport" />--%>
        </Triggers>

    </asp:UpdatePanel>

    <div id="divMsg" runat="server" />

    <script type="text/javascript">

        function validateDecGrade(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes
            if ((keycode < 65 || keycode > 90)) {
                return false;
            }
            else {
                return true;
            }

            var result = (n - Math.floor(n)) !== 0;

            if (result)
                return true;
            else
                return false;
        }

    </script>

    <script type="text/javascript">
        function ConfirmCancel() {
            if (confirm('Are you sure you want delete this row?')) {
                return true;
            }
            return false;
        }
    </script>

    <script type="text/javascript">
        function ConfirmSubmit() {
            if (Page_ClientValidate('submit')) {
                return confirm('Are you sure you want submit?');
            }
            return false;
        }
    </script>

</asp:Content>
