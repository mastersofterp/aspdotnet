<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Branch.ascx.cs" Inherits="Academic_Masters_Branch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%--<script src="../../Content/jquery.js" type="text/javascript"></script>

<script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

<script type="text/javascript" charset="utf-8">
        $(document).ready(function() {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
</script>--%>


<div id="divMsg" runat="server">
</div>

<div class="row">
    <div class="col-md-12 col-sm-12 col-12">
        <div class="box box-primary">
            <div id="div1" runat="server"></div>
            <div class="box-header with-border">
                <h3 class="box-title">BRANCH MASTER</h3>
            </div>

            <div class="box-body">
                <div class="col-12">
                    <div class="row">
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Degree </label>
                            </div>
                            <asp:DropDownList ID="ddlDegreeName" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="True"
                                ValidationGroup="Branch" ToolTip="Degree Name">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDegreeName" runat="server" ControlToValidate="ddlDegreeName"
                                Display="None" ErrorMessage="Please Select Degree " ValidationGroup="Branch"
                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Department </label>
                            </div>
                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" AppendDataBoundItems="True"
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
                                <label>Short Name </label>
                            </div>
                            <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="50"
                                ToolTip="Short Name" />
                            <asp:RequiredFieldValidator ID="rfvShortName" runat="server" ControlToValidate="txtShortName"
                                Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="Branch"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Long Name </label>
                            </div>
                            <asp:TextBox ID="txtLongName" runat="server" TabIndex="4" MaxLength="200" ToolTip="Long Name"
                                CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvLongName" runat="server" ControlToValidate="txtLongName"
                                Display="None" ErrorMessage="Please Enter Long Name" ValidationGroup="Branch"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Branch Name(In hindi) </label>
                            </div>
                            <asp:TextBox ID="txtBranchName" runat="server" TabIndex="5" MaxLength="100" CssClass="form-control"
                                ToolTip="Branch Name" />
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Duration </label>
                            </div>
                            <asp:TextBox ID="txtDuration" runat="server" TabIndex="6" CssClass="form-control" ToolTip="Duration" />
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
                                <label>Code </label>
                            </div>
                            <asp:TextBox ID="txtCode" runat="server" TabIndex="7" MaxLength="3" CssClass="form-control"
                                ToolTip="Code" />(Plz.Enter numeric code)
                                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode"
                                                Display="None" ErrorMessage="Please Enter Code" ValidationGroup="Branch" SetFocusOnError="True"
                                                Width="200px"></asp:RequiredFieldValidator>

                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                TargetControlID="txtCode">
                            </ajaxToolKit:FilteredTextBoxExtender>
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Branch /degree Belong From Section </label>
                            </div>
                            <asp:DropDownList ID="ddlEducation" runat="server" TabIndex="8" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                <asp:ListItem Value="UG">Under Graduate</asp:ListItem>
                                <asp:ListItem Value="PG">Post Graduate</asp:ListItem>
                                <asp:ListItem Value="PF">Professional Courses</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEducation"
                                Display="None" ErrorMessage="Please Enter UG/PG Certiria" ValidationGroup="Branch"
                                SetFocusOnError="True" InitialValue="-1" Width="200px"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <fieldset class="fieldset">
                    <legend class="legend">Intake Capacity</legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="intaketblpadd">I-Intake :
                            </td>
                            <td>
                                <asp:TextBox ID="txtIntake1" runat="server" TabIndex="9" ToolTip="I Intake" Width="50px" MaxLength="3" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="fteStateRank" runat="server" FilterType="Numbers"
                                    TargetControlID="txtIntake1">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="intaketblpadd">II-Intake :
                            </td>
                            <td>
                                <asp:TextBox ID="txtIntake2" runat="server" TabIndex="10" ToolTip="II Intake" Width="50px" MaxLength="3" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                    TargetControlID="txtIntake2">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="intaketblpadd">III-Intake :
                            </td>
                            <td>
                                <asp:TextBox ID="txtIntake3" runat="server" TabIndex="11" ToolTip="III Intake" Width="50px" MaxLength="3" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                    TargetControlID="txtIntake3">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="intaketblpadd">IV-Intake :
                            </td>
                            <td>
                                <asp:TextBox ID="txtIntake4" runat="server" TabIndex="12" ToolTip="IV Intake" Width="50px" MaxLength="3" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                    TargetControlID="txtIntake4">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="intaketblpadd">V-Intake :
                            </td>
                            <td>
                                <asp:TextBox ID="txtIntake5" runat="server" ToolTip="V Intake" TabIndex="13" Width="50px" MaxLength="3" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                    TargetControlID="txtIntake5">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </td>
                        </tr>
                    </table>
                </fieldset>

                <div class="col-12 btn-footer">
                    <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Branch"
                        CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="14" />

                    <asp:Button ID="btnReport" runat="server" TabIndex="16" Text="Report" OnClick="btnReport_Click"
                        CssClass="btn btn-info" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                        CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="15" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Branch"
                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                </div>

                <div class="col-12">
                    <asp:Repeater ID="lvBranch" runat="server">
                        <HeaderTemplate>
                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                <thead class="bg-light-blue">
                                    <tr>
                                        <th>Action
                                        </th>
                                        <th>Degree Name
                                        </th>
                                        <th>Short Name
                                        </th>
                                        <th>Long Name
                                        </th>
                                        <th>Belong from Section
                                        </th>
                                        <th>Total Intake
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("BRANCHNO") %>'
                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="17" />
                                </td>
                                <td>
                                    <%# Eval("DEGREENAME")%>
                                </td>
                                <td>
                                    <%# Eval("SHORTNAME")%>
                                </td>
                                <td>
                                    <%# Eval("LONGNAME") %>
                                </td>
                                <td>
                                    <%# Eval("UGPGPF") %>
                                </td>
                                <td>
                                    <%# Eval("TOTALINTAKE") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody></table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</div>

