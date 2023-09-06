<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecieptTypeDefinition.ascx.cs"
    Inherits="Fees_RecieptTypeDefinition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%--<script src="../../Content/jquery.js" type="text/javascript"></script>

<script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

<%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function() {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
</script>--%>

<div class="row">
    <div class="col-md-12 col-sm-12 col-12">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Receipt Type</h3>
            </div>

            <div class="box-body">
           
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                              <%--  <sup>* </sup>
                                <label>Receipt Code</label>--%>
                            </div>
                            <asp:TextBox ID="txtCode" runat="server" TabIndex="1" MaxLength="3"
                                CssClass="form-control" ToolTip="Please Enter Receipt Code" Visible="false" />
                            <asp:RequiredFieldValidator ID="valCode" runat="server" ControlToValidate="txtCode"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Receipt Code." />
                            <ajaxToolKit:FilteredTextBoxExtender ID="fteCode" runat="server" FilterMode="InvalidChars"
                                FilterType="Custom" InvalidChars="`~!@#$%^&*()_+-=|\{}[]:;'<>?,./" TargetControlID="txtCode">
                            </ajaxToolKit:FilteredTextBoxExtender>
                        </div>
                    <div class="col-12">
                    <div class="row">
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Receipt Title</label>
                            </div>
                            <asp:TextBox ID="txtTitle" runat="server" TabIndex="2" MaxLength="50"
                                CssClass="form-control" ToolTip="Please Enter Receipt Title" />
                            <asp:RequiredFieldValidator ID="valTitile" runat="server" ControlToValidate="txtTitle"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Receipt Title." />
                            <ajaxToolKit:FilteredTextBoxExtender ID="fteTitle" runat="server" FilterMode="InvalidChars"
                                FilterType="Custom" InvalidChars="`~!@#$%^&*()_+-=|\{}[]:;'<>?,./" TargetControlID="txtTitle">
                            </ajaxToolKit:FilteredTextBoxExtender>
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Receipt Code</label>
                            </div>
                            <asp:DropDownList ID="ddlReceiptCode" runat="server" TabIndex="3" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                ToolTip="Please Select Receipt">
                                <asp:ListItem Value="0">Select Receipt Code</asp:ListItem>  
                                
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReceiptCode"
                                InitialValue="0" ValidationGroup="Submit" Display="None"
                                ErrorMessage="Please select Receipt Code." />
                        </div>
                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>This Receipt belongs to</label>
                            </div>
                            <asp:DropDownList ID="ddlBelongsTo" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                ToolTip="Please Select Receipt Belongs To">
                                <asp:ListItem Text="Please Select" Value="0" />
                                <asp:ListItem Text="Academic" Value="A" />
                                <asp:ListItem Text="Hostel" Value="H" />
                                <asp:ListItem Text="Mess" Value="M" />
                                <asp:ListItem Text="Miscellaneous" Value="M" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="valBelongsTo" runat="server" ControlToValidate="ddlBelongsTo"
                                InitialValue="0" ValidationGroup="Submit" Display="None"
                                ErrorMessage="Please select module name from which this receipt type belongs." />
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label>Account No.</label>
                            </div>
                            <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control" TabIndex="3"
                                MaxLength="12" ToolTip="Please Enter Account Number" />
                            <asp:RequiredFieldValidator ID="valAccountNo" runat="server" ControlToValidate="txtAccountNo"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Account No." />
                            <ajaxToolKit:FilteredTextBoxExtender ID="fteAccountNo" runat="server" FilterMode="ValidChars"
                                FilterType="Numbers" TargetControlID="txtAccountNo">
                            </ajaxToolKit:FilteredTextBoxExtender>
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server" visible="false">
                            <div class="label-dynamic">
                                <label>Company</label>
                            </div>
                            <asp:DropDownList ID="ddlCompany" runat="server" TabIndex="4" />
                            <asp:CompareValidator ID="valCompany" runat="server" ControlToValidate="ddlCompany" CssClass="form-control" data-select2-enable="true"
                                Display="None" ErrorMessage="Please select company name." ValueToCompare="Please Select"
                                Type="String" Operator="NotEqual" ValidationGroup="Submit" />
                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2">
                            <div class="label-dynamic">
                                <label>Link with Accounts</label>
                            </div>
                            <asp:RadioButton ID="rdoYes" runat="server" Text="Yes" GroupName="Link" TabIndex="5" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoNo" runat="server" Text="No" GroupName="Link" Checked="true"
                                    TabIndex="6" />
                        </div>
                              <div class="form-group col-lg-3 col-md-6 col-12" id="Div3">

                                        <label>Is Tution/Admission/Institute Fees</label>

                                        <asp:CheckBox ID="chkStatus" runat="server"  ToolTip="Check status" TabIndex="6" />
                                    </div>
                    </div>
                </div>
        </div>

                <div class="col-12 btn-footer">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                        OnClick="btnSubmit_Click" TabIndex="7" CssClass="btn btn-primary" />
                    <asp:Button ID="btnReport" runat="server" TabIndex="8" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                        OnClick="btnCancel_Click" TabIndex="9" CssClass="btn btn-warning" />

                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ValidationGroup="Submit"
                        ShowMessageBox="true" ShowSummary="false" />
                </div>

                <div class="col-12">
                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                        <asp:Repeater ID="lvRecieptType" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>Receipt Type List</h5>
                                </div>
                                <thead class="bg-light-blue">
                                    <tr>
                                        <th>Edit
                                        </th>
                                        <th>Title
                                        </th>
                                        <th>Receipt Code
                                        </th>
                                    </tr>
                                    <thead>
                                <tbody>
                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                            CommandArgument='<%# Eval("RCPTTYPENO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                            OnClick="btnEdit_Click" TabIndex="10" />
                                    </td>
                                    
                                    <td>
                                        <%# Eval("RECIEPT_TITLE")%>
                                    </td>
                                    <td>
                                        <%# Eval("RC_NAME")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>

                <div id="divMsg" runat="server">
                </div>
            </div>

        </div>
    </div>
</div>
