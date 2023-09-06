<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeesTransaction.aspx.cs" Inherits="ACADEMIC_EXAMINATION_FeesTransaction" MasterPageFile="~/SiteMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam"
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

    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FEE DEFINITION ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="save"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Fee Item Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlfeeitem" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvfeeitem" runat="server" ControlToValidate="ddlfeeitem"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="save"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Amount </label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <div class="fa fa-inr text-green"></div>
                                            </div>
                                            <asp:TextBox ID="txtAmount" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Enter Amount" onkeypress="CheckNumeric(event);" MaxLength="10">
                                            </asp:TextBox>
                                            <div class="input-group-addon">
                                                <span>.00</span>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvtxtamount" runat="server" ControlToValidate="txtAmount" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)"
                                                Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="save"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlsubjecttype" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsubtype" runat="server" ControlToValidate="ddlsubjecttype"
                                            Display="None" ErrorMessage="Please Select Subject Type" InitialValue="0" ValidationGroup="save"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnsubmit" runat="server" Font-Bold="False" OnClick="btnsubmit_Click"
                                    TabIndex="6" Text="Submit" ValidationGroup="save" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-info" TabIndex="7" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" TabIndex="8"
                                    Text="Cancel" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummaryShow" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="save" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlfeeItem" runat="server" ScrollBars="Auto" Visible="false">
                                    <asp:ListView ID="lvFeeItem" runat="server" Visible="false" ScrollBars="Auto">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Fee Definition List</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="ID5">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th id="Th1" runat="server">Session Name</th>
                                                            <th>Feel Item Name</th>
                                                            <th>Amount </th>
                                                            <th>Subject Type</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>

                                            <tr id="trCurRow">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%# Eval("FeeItemTransId")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" TabIndex="5" />
                                                </td>
                                                <td><%# Eval("SESSION_NAME")%></td>
                                                <td><%# Eval("FeeItemName")%></td>
                                                <td><%# Eval("AMOUNT")%></td>
                                                <td><%# Eval("SUBNAME")%></td>

                                            </tr>

                                        </ItemTemplate>

                                    </asp:ListView>
                                </asp:Panel>
                               
                                <div id="div2" runat="Server">
                                </div>
                            </div>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)

                        if (e.enable = true)
                            e.checked = true;
                        else
                            e.checked = false;
                }
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function CheckNumeric(e) {

            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }

    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>


