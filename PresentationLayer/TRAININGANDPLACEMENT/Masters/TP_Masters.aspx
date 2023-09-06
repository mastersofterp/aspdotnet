<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TP_Masters.aspx.cs" Inherits="TRAININGANDPLACEMENT_Masters_TP_Masters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive2" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive3" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive4" runat="server" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hfdActive5" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive6" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive7" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive8" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdActive9" runat="server" ClientIDMode="Static" />
    <style>
        .nav-link {
            display: block;
            padding: 0.5rem 0.5rem;
        }
    </style>

    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }
    </style>
    <%-- <asp:UpdatePanel ID="upnlmain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">T&P Masters</h3>
                </div>
                <div id="Tabs" role="tabpanel">
                    <div class="box-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" data-toggle="tab" href="#tab_1">Tie-ups and MOUs</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_2">Internship</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_3">Industry Association</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_4">Expert Talk</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_5">Alumni Interaction</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_6">Contest Based Hiring</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_7">Foreign Language</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_8">Industry Visits</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#tab_9">Placement Talk</a>
                                </li>
                            </ul>

                            <div class="tab-content" id="my-tab-content">
                                <div class="tab-pane active" id="tab_1">
                                    <asp:UpdatePanel ID="upntab1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAccYear" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rqfAccYear" runat="server" ControlToValidate="ddlAccYear"
                                                            ValidationGroup="TP" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment" ValidationGroup="TP"
                                                            ErrorMessage="Please select Department " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Organization </label>
                                                        </div>
                                                        <asp:TextBox ID="txtOrganization" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOrganization" runat="server" ControlToValidate="txtOrganization"
                                                            ValidationGroup="TP"
                                                            ErrorMessage="Please select Name Of Organization " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Nature of MOUs </label>
                                                        </div>
                                                        <asp:TextBox ID="txtNatureMOU" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvNatureMOU" runat="server" ControlToValidate="txtNatureMOU"
                                                            Display="None" ErrorMessage="Please Enter Nature of MOUs" SetFocusOnError="true"
                                                            ValidationGroup="TP"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Benefits to Students and Staff </label>
                                                        </div>
                                                        <asp:TextBox ID="txtBenefits" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvBenefits" runat="server" ControlToValidate="txtBenefits"
                                                            ValidationGroup="TP"
                                                            ErrorMessage="Please select Benefits to Students and Staff " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Collaboration Year  </label>
                                                        </div>
                                                        <asp:TextBox ID="txtCollaborationYear" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCollaborationYear" runat="server" ControlToValidate="txtCollaborationYear"
                                                            Display="None" ErrorMessage="Please Enter Collaboration Year" SetFocusOnError="true" ValidationGroup="TP"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Collaboration Expired year </label>
                                                        </div>
                                                        <asp:TextBox ID="txtCollaborationExpYear" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCollaborationExpYear" runat="server" ControlToValidate="txtCollaborationExpYear"
                                                            Display="None" ErrorMessage="Please Enter Collaboration Expired year" SetFocusOnError="true" ValidationGroup="TP"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Claim </label>
                                                        </div>
                                                        <asp:TextBox ID="txtClaim" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvClaim" runat="server" ControlToValidate="txtClaim"
                                                            ValidationGroup="TP"
                                                            ErrorMessage="Please select Claim" SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active1" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active1"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validate1()" ValidationGroup="TP"></asp:LinkButton>
                                                <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="TP" OnClick="btnSubmit_Click" OnClientClick="return validate1()"/>--%>
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                                <asp:ValidationSummary ID="vsEmp" runat="server" ValidationGroup="TP" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-12">
                                                <asp:ListView ID="lvTPDmaster" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap  display" style="width: 100%;">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit
                                                                    </th>
                                                                    <th>Academic Year </th>
                                                                    <th>Department
                                                                    </th>
                                                                    <th>Name Of Organization
                                                                    </th>
                                                                    <th>Nature Of MOUs
                                                                    </th>
                                                                    <th>Benefits to Student/Staff
                                                                    </th>
                                                                    <th>Collaboration Year
                                                                    </th>
                                                                    <th>Collaboration Expired Year
                                                                    </th>
                                                                    <th>Claim
                                                                    </th>
                                                                    <th>Status</th>
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
                                                                <asp:ImageButton ID="btnFDREdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                    AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="1" OnClick="btnFDREdit_Click" />
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("YEARNAME")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("NAME_OF_ORGNIZATION")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("NAME_OF_MOUS")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("BENEFITS_TO_STUDENTS_AND_STAFF")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("COLLABORATION_YEAR")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("COLLABORATION_EXPIRED_YEAR")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblFdSrNo" runat="server" Text='<%# Eval("CLAIM")%>'></asp:Label></td>
                                                            <td class="text-center">
                                                                <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="tab_2">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropDownList1"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList2"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Department " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Student </label>
                                                        </div>
                                                        <asp:TextBox ID="txtNameStudent" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNameStudent"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Name Of Student " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Company/Institute </label>
                                                        </div>
                                                        <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCompany"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Name Of Company/Institute " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Company Address </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Company Address " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Contact Person </label>
                                                        </div>
                                                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator88" runat="server" ControlToValidate="txtContactPerson"
                                                            ValidationGroup="TP2" ErrorMessage="Please Enter Contact Person " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Email ID </label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmailID"
                                                            Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="TP2"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator89" runat="server" ControlToValidate="txtEmailID"
                                                            ValidationGroup="TP2" ErrorMessage="Please Enter Email ID " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                            ControlToValidate="txtMobileNo" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Mobile No." SetFocusOnError="true" ValidationGroup="TP2"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Company Website </label>
                                                        </div>
                                                        <asp:TextBox ID="txtCompanyWebiste" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" ControlToValidate="txtCompanyWebiste"
                                                            ValidationGroup="TP2" ErrorMessage="Please Enter Company Website" SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Internship Duration </label>
                                                        </div>
                                                        <asp:TextBox ID="txtInternshipDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtInternshipDuration"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Internship Duration" SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Class </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%--      <asp:ListItem Value="1">SY</asp:ListItem>
                                                <asp:ListItem Value="2">TY</asp:ListItem>
                                                <asp:ListItem Value="3">BE</asp:ListItem>
                                                <asp:ListItem Value="4">B.Tech</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlClass"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Class " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Level </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlLevel" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%-- <asp:ListItem Value="1">International</asp:ListItem>
                                                <asp:ListItem Value="2">National</asp:ListItem>
                                                <asp:ListItem Value="3">State</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlLevel"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Level " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mode of Internship </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlModeInternship" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Online</asp:ListItem>
                                                            <asp:ListItem Value="2">Offline</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlModeInternship"
                                                            ValidationGroup="TP2" ErrorMessage="Please select Mode of Internship " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active2" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active2"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">

                                                <asp:LinkButton ID="Button1" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="Button1_Click" OnClientClick="return validate2()" ValidationGroup="TP2"></asp:LinkButton>
                                                <%--<asp:Button ID="Button1" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" OnClick="Button1_Click" OnClientClick="return validate2()" ValidationGroup="TP2" />--%>
                                                <asp:Button ID="btntab2Cancel" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="btntab2Cancel_Click" />
                                                <asp:Button ID="btnShow1" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow_Click1" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="TP2" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-12">
                                                    <asp:ListView ID="ListView2" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblInternship">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Academic Year </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Name Of Student
                                                                        </th>
                                                                        <th>Name Of Company
                                                                        </th>
                                                                        <th>Contact Person
                                                                        </th>
                                                                        <th>Email ID
                                                                        </th>
                                                                        <th>Mobile No.
                                                                        </th>
                                                                        <th>Company Website
                                                                        </th>
                                                                        <th>Internship Duration</th>
                                                                        <th>Class</th>
                                                                        <th>Level</th>
                                                                        <th>Mode of Internship</th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnFDREdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="1" OnClick="btnFDREdit_Click1" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label10" runat="server" Text='<%# Eval("NAME_OF_STUDENT")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("NAME_OF_COMPANY_INSTITUTE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("CONTACT_PERSON")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("EMAIL_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("MOBILE_NO")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("COMPANY_WEBSITE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label16" runat="server" Text='<%# Eval("INTERNSHIP_DURATION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label17" runat="server" Text='<%# Eval("CLASS_NO")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label18" runat="server" Text='<%# Eval("LEVEL_NO")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label19" runat="server" Text='<%# Eval("MODE_OF_INTERNSHIP")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="LabelIS" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>' Style="font-size: 10px"></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="tab_3">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcademicYear3" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlAcademicYear3"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartment3" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlDepartment3"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Department " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Advisor </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAdvisor" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtAdvisor"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Name Of Advisor" SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Designation of Advisor </label>
                                                        </div>
                                                        <asp:TextBox ID="txtDesignationAdvisor" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtDesignationAdvisor"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Designation of Advisor " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Advisor's Company Name </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAdvisorCampanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtAdvisorCampanyName"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Advisor's Company Name " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Location </label>
                                                        </div>
                                                        <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtLocation"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Location " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Email ID </label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmailID3" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator87" runat="server" ControlToValidate="txtEmailID3"
                                                            ValidationGroup="TP3" ErrorMessage="Please Enter Email ID " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailID3"
                                                            Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ValidationGroup="TP3"></asp:RegularExpressionValidator>
                                                        <%--"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtMobileNo3" runat="server" CssClass="form-control" MaxLength="10" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                            ControlToValidate="txtMobileNo3" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Mobile No." SetFocusOnError="true" ValidationGroup="TP3"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Expertee Domain </label>
                                                        </div>
                                                        <asp:TextBox ID="txtExperteeDomain" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtExperteeDomain"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Expertee Domain " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Credit Claim </label>
                                                        </div>
                                                        <asp:TextBox ID="txtCreditClaim" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtCreditClaim"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Credit Claim " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Staff Coordinator</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStaffCoordinator" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtStaffCoordinator"
                                                            ValidationGroup="TP3" ErrorMessage="Please select Staff Coordinator " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active3" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active3"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btntabSubmit3" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="btntabSubmit3_Click" OnClientClick="return validate3()" ValidationGroup="TP3"></asp:LinkButton>
                                                <%--<asp:Button ID="btntabSubmit3" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="TP3" OnClick="btntabSubmit3_Click" />--%>
                                                <asp:Button ID="Button4" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="Button4_Click" />
                                                <asp:Button ID="btnShow2" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow_Click2" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="TP3" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div class="col-12">
                                               

                                                    <asp:ListView ID="ListView3" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblIndustryAssociation">
                                                                <thead class="bg-light-blue" >
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Academic Year </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Name Of Advisor
                                                                        </th>
                                                                        <th>Designation of Advisor
                                                                        </th>
                                                                        <th>Advisor's Company Name
                                                                        </th>
                                                                        <th>Location</th>
                                                                        <th>Email ID
                                                                        </th>
                                                                        <th>Mobile No.
                                                                        </th>
                                                                        <th>Expertee Domain
                                                                        </th>
                                                                        <th>Credit Claim</th>
                                                                        <th>Staff Coordinator</th>
                                                                        <th>Status</th>
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
                                                                    <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/edit.png" />--%>

                                                                    <asp:ImageButton ID="btnFDREdittab3" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="3" OnClick="btnFDREdittab3_Click" />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label31" runat="server" Text='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label32" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label33" runat="server" Text='<%# Eval("NAME_OF_ADVISOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label34" runat="server" Text='<%# Eval("DESIGNATION_OF_ADVISOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label35" runat="server" Text='<%# Eval("ADVISOR_COMPANY_NAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Eval("LOCATION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Eval("EMAIL_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label38" runat="server" Text='<%# Eval("MOBILE_NO")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label39" runat="server" Text='<%# Eval("EXPERT_DOMAIN")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label40" runat="server" Text='<%# Eval("CREDIT_CLAIM")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label41" runat="server" Text='<%# Eval("MODE_OF_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label42" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>

                                                                <%--<td style="color:green">Active</td>--%>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>


                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="tab_4">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="DropDownList5"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="DropDownList6"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Department " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Expert </label>
                                                        </div>
                                                        <asp:TextBox ID="txtExpert" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtExpert"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Name Of Expert " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Designation of Expert </label>
                                                        </div>
                                                        <asp:TextBox ID="txtDesignationExpert" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtDesignationExpert"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Designation of Expert " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Expert's Company Name </label>
                                                        </div>
                                                        <asp:TextBox ID="txtExpertCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtExpertCompanyName"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Expert's Company Name " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Email ID </label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator85" runat="server" ControlToValidate="TextBox7"
                                                            ValidationGroup="TP4" ErrorMessage="Please Enter Email ID " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox7"
                                                            Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="TP4"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" MaxLength="10" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server"
                                                            ControlToValidate="TextBox8" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Mobile No." SetFocusOnError="true" ValidationGroup="TP4"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Topic Of Interaction </label>
                                                        </div>
                                                        <asp:TextBox ID="txtTopicInteraction" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtTopicInteraction"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Topic Of Interaction  " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Interaction  </label>
                                                        </div>

                                                        <div class="input-group date">
                                                            <div class="input-group-addon" id="Image3">
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtDateInteraction" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator86" runat="server" ControlToValidate="txtDateInteraction"
                                                                ValidationGroup="TP4" ErrorMessage="Please Enter Date Of Interaction  " SetFocusOnError="true"
                                                                Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image3" PopupPosition="BottomLeft"
                                                                TargetControlID="txtDateInteraction">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDateInteraction">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mode </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlMode" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Online</asp:ListItem>
                                                            <asp:ListItem Value="2">Offline</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="ddlMode"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Mode " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Class </label>
                                                        </div>
                                                        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%--<asp:ListItem Value="1">SY</asp:ListItem>
                                                <asp:ListItem Value="2">TY</asp:ListItem>
                                                <asp:ListItem Value="3">BE</asp:ListItem>
                                                <asp:ListItem Value="4">B.Tech</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="DropDownList7"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Class " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>No. of Student Benefitted</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStdBenefitted" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtStdBenefitted"
                                                            ValidationGroup="TP4" ErrorMessage="Please select No. of Student Benefitted  " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Staff Coordinator</label>
                                                        </div>
                                                        <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="TextBox11"
                                                            ValidationGroup="TP4" ErrorMessage="Please select Staff Coordinator  " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active4" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active4"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="Button5" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="Button5_Click" OnClientClick="return validate4()" ValidationGroup="TP4"></asp:LinkButton>
                                                <%-- <asp:Button ID="Button5" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="TP4" OnClick="Button5_Click" />--%>
                                                <asp:Button ID="Button6" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="Button6_Click" />
                                                <asp:Button ID="btnShow4" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow4_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="TP4" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-12">
                                              
                                                    <asp:ListView ID="ListView4" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblExpertTalk">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Academic Year </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Name Of Expert
                                                                        </th>
                                                                        <th>Designation of Expert
                                                                        </th>
                                                                        <th>Expert's Company Name
                                                                        </th>
                                                                        <th>Email ID
                                                                        </th>
                                                                        <th>Mobile No.
                                                                        </th>
                                                                        <th>Topic of Interaction
                                                                        </th>
                                                                        <th>Date of Interaction</th>
                                                                        <th>Mode</th>
                                                                        <th>Class</th>
                                                                        <th>No. of Students Benefitted</th>
                                                                        <th>Staff Coordinator</th>
                                                                        <th>Status</th>
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
                                                                    <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/edit.png" />--%>

                                                                    <asp:ImageButton ID="btnFDREdittab4" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="4" OnClick="btnFDREdittab4_Click" />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label31" runat="server" Text='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label32" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label33" runat="server" Text='<%# Eval("NAME_OF_EXPERT")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label34" runat="server" Text='<%# Eval("DESIGNATION_OF_EXPERT")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label35" runat="server" Text='<%# Eval("EXPERT_COMPANY_NAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label36" runat="server" Text='<%# Eval("EMAIL_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label37" runat="server" Text='<%# Eval("MOBILE_NO")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label38" runat="server" Text='<%# Eval("TOPIC_OF_INTERACTION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label39" runat="server" Text='<%# Eval("DATE_OF_INTERACTION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label40" runat="server" Text='<%# Eval("MODE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label21" runat="server" Text='<%# Eval("CLASS")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label41" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT_BENEFITTED")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label42" runat="server" Text='<%# Eval("NAME_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                                <td style="color: green">
                                                                    <asp:Label ID="Label43" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>



                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>


                                <div class="tab-pane fade" id="tab_5">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcademicYeartab5" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlAcademicYeartab5"
                                                            ValidationGroup="TP5" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartmenttab5" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlDepartmenttab5"
                                                            ValidationGroup="TP5" ErrorMessage="Please select Department " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Alumni </label>
                                                        </div>
                                                        <asp:TextBox ID="txtNameAlumni" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtNameAlumni"
                                                            ValidationGroup="TP5" ErrorMessage="Please select Name Of Alumni " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Designation of Alumni </label>
                                                        </div>
                                                        <asp:TextBox ID="txtDesignationAlumni" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtDesignationAlumni"
                                                            ValidationGroup="TP5" ErrorMessage="Please select Designation of Alumni " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Year of Passout </label>
                                                        </div>
                                                        <asp:TextBox ID="txtYearPassout" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtYearPassout"
                                                            ValidationGroup="TP5" ErrorMessage="Please select Year of Passout " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Company Name </label>
                                                        </div>
                                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="txtCompanyName"
                                                            ValidationGroup="TP5" ErrorMessage="Please select Company Name " SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Email ID </label>
                                                        </div>
                                                        <asp:TextBox ID="txtemailidtab5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" ControlToValidate="txtemailidtab5"
                                                            ValidationGroup="TP5" ErrorMessage="Please Enter Email ID" SetFocusOnError="true"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtemailidtab5"
                                                            Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="TP5"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtmobilenotab5" runat="server" CssClass="form-control" MaxLength="10" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server"
                                                            ControlToValidate="txtmobilenotab5" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Mobile No." SetFocusOnError="true" ValidationGroup="TP5"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Topic Of Interaction </label>
                                                        </div>
                                                        <asp:TextBox ID="txtTopicOfInteractiontab5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server"
                                                            ControlToValidate="txtTopicOfInteractiontab5" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Topic Of Interaction." SetFocusOnError="true" ValidationGroup="TP5"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Interaction  </label>
                                                        </div>


                                                        <div class="input-group date">
                                                            <div class="input-group-addon" id="Image4">
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtDateOfInteractiontab5" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator84" runat="server"
                                                                ControlToValidate="txtDateOfInteractiontab5" EnableClientScript="true" Display="None"
                                                                ErrorMessage="Please Enter Date Of Interaction." SetFocusOnError="true" ValidationGroup="TP5"></asp:RequiredFieldValidator>

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image4" PopupPosition="BottomLeft"
                                                                TargetControlID="txtDateOfInteractiontab5">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDateOfInteractiontab5">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mode </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlModetab5" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Online</asp:ListItem>
                                                            <asp:ListItem Value="2">Offline</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="ddlModetab5"
                                                            ValidationGroup="TP5" ErrorMessage="Please select Mode." SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Class </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlClassTab5" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <%--<asp:ListItem Value="1">SY</asp:ListItem>
                                                <asp:ListItem Value="2">TY</asp:ListItem>
                                                <asp:ListItem Value="3">BE</asp:ListItem>
                                                <asp:ListItem Value="4">B.Tech</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="ddlClassTab5"
                                                            ValidationGroup="TP5" ErrorMessage="Please select Class." SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>No. of Student Benefitted</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStudentBenefittedtab5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server"
                                                            ControlToValidate="txtStudentBenefittedtab5" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter No. of Student Benefitted." SetFocusOnError="true" ValidationGroup="TP5"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Staff Coordinator</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStaffCoordinatortab5" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server"
                                                            ControlToValidate="txtStaffCoordinatortab5" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Staff Coordinator." SetFocusOnError="true" ValidationGroup="TP5">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active5" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active5"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmitTab5" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="btnSubmitTab5_Click" OnClientClick="return validate5()" ValidationGroup="TP5"></asp:LinkButton>
                                                <%--<asp:Button ID="btnSubmitTab5" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="TP5" OnClick="btnSubmitTab5_Click" />--%>
                                                <asp:Button ID="btnCancelTab5" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="btnCancelTab5_Click" />
                                                <asp:Button ID="btnShow5" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow5_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="TP5" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-12"> 
                                                    <asp:ListView ID="ListView5" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblAlumniInteraction">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Academic Year </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Name Of Alumni
                                                                        </th>
                                                                        <th>Designation of Alumni
                                                                        </th>
                                                                        <th>Alumni's Company Name
                                                                        </th>
                                                                        <th>Email ID
                                                                        </th>
                                                                        <th>Mobile No.
                                                                        </th>
                                                                        <th>Topic of Interaction
                                                                        </th>
                                                                        <th>Date of Interaction</th>
                                                                        <th>Mode</th>
                                                                        <th>Class</th>
                                                                        <th>No. of Students Benefitted</th>
                                                                        <th>Staff Coordinator</th>
                                                                        <th class="text-center">Status</th>
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
                                                                    <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/edit.png" />--%>

                                                                    <asp:ImageButton ID="btnFDREdittab5" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="5" OnClick="btnFDREdittab5_Click" />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label51" runat="server" Text='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label52" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label53" runat="server" Text='<%# Eval("NAME_OF_ALUMNI")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label54" runat="server" Text='<%# Eval("DESIGNATION_OF_ALUMNI")%>'></asp:Label></td>
                                                                <%-- <td><asp:Label ID="Label55" runat="server" Text='<%# Eval("YEAR_OF_PASSOUT")%>'></asp:Label></td>--%>
                                                                <td>
                                                                    <asp:Label ID="Label56" runat="server" Text='<%# Eval("COMPANY_NAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label57" runat="server" Text='<%# Eval("EMAIL_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label58" runat="server" Text='<%# Eval("MOBILE_NO")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label59" runat="server" Text='<%# Eval("TOPIC_OF_INTERACTION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label60" runat="server" Text='<%# Eval("DATE_OF_INTERACTION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label71" runat="server" Text='<%# Eval("MODE_OF_INTERACTION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label72" runat="server" Text='<%# Eval("CLASS")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label73" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT_BENEFITTED")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label74" runat="server" Text='<%# Eval("NAME_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                                <td class="text-center"><%--style="color: green"--%>
                                                                    <asp:Label ID="Label75" CssClass="badge" runat="server" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>



                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="tab_6">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcademicYear6" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="ddlAcademicYear6"
                                                            ValidationGroup="TP6" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartment6" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="ddlDepartment6"
                                                            ValidationGroup="TP6" ErrorMessage="Please select Department " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Event </label>
                                                        </div>
                                                        <asp:TextBox ID="txtEvent" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server"
                                                            ControlToValidate="txtEvent" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Name Of Event ." SetFocusOnError="true" ValidationGroup="TP6">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Number of Students Participated </label>
                                                        </div>
                                                        <asp:TextBox ID="txtNoStdParticipated" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server"
                                                            ControlToValidate="txtNoStdParticipated" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Number of Students Participated ." SetFocusOnError="true" ValidationGroup="TP6">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Achievement </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAchievement" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server"
                                                            ControlToValidate="txtAchievement" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Achievement ." SetFocusOnError="true" ValidationGroup="TP6">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Award Amount in Rs. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAmountRS" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server"
                                                            ControlToValidate="txtAmountRS" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Award Amount in Rs ." SetFocusOnError="true" ValidationGroup="TP6">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Number of Student Placed </label>
                                                        </div>
                                                        <asp:TextBox ID="txtNoStdPlaced" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server"
                                                            ControlToValidate="txtNoStdPlaced" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Number of Student Placed." SetFocusOnError="true" ValidationGroup="TP6">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Financial Assistance from Institute </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFinancialAss" runat="server" CssClass="form-control" data-select2-enable="true" >
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="2">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="ddlFinancialAss"
                                                            ValidationGroup="TP6" ErrorMessage="Please select Financial Assistance from Institute " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Financial Assistance from Institute in Rs.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFinancialAss" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                   <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server"
                                                            ControlToValidate="txtFinancialAss" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Financial Assistance from Institute in Rs." SetFocusOnError="true" ValidationGroup="TP6">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Remark</label>
                                                        </div>
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Staff Coordinator</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStaffCoordinator6" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server"
                                                            ControlToValidate="txtStaffCoordinator6" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Staff Coordinator." SetFocusOnError="true" ValidationGroup="TP6">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active6" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active6"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmittab6" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="btnSubmittab6_Click" OnClientClick="return validate6()" ValidationGroup="TP6"></asp:LinkButton>
                                                <%--<asp:Button ID="btnSubmittab6" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="TP6" OnClick="btnSubmittab6_Click" />--%>
                                                <asp:Button ID="btncancelTab6" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="btncancelTab6_Click" />
                                                <asp:Button ID="btnShow6" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow6_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="TP6" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-12">
                                                    <asp:ListView ID="ListView6" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblContestBasedHiring">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Academic Year </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Name Of Event
                                                                        </th>
                                                                        <th>No. of Students Participated
                                                                        </th>
                                                                        <th>Achievement</th>
                                                                        <th>No. of Student Placed</th>
                                                                        <th>Staff Coordinator</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>

                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>

                                                                <tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/edit.png" />--%>

                                                                    <asp:ImageButton ID="btnFDREdittab6" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="6" OnClick="btnFDREdittab6_Click" />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label61" runat="server" Text='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label62" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label63" runat="server" Text='<%# Eval("NAME_OF_EVENT")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label64" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT_PARTICIPATED")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label65" runat="server" Text='<%# Eval("ACHIEVEMENT")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label66" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT_PLACED")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label67" runat="server" Text='<%# Eval("NAME_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label68" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                                <%--<td><asp:Label ID="Label60" runat="server" Text='<%# Eval("DATE_OF_INTERACTION")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label71" runat="server" Text='<%# Eval("MODE_OF_INTERACTION")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label72" runat="server" Text='<%# Eval("CLASS")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label73" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT_BENEFITTED")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label74" runat="server" Text='<%# Eval("NAME_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label75" runat="server" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>--%>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="tab_7">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcademicYear7" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="ddlAcademicYear7"
                                                            ValidationGroup="TP7" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartment7" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="ddlDepartment7"
                                                            ValidationGroup="TP7" ErrorMessage="Please select Department. " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Student </label>
                                                        </div>
                                                        <asp:TextBox ID="txtNameStd" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server"
                                                            ControlToValidate="txtNameStd" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Name Of Student." SetFocusOnError="true" ValidationGroup="TP7">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Foreign Language </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlForeignLanguage" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Japanese</asp:ListItem>
                                                <asp:ListItem Value="2">German</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="ddlForeignLanguage"
                                                            ValidationGroup="TP7" ErrorMessage="Please Foreign Language. " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Certification </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCertification" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                            <asp:ListItem Value="2">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="ddlCertification"
                                                            ValidationGroup="TP7" ErrorMessage="Please Certification. " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Level </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlLevel7" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">International</asp:ListItem>
                                                <asp:ListItem Value="2">National</asp:ListItem>
                                                <asp:ListItem Value="3">State</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="ddlLevel7"
                                                            ValidationGroup="TP7" ErrorMessage="Please select Level " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Level of Certification</label>
                                                        </div>
                                                        <asp:TextBox ID="txtLevelCertification" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server"
                                                            ControlToValidate="txtLevelCertification" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Level of Certification." SetFocusOnError="true" ValidationGroup="TP7">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Staff Coordinator</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStaffCoordinator7" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server"
                                                            ControlToValidate="txtStaffCoordinator7" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Staff Coordinator." SetFocusOnError="true" ValidationGroup="TP7">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active7" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active7"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnsubmittab7" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="btnsubmittab7_Click" OnClientClick="return validate7()" ValidationGroup="TP7"></asp:LinkButton>
                                                <%-- <asp:Button ID="btnsubmittab7" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="TP7" OnClick="btnsubmittab7_Click" />--%>
                                                <asp:Button ID="btncanceltab7" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="btncanceltab7_Click" />
                                                <asp:Button ID="btnShow7" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow7_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="TP7" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />

                                            </div>
                                            <div class="col-12">
                                                    <asp:ListView ID="ListView7" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblForeginLanguage">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Academic Year </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Name Of Student
                                                                        </th>
                                                                        <th>Foreign Language
                                                                        </th>
                                                                        <th>Certification</th>
                                                                        <th>Level</th>
                                                                        <th>Level of Certification</th>
                                                                        <th>Staff Coordinator</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>

                                                                <tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/edit.png" />--%>

                                                                    <asp:ImageButton ID="btnFDREdittab7" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="7" OnClick="btnFDREdittab7_Click" />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label611" runat="server" Text='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label622" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label633" runat="server" Text='<%# Eval("NAME_OF_STUDENT")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label644" runat="server" Text='<%# Eval("FOREGIN_LANGUAGE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label655" runat="server" Text='<%# Eval("CERTIFICATION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label666" runat="server" Text='<%# Eval("LEVEL_NO")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label677" runat="server" Text='<%# Eval("LEVEL_OF_CERTIFICATION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label22" runat="server" Text='<%# Eval("NAME_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                                <td style="color: green">
                                                                    <asp:Label ID="Label688" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                                <%--<td><asp:Label ID="Label60" runat="server" Text='<%# Eval("DATE_OF_INTERACTION")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label71" runat="server" Text='<%# Eval("MODE_OF_INTERACTION")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label72" runat="server" Text='<%# Eval("CLASS")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label73" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT_BENEFITTED")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label74" runat="server" Text='<%# Eval("NAME_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                    <td><asp:Label ID="Label75" runat="server" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>--%>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="tab_8">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcademicYear8" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="ddlAcademicYear8"
                                                            ValidationGroup="TP8" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartment8" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlDepartment8"
                                                            ValidationGroup="TP8" ErrorMessage="Please select Department. " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Company/Institute </label>
                                                        </div>
                                                        <asp:TextBox ID="txtCompanyInstitute" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server"
                                                            ControlToValidate="txtCompanyInstitute" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Name Of Company/Institute." SetFocusOnError="true" ValidationGroup="TP8">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Location </label>
                                                        </div>
                                                        <asp:TextBox ID="txtLocations" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server"
                                                            ControlToValidate="txtLocations" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Location." SetFocusOnError="true" ValidationGroup="TP8">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Address </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAddres" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server"
                                                            ControlToValidate="txtAddres" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Address." SetFocusOnError="true" ValidationGroup="TP8">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Email ID </label>
                                                        </div>
                                                        <asp:TextBox ID="txtemailid8" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server"
                                                            ControlToValidate="txtemailid8" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Email Id." SetFocusOnError="true" ValidationGroup="TP8">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtemailid8"
                                                            Display="None" ErrorMessage="Invalid email address" SetFocusOnError="True"
                                                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ValidationGroup="TP8">
                                                        </asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txtmobileno8" runat="server" CssClass="form-control" MaxLength="10" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server"
                                                            ControlToValidate="txtmobileno8" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Mobile No." SetFocusOnError="true" ValidationGroup="TP8"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Website </label>
                                                        </div>
                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server"
                                                            ControlToValidate="txtWebsite" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Website." SetFocusOnError="true" ValidationGroup="TP8"></asp:RequiredFieldValidator>
                                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtWebsite"
                                                            Display="None" ErrorMessage="Enter Email Id Correctly" SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="TP8">
                                                        </asp:RegularExpressionValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Visit  </label>
                                                        </div>
                                                        <%-- <asp:TextBox ID="txtDateVisit" runat="server" CssClass="form-control"></asp:TextBox>--%>

                                                        <div class="input-group date">
                                                            <div class="input-group-addon" id="Image5">
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtDateVisit" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator82" runat="server"
                                                                ControlToValidate="txtDateVisit" EnableClientScript="true" Display="None"
                                                                ErrorMessage="Please Enter Date Of Visit." SetFocusOnError="true" ValidationGroup="TP8"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image5" PopupPosition="BottomLeft"
                                                                TargetControlID="txtDateVisit">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDateVisit">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Class </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlClass8" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">SY</asp:ListItem>
                                                <asp:ListItem Value="2">TY</asp:ListItem>
                                                <asp:ListItem Value="3">BE</asp:ListItem>
                                                <asp:ListItem Value="4">B.Tech</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" ControlToValidate="ddlClass8"
                                                            ValidationGroup="TP8" ErrorMessage="Please select Class. " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>No. of Student Visited</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStdVisited" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator69" runat="server"
                                                            ControlToValidate="txtStdVisited" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter No. of Student Visited." SetFocusOnError="true" ValidationGroup="TP8">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Staff Coordinator</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStaffCoordinator8" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator70" runat="server"
                                                            ControlToValidate="txtStaffCoordinator8" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Staff Coordinator." SetFocusOnError="true" ValidationGroup="TP8">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active8" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active8"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnsubmittab8" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="btnsubmittab8_Click" OnClientClick="return validate8()" ValidationGroup="TP8"></asp:LinkButton>
                                                <%--<asp:Button ID="btnsubmittab8" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="TP8" OnClick="btnsubmittab8_Click" />--%>
                                                <asp:Button ID="btncanceltab8" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="btncanceltab8_Click" />
                                                <asp:Button ID="btnShow8" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow8_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="TP8" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-12">
                                                    <asp:ListView ID="ListView8" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblIndustryVisits">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Academic Year </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Name Of Company
                                                                        </th>
                                                                        <th>Location
                                                                        </th>
                                                                        <th>Email ID
                                                                        </th>
                                                                        <th>Mobile No.
                                                                        </th>
                                                                        <th>Website
                                                                        </th>
                                                                        <th>Date of Visit</th>
                                                                        <th>Class</th>
                                                                        <th>No. of Students Visited</th>
                                                                        <th>Staff Coordinator</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>

                                                                <tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/edit.png" />--%>

                                                                    <asp:ImageButton ID="btnFDREdittab8" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="8" OnClick="btnFDREdittab8_Click" />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label001" runat="server" Text='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label002" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label003" runat="server" Text='<%# Eval("NAME_OF_COMPANY")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label004" runat="server" Text='<%# Eval("LOCATION")%>'></asp:Label></td>
                                                                <%--<td><asp:Label ID="Label005" runat="server" Text='<%# Eval("ADDRESS")%>'></asp:Label></td>--%>
                                                                <td>
                                                                    <asp:Label ID="Label006" runat="server" Text='<%# Eval("EMAIL_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label007" runat="server" Text='<%# Eval("MOBILE_NO")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label008" runat="server" Text='<%# Eval("WEBSITE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label009" runat="server" Text='<%# Eval("DATE_OF_VISIT")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label010" runat="server" Text='<%# Eval("CLASS")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label011" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT_VISITED")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label012" runat="server" Text='<%# Eval("NAME_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label013" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="tab-pane fade" id="tab_9">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcademicYear9" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" ControlToValidate="ddlAcademicYear9"
                                                            ValidationGroup="TP9" ErrorMessage="Please select Academic Year " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartment9" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator72" runat="server" ControlToValidate="ddlAcademicYear9"
                                                            ValidationGroup="TP9" ErrorMessage="Please select Department " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Name Of Company </label>
                                                        </div>
                                                        <asp:TextBox ID="txtNameCompany9" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator73" runat="server"
                                                            ControlToValidate="txtNameCompany9" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Name Of Company." SetFocusOnError="true" ValidationGroup="TP9">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Location </label>
                                                        </div>
                                                        <asp:TextBox ID="txtLocation9" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server"
                                                            ControlToValidate="txtLocation9" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Location." SetFocusOnError="true" ValidationGroup="TP9">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date Of Interaction  </label>
                                                        </div>


                                                        <div class="input-group date">
                                                            <div class="input-group-addon" id="Image6">
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtDateofInteraction9" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image6" PopupPosition="BottomLeft"
                                                                TargetControlID="txtDateofInteraction9">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDateofInteraction9">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                        </div>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Mode </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlMode9" runat="server" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Online</asp:ListItem>
                                                            <asp:ListItem Value="2">Offline</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" ControlToValidate="ddlMode9"
                                                            ValidationGroup="TP9" ErrorMessage="Please select Mode " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>No. of Student Benefitted</label>
                                                        </div>
                                                        <asp:TextBox ID="txtNoofStudentBenefitted" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server"
                                                            ControlToValidate="txtNoofStudentBenefitted" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter No. of Student Benefitted." SetFocusOnError="true" ValidationGroup="TP9">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Staff Coordinator</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStaffCoordinator9" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server"
                                                            ControlToValidate="txtStaffCoordinator9" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Staff Coordinator." SetFocusOnError="true" ValidationGroup="TP9">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Claim</label>
                                                        </div>
                                                        <asp:TextBox ID="txtClaim9" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server"
                                                            ControlToValidate="txtClaim9" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Enter Claim." SetFocusOnError="true" ValidationGroup="TP9">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="Active9" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="Active9"></label>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnsubmittab9" runat="server" CssClass="btn btn-outline-primary" Text="Submit" OnClick="btnsubmittab9_Click" OnClientClick="return validate9()" ValidationGroup="TP9"></asp:LinkButton>
                                                <%--<asp:Button ID="btnsubmittab9" runat="server" Text="Submit" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="TP9" OnClick="btnsubmittab9_Click" />--%>
                                                <asp:Button ID="btncanceltab9" runat="server" Text="Cancel" TabIndex="13" CssClass="btn btn-warning" OnClick="btncanceltab9_Click" />
                                                <asp:Button ID="btnShow9" runat="server" Text="Show" TabIndex="13" CssClass="btn btn-primary" OnClick="btnShow9_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="TP9" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-12">
                                                    <asp:ListView ID="ListView9" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblPlacementTalk">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Academic Year </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Name Of Company
                                                                        </th>
                                                                        <th>Location
                                                                        </th>
                                                                        <th>Date of Interaction</th>
                                                                        <th>Mode</th>
                                                                        <th>No. of Students Visited</th>
                                                                        <th>Staff Coordinator</th>
                                                                        <th>Claim</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>

                                                                <tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/edit.png" />--%>

                                                                    <asp:ImageButton ID="btnFDREdittab9" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="9" OnClick="btnFDREdittab9_Click" />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label201" runat="server" Text='<%# Eval("ACADEMIC_YEAR_ID")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label202" runat="server" Text='<%# Eval("DEPTNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label203" runat="server" Text='<%# Eval("NAME_OF_COMPANY")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label204" runat="server" Text='<%# Eval("LOCATIONOF_COMPANY")%>'></asp:Label></td>
                                                                <%--<td><asp:Label ID="Label205" runat="server" Text='<%# Eval("ADDRESS")%>'></asp:Label></td>--%>
                                                                <td>
                                                                    <asp:Label ID="Label206" runat="server" Text='<%# Eval("DATE_OF_INTERACTION")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label207" runat="server" Text='<%# Eval("MODE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label208" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT_BENEFITTED")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label209" runat="server" Text='<%# Eval("NAME_STAFF_COORDINATOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label210" runat="server" Text='<%# Eval("CLAIM")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label211" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
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
    </div>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>


    <%--===== Data Table Script used in Internship=====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblInternship').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblInternship').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblInternship').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblInternship').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblInternship').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblInternship').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblInternship').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblInternshipy').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>

      <%--===== Data Table Script used in Industry Association=====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblIndustryAssociation').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblIndustryAssociation').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblIndustryAssociation').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblIndustryAssociation').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblIndustryAssociation').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblIndustryAssociation').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblIndustryAssociation').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblIndustryAssociation').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>


      <%--===== Data Table Script used in Expert Talk =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblExpertTalk').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblExpertTalk').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblExpertTalk').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblExpertTalk').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblExpertTalk').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblExpertTalk').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblExpertTalk').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblExpertTalk').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>


     <%--===== Data Table Script used in Alumni Interaction =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblAlumniInteraction').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblAlumniInteraction').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblAlumniInteraction').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblAlumniInteraction').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblAlumniInteraction').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblAlumniInteraction').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblAlumniInteraction').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblAlumniInteraction').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>


         <%--===== Data Table Script used in Contest Based Hiring =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblContestBasedHiring').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblContestBasedHiring').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblContestBasedHiring').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblContestBasedHiring').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblContestBasedHiring').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblContestBasedHiring').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblContestBasedHiring').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblContestBasedHiring').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>

       <%--===== Data Table Script used in Foregin Language =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblForeginLanguage').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblForeginLanguage').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblForeginLanguage').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblForeginLanguage').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblForeginLanguage').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblForeginLanguage').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblForeginLanguage').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblForeginLanguage').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>


        <%--===== Data Table Script used in Industry Visits =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblIndustryVisits').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblIndustryVisits').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblIndustryVisits').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblIndustryVisits').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblIndustryVisits').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblIndustryVisits').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblIndustryVisits').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblIndustryVisits').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>


        <%--===== Data Table Script used in Placement Talk =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblPlacementTalk').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblPlacementTalk').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                            {
                                extend: 'copyHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblPlacementTalk').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },
                            {
                                extend: 'excelHtml5',
                                exportOptions: {
                                    columns: function (idx, data, node) {
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblPlacementTalk').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("select").length > 0) {
                                                nodereturn = "";
                                                $(node).find("select").each(function () {
                                                    var thisOption = $(this).find("option:selected").text();
                                                    if (thisOption !== "Please Select") {
                                                        nodereturn += thisOption;
                                                    }
                                                });
                                            }
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
                                            }
                                            else {
                                                nodereturn = data;
                                            }
                                            return nodereturn;
                                        },
                                    },
                                }
                            },

                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblPlacementTalk').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblPlacementTalk').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                               {
                                   extend: 'copyHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblPlacementTalk').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },
                               {
                                   extend: 'excelHtml5',
                                   exportOptions: {
                                       columns: function (idx, data, node) {
                                           var arr = [0];
                                           if (arr.indexOf(idx) !== -1) {
                                               return false;
                                           } else {
                                               return $('#tblPlacementTalk').DataTable().column(idx).visible();
                                           }
                                       },
                                       format: {
                                           body: function (data, column, row, node) {
                                               var nodereturn;
                                               if ($(node).find("input:text").length > 0) {
                                                   nodereturn = "";
                                                   nodereturn += $(node).find("input:text").eq(0).val();
                                               }
                                               else if ($(node).find("input:checkbox").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("input:checkbox").each(function () {
                                                       if ($(this).is(':checked')) {
                                                           nodereturn += "On";
                                                       } else {
                                                           nodereturn += "Off";
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("a").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("a").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                   nodereturn = "";
                                                   $(node).find("span").each(function () {
                                                       nodereturn += $(this).text();
                                                   });
                                               }
                                               else if ($(node).find("select").length > 0) {
                                                   nodereturn = "";
                                                   $(node).find("select").each(function () {
                                                       var thisOption = $(this).find("option:selected").text();
                                                       if (thisOption !== "Please Select") {
                                                           nodereturn += thisOption;
                                                       }
                                                   });
                                               }
                                               else if ($(node).find("img").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else if ($(node).find("input:hidden").length > 0) {
                                                   nodereturn = "";
                                               }
                                               else {
                                                   nodereturn = data;
                                               }
                                               return nodereturn;
                                           },
                                       },
                                   }
                               },

                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>

    <script type="text/javascript" language="javascript">


        //TAB1
        function validate1() {

            $('#hfdActive').val($('#Active1').prop('checked'));
        }

        function SetStat(val) {
            $('#Active1').prop('checked', val);
        }

        //TAB2

        function validate2() {
            $('#hfdActive2').val($('#Active2').prop('checked'));
        }

        function SetStatActive2(val) {
            $('#Active2').prop('checked', val);
        }

        //tab3

        function validate3() {
            $('#hfdActive3').val($('#Active3').prop('checked'));
        }
        function SetStatActive3(val) {
            $('#Active3').prop('checked', val);
        }

        //tab4

        function validate4() {
            $('#hfdActive4').val($('#Active4').prop('checked'));
        }
        function SetStatActive4(val) {
            $('#Active4').prop('checked', val);
        }
        //tab5


        function validate5() {
            $('#hfdActive5').val($('#Active5').prop('checked'));
        }
        function SetStatActive5(val) {
            $('#Active5').prop('checked', val);
        }

        //tab6

        function validate6() {
            $('#hfdActive6').val($('#Active6').prop('checked'));
        }
        function SetStatActive6(val) {
            $('#Active6').prop('checked', val);
        }

        //tab7

        function validate7() {
            $('#hfdActive7').val($('#Active7').prop('checked'));
        }
        function SetStatActive7(val) {
            $('#Active7').prop('checked', val);
        }

        //tab8

        function validate8() {
            $('#hfdActive8').val($('#Active8').prop('checked'));
        }
        function SetStatActive8(val) {
            $('#Active8').prop('checked', val);
        }

        //tab9

        function validate9() {
            $('#hfdActive9').val($('#Active9').prop('checked'));
        }
        function SetStatActive9(val) {
            $('#Active9').prop('checked', val);
        }





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
    </script>

    <script>
        function TabShow(tabid) {
            var tabName = tabid;
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>

</asp:Content>

