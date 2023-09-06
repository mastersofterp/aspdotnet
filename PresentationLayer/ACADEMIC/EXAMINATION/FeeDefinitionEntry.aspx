<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FeeDefinitionEntry.aspx.cs" Inherits="ACADEMIC_EXAMINATION_FeeDefinitionEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFee"
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

    <asp:UpdatePanel ID="updFee" runat="server">
        <ContentTemplate>
            <div id="divMsg" runat="server">
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>FEE DEFINITION ENTRY</b></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-5 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Regular Fee Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtRegFeeAmt" runat="server" MaxLength="9" onkeyup="return validateNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRegFeeAmt" runat="server" ControlToValidate="txtRegFeeAmt"
                                            Display="None" ErrorMessage="Please Enter Regular Fee Amount" ValidationGroup="" />
                                    </div>
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <asp:RadioButtonList ID="rdlist" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdlist_SelectedIndexChanged">
                                            <asp:ListItem Value="0"> Regular Exam Fee &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1"> Backlog Exam Fee &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2"> Redo Exam Fee</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <br />
                                        <asp:CheckBox ID="chkCourse" runat="server" Text="Coursewise" AutoPostBack="true" OnCheckedChanged="chkCourse_CheckedChanged" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddldegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-12 col-lg-4">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:ListView ID="lvFee" Visible="true" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>Fee Amount</h5>
                                                        </div>
                                                        <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="mytable1">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Course
                                                                        </th>

                                                                        <th>Fee Amount
                                                                        </th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>

                                                            <asp:Label ID="lblSubType" runat="server" Text='<%# Eval("SUBNAME")%>' ToolTip=' <%# Eval("SUBID")%>'></asp:Label>
                                                        </td>


                                                        <td>
                                                            <asp:TextBox ID="txtFee" runat="server" MaxLength="6" Text='<%# Eval("FEE")%>' placeholder="Enter Fee" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="rfvMaxMark" runat="server" ControlToValidate="txtFee"
                                                                Display="None" ErrorMessage="Please Enter  Mark" ValidationGroup="submit"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtFee">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>

                                        <div id="fees" runat="server" visible="true">
                                            <asp:ListView ID="lvfee2" Visible="true" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>Fee Amount</h5>
                                                        </div>

                                                        <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="mytable">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Semester
                                                                        </th>
                                                                        <th>Fee Amount
                                                                        </th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>

                                                        <td>
                                                            <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNO")%>'> </asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:TextBox ID="txtFee" runat="server" MaxLength="6" Text='<%# Eval("FEE")%>' placeholder="Enter Fee" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="rfvMaxMark" runat="server" ControlToValidate="txtFee"
                                                                Display="None" ErrorMessage="Please Enter  Mark" ValidationGroup="submit"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtMaxMarks" runat="server" ValidChars=".0123456789"
                                                                TargetControlID="txtFee">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnShow_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit" Visible="false" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" TabIndex="22"
                                    Text="Report" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed.");
                return false;
            }
            else
                return true;
        }



    </script>


</asp:Content>

