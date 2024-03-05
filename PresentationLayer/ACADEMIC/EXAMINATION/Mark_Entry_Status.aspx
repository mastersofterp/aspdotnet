<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Mark_Entry_Status.aspx.cs" Inherits="ACADEMIC_EXAMINATION_Mark_Entry_Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function convertToUpperCase(input) {
            input.value = input.value.toUpperCase();
        }


        function confirmDelete() {
            return confirm("Are you sure Do you want to delete");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMarkEntryStatus"
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

    <asp:UpdatePanel ID="updMarkEntryStatus" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfGradenew" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Mark Entry Status</h3>

                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Code Description</label>

                                        </div>
                                        <asp:TextBox ID="txtCodeDesc" runat="server" Placeholder="Enter Code Description" ToolTip="Enter Code Description" TabIndex="1" CssClass="form-control multi-select-demo" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCodeDesc" runat="server" ControlToValidate="txtCodeDesc"
                                            Display="None" ErrorMessage="Please Enter Code Description" ValidationGroup="show"
                                            SetFocusOnError="True" InitialValue="" Width="200px"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtCodeDesc"
                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'0123456789" FilterMode="InvalidChars" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Code Value</label>
                                        </div>
                                        <asp:TextBox ID="txtCodeValue" runat="server" Placeholder="Enter Code Value" ToolTip="Enter Code Value" TabIndex="2" CssClass="form-control multi-select-demo" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCodeValue" runat="server" ControlToValidate="txtCodeValue"
                                            Display="None" ErrorMessage="Please Enter Code Value" InitialValue="" SetFocusOnError="True"
                                            ValidationGroup="show" Width="200px"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCodeValue"
                                            ValidChars="0123456789" FilterMode="ValidChars" />

                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Short Name </label>

                                        </div>
                                        <asp:TextBox ID="txtShortname" runat="server" Placeholder="Enter Short Name" ToolTip="Enter Short Name" TabIndex="3" CssClass="form-control multi-select-demo" MaxLength="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvShortName" runat="server" ControlToValidate="txtShortname"
                                            Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="show"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtShortname"
                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'0123456789" FilterMode="InvalidChars" />

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Final Grade </label>
                                        </div>
                                        <asp:TextBox ID="txtFinalGrade" runat="server" Placeholder="Enter Final Grade" ToolTip="Enter Final Grade" TabIndex="4" CssClass="form-control multi-select-demo" MaxLength="3" onkeyup="convertToUpperCase(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFinalGrade" runat="server" ControlToValidate="txtFinalGrade"
                                            Display="None" ErrorMessage="Please Enter Final Grade" InitialValue="" SetFocusOnError="True"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtFinalGrade"
                                            InvalidChars="~`!@#$%^*()_=,./:;<>?'{}[]\|-&&quot;-'0123456789" FilterMode="InvalidChars" />

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Grade Point</label>
                                        </div>
                                        <asp:TextBox ID="txtGradePoint" runat="server" Placeholder="Enter Grade Point" ToolTip="Enter Grade Point" TabIndex="5" CssClass="form-control multi-select-demo" MaxLength="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGradePoint" runat="server" ControlToValidate="txtGradePoint"
                                            Display="None" ErrorMessage="Please Enter Grade Point" ValidationGroup="show"
                                            SetFocusOnError="True" InitialValue="" Width="200px"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtGradePoint"
                                            ValidChars="0123456789." FilterMode="ValidChars" />

                                    </div>

                                </div>


                            </div>
                        </div>
                        <div class="col-12 text-center box-footer">

                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="show" CssClass="btn btn-outline-success" Visible="true" ClientIDMode="Static" OnClick="btnSubmit_Click" TabIndex="6" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" TabIndex="7" />


                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                        <div class="col-12 ">

                            <asp:ListView ID="lvMarkEntryStatus" Visible="false" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable">
                                            <thead class="bg-light-blue">
                                                <tr>

                                                    <th>Action
                                                    </th>
                                                    <th>Code Description
                                                    </th>
                                                    <th>Code Value
                                                    </th>
                                                    <th>Short Name
                                                    </th>
                                                    <th>Final Grade
                                                    </th>
                                                    <th>Grade Point</th>

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

                                            <asp:ImageButton ID="btnEdit1" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png" AlternateText="Edit Record"
                                                ToolTip="Edit Record" OnClick="btnEdit1_Click" CommandArgument='<%# Eval("ID") %>' />
                                            &nbsp;&nbsp;&nbsp
                                                <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" ImageUrl="~/Images/Delete.png" AlternateText="Delete Record" ToolTip="Delete Record" CommandArgument='<%# Eval("ID") %>' OnClick="btnDelete_Click" OnClientClick="return confirmDelete();" />

                                        </td>

                                        <td>
                                            <asp:Label ID="lblCodeDesc" runat="server" Text='<%# Eval("CODE_DESC") %>'></asp:Label>

                                        </td>

                                        <td>
                                            <asp:Label ID="lblCodeValue" runat="server" Text='<%# Eval("CODE_VALUE") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblShortName" runat="server" Text='<%# Eval("SHORT_NAME") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblFGrade" runat="server" Text='<%# Eval("Final_Grade") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <asp:Label ID="lblGradePoint" runat="server" Text='<%# Eval("Grade_Point") %>'></asp:Label>

                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

