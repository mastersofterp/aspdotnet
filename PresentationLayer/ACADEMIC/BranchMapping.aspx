<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BranchMapping.aspx.cs" Inherits="Academic_BranchEntry" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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

    <script type="text/javascript" charset="utf-8">
        //$(document).ready(function () {
        //    $(".display").dataTable({
        //        "bJQueryUI": true,
        //        "sPaginationType": "full_numbers"
        //    });

        //});
        function ToUpper(ctrl) {

            var t = ctrl.value;

            ctrl.value = t.toUpperCase();

        }
        function ConfirmToDelete(me) {
            if (me != null) {
                var chk = confirm("Do you want to delete record......?");
                {
                    if (chk == true) {
                        return true;
                    }
                    else { return false; }
                }
            }

        }

        function ConfirmToEdit(me) {
            if (me != null) {
                var chk = confirm("Do you want to Update record......?");
                {
                    if (chk == true) {
                        return true;
                    }
                    else { return false; }
                }
            }

        }

    </script>

    <asp:UpdatePanel ID="updGradeEntry" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Programme/Branch Mapping</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeName" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Report" ToolTip="Please Select Institute" AutoPostBack="true" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcolg" runat="server" ControlToValidate="ddlCollegeName"
                                            Display="None" ErrorMessage="Please Select Institute " ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollegeName"
                                            Display="None" ErrorMessage="Please Select Institute " ValidationGroup="OptReport"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegreeName" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlDegreeName_SelectedIndexChanged" ValidationGroup="Report" ToolTip="Please Select Degree">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegreeName" runat="server" ControlToValidate="ddlDegreeName"
                                            Display="None" ErrorMessage="Please Select Degree " ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Branch" ToolTip="Department">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Department" ValidationGroup="Branch"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/Branch Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranchName" runat="server" TabIndex="4" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="Branch" ToolTip=" Programme/Branch Name">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLongName" runat="server" ControlToValidate="ddlBranchName"
                                            Display="None" ErrorMessage="Please Select Programme/Branch Name" ValidationGroup="Branch"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:CheckBox ID="chkEng" Text="" runat="server" TabIndex="5"/> Check if Degree & Programme Name is Same
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Duration</label>
                                        </div>
                                        <asp:TextBox ID="txtDuration" runat="server" TabIndex="6" ToolTip="Duration" placeholder="Please Enter Duration" CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="rfvDuration" runat="server" ControlToValidate="txtDuration"
                                            Display="None" ErrorMessage="Please Enter Duration" ValidationGroup="Branch"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                                        <asp:CompareValidator ID="cvDuration" runat="server" ErrorMessage="Please Enter Numeric Value for Duration"
                                            ControlToValidate="txtDuration" Display="None" SetFocusOnError="true" Type="Integer"
                                            Operator="DataTypeCheck" ValidationGroup="Branch" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme Short Name</label>
                                        </div>
                                        <asp:TextBox ID="txtCode" runat="server" TabIndex="7" MaxLength="16" CssClass="form-control"
                                            ToolTip="Please Enter Branch Short Name" onkeyup="ToUpper(this)" ValidationGroup="Branch" placeholder="Please Enter Programme/Branch Short Name" />
                                        <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode"
                                            Display="None" ErrorMessage="Please Enter Programme/Branch Short Name" ValidationGroup="Branch"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCode"
                                            FilterType="Custom,LowerCaseLetters,UpperCaseLetters,Numbers" ValidChars="-&.()/ ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme Code</label>
                                        </div>
                                        <asp:TextBox ID="txtBrCode" runat="server" TabIndex="8" MaxLength="16" CssClass="form-control"
                                            ToolTip="Please Enter Programme/Branch Code" onkeyup="ToUpper(this)" placeholder="Please Enter Programme/Branch Code" AutoCompleteType="None" />
                                        <%--<asp:RequiredFieldValidator ID="rfvBrCode" runat="server" ControlToValidate="txtBrCode"
                                            Display="None" ErrorMessage="Please Enter Branch code" ValidationGroup="Branch"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;--%>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtBrCode"
                                            FilterType="Custom,LowerCaseLetters,UpperCaseLetters,Numbers" ValidChars="-/">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Code</label>
                                        </div>
                                        <asp:TextBox ID="txtSchoolInstituteCode" runat="server" TabIndex="9" MaxLength="16" CssClass="form-control"
                                            ToolTip="Please Enter School/Institute Code" onkeyup="ToUpper(this)" placeholder="Please Enter School/Institute Code" AutoCompleteType="None" />
                                        <asp:RequiredFieldValidator ID="rfvSchoolInstituteCode" runat="server" ControlToValidate="txtSchoolInstituteCode"
                                            Display="None" ErrorMessage="Please Enter School/Institute Code" ValidationGroup="Branch"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeSchoolInstituteCode" runat="server" TargetControlID="txtSchoolInstituteCode"
                                            FilterType="Custom,LowerCaseLetters,UpperCaseLetters,Numbers" ValidChars="-/">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstituteType" runat="server" TabIndex="10" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select School/Institute Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">AICTE</asp:ListItem>
                                            <asp:ListItem Value="2">NON AICTE</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSchoolInstituteType" runat="server" ControlToValidate="ddlSchoolInstituteType"
                                            Display="None" ErrorMessage="Please Select School/Institute Type" ValidationGroup="Branch"
                                            SetFocusOnError="True" InitialValue="0" Width="200px"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" TabIndex="11" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Programme Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Programme Type" ValidationGroup="Branch"
                                            SetFocusOnError="True" InitialValue="0" Width="200px"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-6">
                                        <div class="sub-heading">
                                            <h5>Intake Capacity</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-4 col-6">
                                        <div class="label-dynamic">
                                            <label>I-Intake</label>
                                        </div>
                                        <asp:TextBox ID="txtIntake1" runat="server" TabIndex="12" CssClass="form-control" ToolTip="I Intake" MaxLength="3" placeholder="I-Intake" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteStateRank" runat="server" FilterType="Numbers"
                                            TargetControlID="txtIntake1">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-4 col-6">
                                        <div class="label-dynamic">
                                            <label>II-Intake</label>
                                        </div>
                                        <asp:TextBox ID="txtIntake2" runat="server" TabIndex="13" CssClass="form-control" ToolTip="II Intake" MaxLength="3" placeholder="II-Intake" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                            TargetControlID="txtIntake2">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-4 col-6">
                                        <div class="label-dynamic">
                                            <label>III-Intake</label>
                                        </div>
                                        <asp:TextBox ID="txtIntake3" runat="server" TabIndex="14" CssClass="form-control" ToolTip="III Intake" MaxLength="3" placeholder="III-Intake" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                            TargetControlID="txtIntake3">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-4 col-6">
                                        <div class="label-dynamic">
                                            <label>IV-Intake</label>
                                        </div>
                                        <asp:TextBox ID="txtIntake4" runat="server" TabIndex="15" CssClass="form-control" ToolTip="IV Intake" MaxLength="3" placeholder="IV-Intake" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                            TargetControlID="txtIntake4">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-2 col-md-4 col-6">
                                        <div class="label-dynamic">
                                            <label>V-Intake</label>
                                        </div>
                                        <asp:TextBox ID="txtIntake5" runat="server" ToolTip="V Intake" TabIndex="16" CssClass="form-control" MaxLength="3" placeholder="V-Intake" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                            TargetControlID="txtIntake5">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Branch"
                                    CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="17" />
                                <asp:Button ID="btnReport" runat="server" TabIndex="18" Text="Report" OnClick="btnReport_Click"
                                    CssClass="btn btn-info" ValidationGroup="OptReport" ToolTip="Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="19" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Branch"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="OptReport" />
                            </div>
                                
                            <div class="col-md-12 table-table-responsive">
                                <%-- <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="400px">--%>
                                <asp:ListView ID="lvBranch" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Programme/Branch Mapping List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divbranchlist">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center;">Delete </th>
                                                    <th style="text-align: center;">Edit </th>
                                                    <th>School/Institute Name </th>
                                                    <th>Degree Name </th>
                                                    <th>Department Name </th>
                                                    <th>Programme/Branch Name </th>
                                                    <%--<th>Short Name </th>--%>
                                                    <th>School/Institute Code </th>
                                                    <th>Programme/Branch Code </th>
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
                                                <asp:ImageButton ID="btnDel" runat="server" AlternateText="Delete Record" CommandArgument='<%# Eval("CDBNO") %>' ImageUrl="~/IMAGES/delete.gif" OnClick="btnDel_Click" OnClientClick="return ConfirmToDelete(this);" TabIndex="6" ToolTip="Delete" />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:ImageButton ID="btnedit" runat="server" AlternateText="Delete Record" CommandArgument='<%# Eval("CDBNO") %>' ImageUrl="~/IMAGES/edit.png" OnClick="btnedit_Click" TabIndex="6" ToolTip="Edit" />
                                            </td>
                                            <td><%# Eval("COLLEGE_NAME")%></td>
                                            <td><%# Eval("DEGREENAME")%></td>
                                            <td><%# Eval("DEPTNAME")%></td>
                                            <td><%# Eval("LONGNAME") %></td>
                                            <%--<td><%# Eval("CODE") %></td>--%>
                                            <td><%# Eval("SCHOOL_COLLEGE_CODE") %></td>
                                            <td><%# Eval("BRANCH_CODE") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <%--      </asp:Panel>--%>
                            </div>
                            
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divbranchlist').DataTable({

            });
        }

    </script>--%>
</asp:Content>

