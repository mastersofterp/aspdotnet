<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentHostelIdentityCard.aspx.cs" Inherits="Hostel_Report_StudentHostelIdentityCard"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT HOSTEL IDENTITY CARD</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Session No. </label>
                                </div>
                                <asp:DropDownList ID="ddlHostelSessionNo" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelSession" runat="server" ControlToValidate="ddlHostelSessionNo"
                                    Display="None" ErrorMessage="Please Select Hostel Session" InitialValue="0" ValidationGroup="Show"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <label>Hostel No. </label>
                                </div>
                                <asp:DropDownList ID="ddlHostelNo" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlHostelNo_SelectedIndexChanged" AutoPostBack="True" TabIndex="2" data-select2-enable="true">
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlHostelNo"
                                    Display="None" ErrorMessage="Please Select Hostel" InitialValue="0" ValidationGroup="Show"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Block No.</label>
                                </div>
                                <asp:DropDownList ID="ddlBlockNo" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlBlockNo_SelectedIndexChanged" AutoPostBack="True" TabIndex="3" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Floor</label>
                                </div>
                                <asp:DropDownList ID="ddlFloor" runat="server" CssClass="form-control" TabIndex="4" AppendDataBoundItems="True" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                            </div>

                        
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click"
                            ToolTip="Shows Students under Selected Criteria" ValidationGroup="Show" CssClass="btn btn-primary" TabIndex="5" />

                        <asp:Button ID="btnPrintReport" runat="server" Text="ID Card" OnClick="btnPrintReport_Click"
                            ToolTip="Show Report" OnClientClick="return validateAssign();" CssClass="btn btn-info" TabIndex="6" />


                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" TabIndex="7" />

                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ValidationGroup="Show" ShowSummary="False" />

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="academic" />
                    </div>

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Total Students </label>
                                </div>
                                <asp:TextBox ID="txtTotStud" runat="server" Enabled="false" Text="0" CssClass="watermarked form-control" TabIndex="8"></asp:TextBox>
                                <asp:HiddenField ID="hftot" runat="server" Value="0" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 d-none">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add Student for Hostel Identity Card Printing</h5>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Enter Enrollment No. </label>
                                </div>
                                <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control" ToolTip="Enter Enrollment No." TabIndex="9"></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="academic"
                                    CssClass="btn btn-primary" ToolTip="Add Enrollment No. into List." TabIndex="10" />

                                <asp:RequiredFieldValidator ID="rfvSearchText" runat="server" ControlToValidate="txtSearchText"
                                    ErrorMessage="Please Enter Enrollment No." ValidationGroup="academic" SetFocusOnError="true"
                                    Display="None">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblEnrollmentNo" runat="server" SkinID="Msglbl">
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:ListView ID="lvStudentRecords" runat="server">
                            <LayoutTemplate>
                                <div id="listViewGrid">
                                    <div class="sub-heading">
                                        <h5>Search Results</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkIdentityCard" runat="server" onclick="totAllSubjects(this);"
                                                        ToolTip="Select or Deselect All Records" />
                                                </th>
                                                <th>Student Name
                                                </th>
                                                <th>Enroll. No.
                                                </th>
                                                <th>Branch Name
                                                </th>
                                                <th>Year Name
                                                </th>
                                                <th>ID Alloted Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                                <%--<div class="listview-container">
                                            <div id="demo-grid" class="vista-grid">
                                                <table id="tblStudentRecords" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>--%>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkReport" runat="server" onclick="totSubjects(this)" />
                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("ENROLLMENTNO")%>
                                    </td>
                                    <td>
                                        <%# Eval("SHORTNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("YEARNAME")%>
                                    </td>
                                    <td>
                                       <asp:Label ID="lblAllotStatus" runat="server" ForeColor='<%# Eval("ALLOTED_STATUS").ToString().Equals("ALLOTED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ALLOTED_STATUS")%>'></asp:Label>
                                    </td>
                           
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" class="data_label">
                                    -- No Student Record Found --
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
                        var hftot = document.getElementById('<%= hftot.ClientID %>');

                        var frm = document.forms[0]
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            var e = frm.elements[i];
                            if (e.type == 'checkbox') {
                                if (headchk.checked == true)
                                    e.checked = true;
                                else
                                    e.checked = false;
                            }
                        }

                        if (headchk.checked == true)
                            txtTot.value = hftot.value;
                        else
                            txtTot.value = 0;
                    }

                    function validateAssign() {
                        var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

                        if (txtTot == 0 || document.getElementById('<%= ddlHostelSessionNo.ClientID %>').selectedIndex == 0) {
                            alert('Please Select Atleast One Student for Hostel ID Card Printing.');
                            return false;
                        }
                        else
                            return true;
                    }

    </script>
</asp:Content>
