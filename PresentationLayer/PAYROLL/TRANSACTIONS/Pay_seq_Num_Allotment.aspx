<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_seq_Num_Allotment.aspx.cs" Inherits="PayRoll_Pay_seq_Num_Allotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SEQUENCE NUMBER ALLOTMENT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Staff</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select College"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <%--<label>Staff</label>--%>
                                                <label>Scheme/Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select Staff"
                                                OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Please Select Scheme/Staff" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Employee Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged"
                                                TabIndex="45">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEmpType" ValidationGroup="emp"
                                                ErrorMessage="Please select Employee Type" SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>


                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddldept" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select Department"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldept"
                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Order By</label>
                                            </div>
                                            <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Select Order By"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">IDNO</asp:ListItem>
                                                <asp:ListItem Value="2">SEQUENCE NO</asp:ListItem>
                                                <asp:ListItem Value="3">DOJ</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlorderby"
                                                Display="None" ErrorMessage="Please Select Order By" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlSeqNum" runat="server">
                                    <asp:ListView ID="lvSeqNum" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees For Sequence Number Allotment" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Sequence Number Allotment</h5>
                                            </div>
                                            <table class="table table-striped table-bordered" style="width: 100%;">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>ID No.
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                        <th>Designation
                                                        </th>
                                                        <th>Seq. No
                                                        </th>
                                                    </tr>
                                                    <thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("Idno")%>
                                                </td>
                                                <td>
                                                    <%#Eval("EMPNAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("SUBDESIG")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSqeNo" runat="server" MaxLength="3" onkeyup="return ValidateNumeric(this);"
                                                        Text='<%#Eval("SEQ_NO")%>' ToolTip='<%#Eval("Idno")%>' TabIndex="5" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="rfvtxtSqeNo" runat="server" ControlToValidate="txtSqeNo"
                                                        Display="None" ErrorMessage="Please Enter Seq.No" ValidationGroup="payroll" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="rvtxtSqeNo" runat="server" ControlToValidate="txtSqeNo" MinimumValue="0"
                                                        MaximumValue="999" Display="None" ValidationGroup="payroll" ErrorMessage="Please Enter sequence number between 0 to 999"
                                                        Type="Integer" SetFocusOnError="true">
                                                    </asp:RangeValidator>
                                                    <asp:CompareValidator ID="cvtxtSqeNo" runat="server" ControlToValidate="txtSqeNo"
                                                        Display="None" ErrorMessage="Please Enter integer Value" SetFocusOnError="true"
                                                        ValidationGroup="payroll" Operator="DataTypeCheck" Type="Integer">  
                                                    </asp:CompareValidator>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Click to Save" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="6"
                                    OnClick="btnSub_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Click to Reset" CausesValidation="false"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="7" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function ValidateNumeric(txt) {


            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                // alert(txt.value)
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters allowed");
                return false;
            }
            else {
                return true;
            }
        }
    </script>

</asp:Content>
