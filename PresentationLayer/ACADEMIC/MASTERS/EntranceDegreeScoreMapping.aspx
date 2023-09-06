<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntranceDegreeScoreMapping.aspx.cs" Inherits="ACADEMIC_MASTERS_EntranceDegreeScoreMapping"
    MasterPageFile="~/SiteMasterPage.master" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updMappedData"
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

    <style>
        .checkbox-list-box {
            height:auto;
        }
    </style>

    <asp:UpdatePanel ID="updMappedData" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Entrance Exam Score Validation</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem>Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="submit" InitialValue="0"
                                            ErrorMessage="Please Select Degree."></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="2">
                                            <asp:ListItem>Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ValidationGroup="submit" InitialValue="0"
                                            ErrorMessage="Please Select Exam Type."></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Minimum Score/Percentage</label>
                                        </div>
                                        <asp:TextBox ID="txtMinScore" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="3" MaxLength="5">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMinScore"
                                            Display="None" ValidationGroup="submit"
                                            ErrorMessage="Please Enter Minimum Score."></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMinScore"
                                            ValidChars="0123456789." Enabled="True" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Category</label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-box">
                                            <asp:CheckBoxList ID="chklstCategory" runat="server" CssClass="checkboxlist checkbox-list-style" AppendDataBoundItems="true" TabIndex="4" RepeatDirection="horizontal" RepeatColumns="3" TextAlign="Right" OnSelectedIndexChanged="chklstCategory_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="10" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="11" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnllst" runat="server">
                                    <asp:ListView ID="lvMappingList" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Entrance Exam Score Validation List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divMappingList">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Edit </th>
                                                        <th>Degree </th>
                                                        <th>Exam </th>
                                                        <th>Category </th>
                                                        <th>Minimum Score/Percentage </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("ENT_NO")%>' ImageUrl="~/images/edit1.gif" TabIndex="12" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td><%# Eval("DEGREENAME")%></td>
                                                <td><%# Eval("QUALIEXMNAME")%></td>
                                                <td><%# Eval("CATEGORY")%></td>
                                                <td><%# Eval("MIN_SCORE") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                           
                        </div>

                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divMappingList').DataTable({

            });
        }

    </script>--%>
    <script>
        function NumericValue(ob) {
            var invalidChars = /([^0-9.])/gi
            if (invalidChars.test(ob.value)) {
                ob.value = ob.value.replace(invalidChars, "");
            }
        }
    </script>
</asp:Content>
