<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RulesCreation.aspx.cs" Inherits="ACADEMIC_RulesCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        hr.style1 {
            border-top: 1px solid #8c8b8b;
        }
    </style>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upddetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>



    <asp:UpdatePanel ID="upddetails" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>
            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
            <div class="row">
                <div class="col-sm-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>PROMOTION RULES CREATION</b></h3>

                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <%-- <asp:Panel ID="pnlSemesterPattern" runat="server" Style="text-align: left;">--%>
                            <div class="col-sm-12">

                                <div class="col-md-3 form-group">
                                    <label><span style="color: red;">*</span>College /School Name :</label>
                                    <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" class="form-control" TabIndex="1" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlColg"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="rules">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 form-group">
                                    <label><span style="color: red;">*</span>Degree :</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" class="form-control" TabIndex="2" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="rules">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-3 form-group">
                                    <label><span style="color: red;">*</span>Branch :</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" class="form-control" TabIndex="3" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="rules">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 form-group">
                                    <label><span style="color: red;">*</span>Scheme :</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" class="form-control" TabIndex="4" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlScheme"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="rules">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 form-group">
                                    <label>Duration Year (Regular) :</label>
                                    <asp:TextBox ID="txtDurationRegular" runat="server" BorderStyle="Ridge" Enabled="false" TabIndex="5" class="form-control"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDurationRegular"
                                        Display="None" ErrorMessage="Please Enter Duration" ValidationGroup="rules">
                                    </asp:RequiredFieldValidator>--%>
                                    <ajaxToolKit:FilteredTextBoxExtender
                                        ID="txtNumbers_FilteredTextBoxExtender"
                                        runat="server"
                                        TargetControlID="txtDurationRegular"
                                        FilterType="Numbers">
                                    </ajaxToolKit:FilteredTextBoxExtender>

                                </div>

                                <div class="col-md-3 form-group">
                                    <label>Span Period :</label>
                                    <asp:TextBox ID="txtSpanPeriod" runat="server" BorderStyle="Ridge" Enabled="true" TabIndex="6" class="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSpanPeriod"
                                        Display="None" ErrorMessage="Please Enter Span Period" ValidationGroup="rules">
                                    </asp:RequiredFieldValidator>--%>
                                    <ajaxToolKit:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender2"
                                        runat="server"
                                        TargetControlID="txtSpanPeriod"
                                        FilterType="Numbers">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <br />
                            <div class="col-sm-12">
                                <div class="well">
                                    <div class="col-sm-12">
                                        <div class="col-md-3 form-group">
                                        </div>
                                        <div class="col-md-5 form-group">
                                            <label style="font-size: larger;">No. Of Backlogss :</label>

                                        </div>
                                        <div class="col-md-4 form-group">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                                <label>SEM1 - SEM2 :</label>
                                            </div>
                                            <div class="col-md-3 form-group">
                                                <asp:TextBox ID="txtSEM1_SEM2" runat="server" BorderStyle="Ridge" Enabled="true" TabIndex="7"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtender3"
                                                    runat="server"
                                                    TargetControlID="txtSEM1_SEM2"
                                                    FilterType="Numbers, Custom"
                                                    ValidChars="-">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-6 form-group">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                                <label>SEM2 - SEM3 :</label>
                                            </div>
                                            <div class="col-md-3 form-group">
                                                <asp:TextBox ID="txtSEM2_SEM3" runat="server" BorderStyle="Ridge" Enabled="true" TabIndex="8"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtender4"
                                                    runat="server"
                                                    TargetControlID="txtSEM2_SEM3"
                                                    FilterType="Numbers, Custom"
                                                    ValidChars="-">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label>>> No. Of backlogs including 1st and 2nd sem</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                                <label>SEM3 - SEM4 :</label>
                                            </div>
                                            <div class="col-md-3 form-group">
                                                <asp:TextBox ID="txtSEM3_SEM4" runat="server" BorderStyle="Ridge" Enabled="true" TabIndex="9"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtender5"
                                                    runat="server"
                                                    TargetControlID="txtSEM3_SEM4"
                                                    FilterType="Numbers, Custom"
                                                    ValidChars="-">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label>>> No. Of backlogs from 1st to 3rd sem</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                                <label>SEM4 - SEM5 :</label>
                                            </div>
                                            <div class="col-md-3 form-group">
                                                <asp:TextBox ID="txtSEM4_SEM5" runat="server" BorderStyle="Ridge" Enabled="true" TabIndex="10"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtender6"
                                                    runat="server"
                                                    TargetControlID="txtSEM4_SEM5"
                                                    FilterType="Numbers, Custom"
                                                    ValidChars="-">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label>>> No. Of backlogs from 1st to 4th sem</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                                <label>SEM5 - SEM6 :</label>
                                            </div>
                                            <div class="col-md-3 form-group">
                                                <asp:TextBox ID="txtSEM5_SEM6" runat="server" BorderStyle="Ridge" Enabled="true" TabIndex="11"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtender1"
                                                    runat="server"
                                                    TargetControlID="txtSEM5_SEM6"
                                                    FilterType="Numbers, Custom"
                                                    ValidChars="-">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label>>> No. Of backlogs from 1st to 4th sem</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                                <label>SEM6 - SEM7 :</label>
                                            </div>
                                            <div class="col-md-3 form-group">
                                                <asp:TextBox ID="txtSEM6_SEM7" runat="server" BorderStyle="Ridge" Enabled="true" TabIndex="12"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtender7"
                                                    runat="server"
                                                    TargetControlID="txtSEM6_SEM7"
                                                    FilterType="Numbers, Custom"
                                                    ValidChars="-">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label>>> No. Of backlogs from 1st to 6th sem</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                                <label>SEM7 - SEM8 :</label>
                                            </div>
                                            <div class="col-md-3 form-group">
                                                <asp:TextBox ID="txtSEM7_SEM8" runat="server" BorderStyle="Ridge" Enabled="true" TabIndex="13"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender
                                                    ID="FilteredTextBoxExtender8"
                                                    runat="server"
                                                    TargetControlID="txtSEM7_SEM8"
                                                    FilterType="Numbers, Custom"
                                                    ValidChars="-">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label>>> No. Of backlogs from 1st to 7th sem</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                                <label>Remarks :</label>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <asp:TextBox ID="txtRemark" runat="server" BorderStyle="Ridge" TextMode="MultiLine" Enabled="true" TabIndex="14"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 form-group">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                            </div>
                                            <div class="col-md-9 form-group">
                                                <div style="color: Red; font-weight: bold">
                                                    <label>Notes: Enter (-1) to Indicates that All Paper Carry, Enter (0) to Indicates that No Paper Carry</label>
                                                </div>
                                            </div>
                                            <%--<div class="col-md-3 form-group">
                                           
                                      </div>--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3 form-group">
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <asp:Button ID="btnSave" runat="server" TabIndex="15" Text="Save" ValidationGroup="rules" CssClass="btn btn-success"
                                                    ToolTip="SAVE" OnClick="btnSave_Click" />&nbsp;&nbsp;
                                      
                                    <asp:Button ID="btnClear" runat="server" TabIndex="16" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="rules"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:ListView ID="lvSemProRules" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4>Select Student</h4>
                                                <table class="table table-hover table-bordered table-responsive">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th align="left">Edit
                                                            </th>
                                                            <th align="left">Degree
                                                            </th>
                                                            <th align="left">Branch
                                                            </th>
                                                            <th align="left">Academic Pattern
                                                            </th>
                                                            <th align="left">Duration
                                                            </th>
                                                            <th align="left">Span Period
                                                            </th>
                                                            <th align="left">Backlog SEM1-SEM2
                                                            </th>
                                                            <th align="left">Backlog SEM2-SEM3
                                                            </th>
                                                            <th align="left">Backlog SEM3-SEM4
                                                            </th>
                                                            <th align="left">Backlog SEM4-SEM5
                                                            </th>
                                                            <th align="left">Backlog SEM5-SEM6
                                                            </th>
                                                            <th align="left">Backlog SEM6-SEM7
                                                            </th>
                                                            <th align="left">Backlog SEM7-SEM8
                                                            </th>
                                                            <th align="left">Remarks
                                                            </th>

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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                        CommandArgument='<%# Eval("RULE_ID")%>' AlternateText='<%# Eval("COLLEGE_ID")%>' ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("RULE_ID") %>' AlternateText='<%# Eval("COLLEGE_ID") %>' OnClick="btnDelete_Click"
                                                        ToolTip="Delete Record" OnClientClick="return confirm('Yor are deleting record permanently. \r\n Are you sure ?');" />--%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblregno" Text='<%# Eval("DEGREE")%>' ToolTip='<%# Eval("DEGREENO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblstudname" Text='<%# Eval("BRANCH")%>' ToolTip='<%# Eval("BRANCHNO")%>'></asp:Label>
                                                    <asp:HiddenField ID="hdnSchemeno" runat="server" Value='<%#Eval("SCHEMENO") %>' />
                                                </td>

                                                <td>
                                                    <%# Eval("ACADEMIC_PATTERN") %>
                                                </td>
                                                <td>
                                                    <%# Eval("DURATION_REGULAR") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SPAN_PERIOD") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BACKLOG_SEM1_SEM2") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BACKLOG_SEM2_SEM3") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BACKLOG_SEM3_SEM4") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BACKLOG_SEM4_SEM5") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BACKLOG_SEM5_SEM6") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BACKLOG_SEM6_SEM7") %>
                                                </td>
                                                <td>
                                                    <%# Eval("BACKLOG_SEM7_SEM8") %>
                                                </td>
                                                <td>
                                                    <%# Eval("REMARKS") %>
                                                </td>

                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>

                            </div>
                            <%-- </asp:Panel>--%>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>

            <%--<asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnClear" />
            <asp:PostBackTrigger ControlID="btnYearSave" />
            <asp:PostBackTrigger ControlID="btnYearCancel" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>
    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>


    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });

    </script>--%>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

</asp:Content>

