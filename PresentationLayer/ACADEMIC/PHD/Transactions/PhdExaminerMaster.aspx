<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PhdExaminerMaster.aspx.cs" MaintainScrollPositionOnPostback="false" Inherits="Academic_PhdExaminerMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" charset="utf-8">
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
    </script>--%>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upd1"
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
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Examiner Details</h3>
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="updPS" runat="server">
                                <asp:Label ForeColor="Red" Font-Bold="true" ID="lblUserMsg" runat="server"></asp:Label>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden>
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Internal/External </label>
                                            </div>
                                            <asp:RadioButton ID="rbInternal" runat="server" Text="Internal" GroupName="IntExt" AutoPostBack="true"
                                                TabIndex="6" OnCheckedChanged="rbInternal_CheckedChanged" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbExternal" runat="server" Text="External" AutoPostBack="true" Checked="true"
                                                GroupName="IntExt" TabIndex="7" OnCheckedChanged="rbInternal_CheckedChanged" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Examiner Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlexaminer" runat="server" Visible="false" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="1" ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlexaminer_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ToolTip="Please Enter Examiner Name" ID="txtexaminer" MaxLength="50" runat="server" TabIndex="1" CssClass="form-control" Placeholder="Please Enter Examiner Name" />
                                            <asp:RequiredFieldValidator ID="valStaff" runat="server" ControlToValidate="txtexaminer"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Examiner Name"
                                                SetFocusOnError="true" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteExaminer" runat="server" TargetControlID="txtexaminer" InvalidChars="1234567890~`!@#$%^&*()_+={[}]|\:;'<,>.?|-&&quot;'" FilterMode="InvalidChars">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department Name </label>
                                            </div>
                                            <asp:TextBox ID="txtdepartment" runat="server" ToolTip="Please Enter Department Name" MaxLength="50" TabIndex="1" CssClass="form-control" Placeholder="Please Enter Department Name" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtdepartment"
                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="1234567890" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtdepartment"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtdepartment"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Department Name"
                                                SetFocusOnError="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Specialization </label>
                                            </div>
                                            <asp:TextBox ID="txtSpecialization" runat="server" ToolTip="Please Enter Specialization"
                                                TabIndex="1" CssClass="form-control" Placeholder="Please Enter Specialization" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtSpecialization" InvalidChars="1234567890~`!@#$%^&*()_+={[}]|\:;'<,>.?|-&&quot;'" FilterMode="InvalidChars">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Affiliation </label>
                                            </div>
                                            <asp:TextBox ID="txtAffiliation" runat="server" ToolTip="Please Enter Affiliation"
                                                TabIndex="1" CssClass="form-control" Placeholder="Please Enter Affiliation" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtAffiliation" InvalidChars="1234567890~`!@#$%^&*()_+={[}]|\:;'<,>.?|-&&quot;'" FilterMode="InvalidChars">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Mobile No.1 </label>
                                            </div>
                                            <asp:TextBox ID="txtMobile" runat="server" ToolTip="Please Enter Contact no." TabIndex="1" MaxLength="10" CssClass="form-control" Placeholder="Please Enter Mobile No.1" />
                                            <asp:RequiredFieldValidator ID="rfvContactNo" runat="server" ControlToValidate="txtMobile"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Mobile No.1"
                                                SetFocusOnError="true" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtContactNo" runat="server" FilterType="Numbers"
                                                TargetControlID="txtMobile">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Mobile No.2 </label>
                                            </div>
                                            <asp:TextBox ID="txtothermobile" runat="server" ToolTip="Please Enter Contact no." TabIndex="1" MaxLength="10" CssClass="form-control" Placeholder="Please Enter Mobile No.2" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                TargetControlID="txtothermobile">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Telephone No.1 </label>
                                            </div>
                                            <asp:TextBox ID="txtContact" runat="server" ToolTip="Please Enter Contact no." TabIndex="1" MaxLength="12" CssClass="form-control" Placeholder="Please Enter Telephone No.1" />
                                            <%--  <asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtContact"
                                        ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Telephone No.1"
                                        SetFocusOnError="true" />--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                TargetControlID="txtContact">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Telephone No.2 </label>
                                            </div>
                                            <asp:TextBox ID="txtothercontact" runat="server" ToolTip="Please Enter Contact no." TabIndex="1" MaxLength="12" CssClass="form-control" Placeholder="Please Enter Telephone No.2" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                TargetControlID="txtothercontact">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Email </label>
                                            </div>
                                            <asp:TextBox ID="txtEmail" runat="server" ToolTip="Please Enter Email Address" MaxLength="50" TabIndex="1" CssClass="form-control" Placeholder="Please Enter Email" />
                                            <asp:RegularExpressionValidator ID="rfvLocalEmail" runat="server" ControlToValidate="txtEmail"
                                                Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorMessage="Please Enter Valid EmailId" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Email."
                                                SetFocusOnError="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Bank Name </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlBankname" TabIndex="1" Width="100%" AppendDataBoundItems="true"
                                                ToolTip="Please Select Bank." CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Account No. </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtAccountNo" TabIndex="1" Width="100%" ToolTip="Please Enter Bank Account Number." CssClass="form-control" placeholder="Please Enter Account No." MaxLength="20"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtAccountNo"
                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtAccountNo"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Account Holder Name </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtholdername" TabIndex="1" Width="100%" ToolTip="Please Enter Account Holder Name" CssClass="form-control" Style="text-transform: uppercase"
                                                Placeholder="Please Enter Account Holder Name"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txtholdername"
                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="1234567890" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txtholdername"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>IFSC Code </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtIFSCCode" Width="100%" TabIndex="1" ToolTip="Please Enter IFSC Code." CssClass="form-control" Placeholder="Please Enter IFSC Code" MaxLength="11" Style="text-transform: uppercase"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteIFSCcode" runat="server" TargetControlID="txtIFSCCode"
                                                InvalidChars="~!@#$%^&*()_+|?></?" FilterMode="InvalidChars" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Branch Name </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtbranch" TabIndex="1" Width="100%" ToolTip="Please Enter Branch Name" CssClass="form-control"
                                                Placeholder="Please Enter Branch Name"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtbranch"
                                                FilterType="Custom" FilterMode="InvalidChars" InvalidChars="1234567890" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtbranch"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Address </label>
                                            </div>
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="1" ToolTip="Please Enter  address" MaxLength="100" TabIndex="1" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Address" SetFocusOnError="true" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtAddress"
                                                InvalidChars="~`!@#$%^*()_+=,.:;<>?'{}[]\|&&quot;'" FilterMode="InvalidChars" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divStatus" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Status </label>
                                            </div>
                                            <asp:CheckBox ID="chkActiveOrInactive" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" ToolTip="Select to Save" TabIndex="1" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnreport" runat="server" Text="Report" ToolTip="Select to Report" TabIndex="1" CssClass="btn btn-info" OnClick="btnReport_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ToolTip="Select to cancel" ShowSummary="false" ValidationGroup="Submit" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="AddPS" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnllist" runat="server">

                                    <div class="form-group col-sm-12 table-responsive">
                                        <asp:ListView ID="lvdetails" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="sub-heading">
                                                        <h5>List of Examiner Details</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="id1">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>SR No.
                                                                </th>
                                                                <th>Edit
                                                                </th>
                                                                <th hidden>External/Internal
                                                                </th>
                                                                <th>Examiner Name
                                                                </th>
                                                                <th>Mobile No 1.
                                                                </th>
                                                                <th hidden>Mobile No 2.
                                                                </th>
                                                                <th>Telephone No 1.
                                                                </th>
                                                                <th hidden>Telephone No 2.
                                                                </th>
                                                                <th>Email Id
                                                                </th>
                                                                <th id="ExaminerStatus">Status
                                                                </th>
                                                                <th hidden>Address
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
                                                <tr class="item">
                                                    <td>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("IDNO") %>' AlternateText="Edit Record" ToolTip="Select to Edit" OnClick="btnEdit_Click" TabIndex="1" />
                                                    </td>
                                                    <td hidden>
                                                        <%# Eval("INTERNAL")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MOBILE")%>
                                                    </td>
                                                    <td hidden>
                                                        <%# Eval("OTHERMOBILE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CONTACT_NO")%>
                                                    </td>
                                                    <td hidden>
                                                        <%# Eval("OTHERCONTACT_NO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EMAILID")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EXAMINER_STATUS")%>
                                                    </td>
                                                    <td hidden>
                                                        <%# Eval("ADDRESS")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div id="divMsg" runat="server">
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnreport" />
        </Triggers>
    </asp:UpdatePanel>

    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('#id1').dataTable({
                paging: true,
                searching: true,
                ordering: true,
                info: false,
                bDestroy: true
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('#id1').dataTable({
                paging: true,
                searching: true,
                ordering: true,
                info: false,
                bDestroy: true
            });
        });
    </script>--%>
</asp:Content>
