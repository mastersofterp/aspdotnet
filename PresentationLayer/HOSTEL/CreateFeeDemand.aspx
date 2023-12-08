<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CreateFeeDemand.aspx.cs" Inherits="Hostel_CreateFeeDemand" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Create Fee Demand</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlFalse" runat="server" Visible="false">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Create Demand</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Demand For </label>
                                    </div>
                                    <asp:RadioButtonList ID="rdoDemand" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                        OnSelectedIndexChanged="rdoDemand_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Selected="True">Full Payment</asp:ListItem>
                                        <asp:ListItem Value="2">Half Payment</asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session </label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Hostel Session" ValidationGroup="submit"
                                    InitialValue="0" SetFocusOnError="true" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Demand of Receipt Type </label>
                                </div>
                                <asp:DropDownList ID="ddlReceiptType" AppendDataBoundItems="true" runat="server"
                                    CssClass="form-control" data-select2-enable="true" />
                                <asp:RequiredFieldValidator ID="valReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                    Display="None" ErrorMessage="Please Select Receipt Type." ValidationGroup="submit"
                                    InitialValue="0" SetFocusOnError="true" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Current Semester </label>
                                </div>
                                <asp:DropDownList ID="ddlsemester" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlsemester"
                                    Display="None" ErrorMessage="Please Select Filter By Semester" ValidationGroup="submit"
                                    InitialValue="0" SetFocusOnError="true" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Create Demand for Semester </label>
                                </div>
                                <asp:DropDownList ID="ddlForSemester" AppendDataBoundItems="true" runat="server"
                                    CssClass="form-control" OnSelectedIndexChanged="ddlForSemester_SelectedIndexChanged" AutoPostBack="True" data-select2-enable="true" />
                                <asp:RequiredFieldValidator ID="rfvForSemester" runat="server" ControlToValidate="ddlForSemester"
                                    Display="None" ErrorMessage="Please Select Demand for Semester" ValidationGroup="submit"
                                    InitialValue="0" SetFocusOnError="true" />

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="overwrite" visible="false">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:CheckBox ID="chkOverwrite" Text="Overwrite existing demand" runat="server" />
                            </div>

                        </div>
                    </div>

                    <div class="col-12" runat="server" id="filter">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Filter By</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Degree </label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Branch </label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShowStudents" runat="server" Text="Show Students" OnClick="btnShowStudents_Click"
                            ValidationGroup="submit" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCreateDemand" runat="server" Text="Create Demand for All" OnClick="btnCreateDemand_Click"
                            ValidationGroup="submit" CssClass="btn btn-primary" Visible="false" />
                        <asp:Button ID="btnDemand" runat="server" Text="Create Demand" OnClick="btnDemand_Click"
                            ValidationGroup="submit" Visible="false" CssClass="btn btn-primary" />
                        <asp:Button ID="btnSlip" runat="server" Text="Print Challan" OnClick="btnSlip_Click"
                            ValidationGroup="submit" CssClass="btn btn-info" Visible="false" />

                        <asp:ValidationSummary ID="valSummery" DisplayMode="List" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="submit" />
                    </div>

                    <div class="col-12" runat="server" visible="false">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Reports</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <asp:RadioButton ID="rdoDetailedReport" GroupName="report" runat="server"
                                    Text="Detailed Report" Checked="true" />
                                <asp:RadioButton ID="rdoSummaryReport" GroupName="report" runat="server"
                                    Text="Summary Report" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-primary"
                                    ValidationGroup="submit" OnClick="btnShowReport_Click" />
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="PnlLv" runat="server">
                        <div id="divSelectedStudents" runat="server" visible="false">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Demand Creation for selected students</h5>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnCreateDemandForSelStuds" runat="server" Text="Create Demand for Selected Students"
                                    OnClick="btnCreateDemandForSelStuds_Click" ValidationGroup="submit" CssClass="btn btn-primary" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvStudents" runat="server">
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div class="sub-heading">
                                                <h5>List of Students</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="return CheckAll(this);" />  Select All
                                                        </th>
                                                        <th>Reg. No.
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Degree
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Amount
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
                                                <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                <asp:HiddenField ID="hidStudentNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("DEGREE")%>
                                            </td>
                                            <td>
                                                <%# Eval("BRANCH")%>
                                                <asp:HiddenField ID="HidBranchNo" runat="server" Value='<%# Eval("BRANCHNO")%>' />
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTER")%>
                                                <asp:HiddenField ID="HidSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                <asp:HiddenField ID="HidPtypeNo" runat="server" Value='<%# Eval("PTYPE")%>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Text='  <%# Eval("AMOUNT")%>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" CssClass="btn btn-warning" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        function CheckAll(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;
                        headchk.checked == true;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGatePass_chkAll').checked = true;
                    }
                    else {
                        e.checked = false;
                        headchk.checked == false;
                        document.getElementById('ctl00_ContentPlaceHolder1_lvGatePass_chkAll').checked = false;
                    }
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
  
</asp:Content>

