<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelStudentBonafideCertificate.aspx.cs" Inherits="Academic_HostelStudentBonafideCertificate"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Hostel Bonafide</h3>
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
                                    <label>Session No. </label>
                                </div>
                                <asp:DropDownList ID="ddlHostelSessionNo" runat="server" AppendDataBoundItems="True"
                                    TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                    ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel No.</label>
                                </div>
                                <asp:DropDownList ID="ddlHostelNo" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelNo" runat="server" ErrorMessage="Please Select Hostel No"
                                    ControlToValidate="ddlHostelNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="tradm" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Admission Year </label>
                                </div>
                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true"
                                    TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ErrorMessage="Please Select Admission Year"
                                    ControlToValidate="ddlAdmBatch" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-4 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:RadioButton ID="rdoBonafide" Text="Bonafide  Certificate"
                                    GroupName="ReportType" TabIndex="4" Checked="true" runat="server" />

                                &nbsp;<asp:RadioButton ID="rdoHostel" Text="Hostel Residence Certificate "
                                    GroupName="ReportType" TabIndex="4" runat="server" />
                            </div>


                            <div class="form-group col-lg-2 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Total Selected Students </label>
                                </div>
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" CssClass="watermarked form-control" Enabled="false" TabIndex="5" />
                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click"
                            ToolTip="Shows Students under Selected Criteria." ValidationGroup="Submit" TabIndex="6" CssClass="btn btn-primary" />
                        <asp:Button ID="btnPrintReport" runat="server" Text="Report"
                            OnClick="btnPrintReport_Click" ToolTip="Show Report Under Selected Criteria."
                            TabIndex="7" OnClientClick="return validateAssign();" CssClass="btn btn-info" ValidationGroup="Submit" Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                            OnClick="btnCancel_Click" TabIndex="8" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="vsBonafide" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                    </div>

                    <div class="col-12" id="fld" runat="server" visible="false">
                        <div class="row" id="divAddStud" runat="server" visible="false">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add Student for Hostel Bonafide Certificate Printing</h5>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Enter Enrollment No. </label>
                                </div>
                                <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control" ToolTip="Enter Enrollment No."></asp:TextBox>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="Add"
                                    CssClass="btn btn-primary" ToolTip="Add Enrollment No. into List." />

                                <asp:RequiredFieldValidator ID="rfvSearchText" runat="server" ControlToValidate="txtSearchText"
                                    ErrorMessage="Please Enter Enrollment No." ValidationGroup="Add" SetFocusOnError="true"
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
                                                    <asp:CheckBox ID="cbRow1" runat="server" onclick="totAllSubjects(this)" TabIndex="4" />
                                                </th>
                                                <th>Student Name
                                                </th>
                                                <th>Enroll. No.
                                                </th>
                                                <th>Branch Name
                                                </th>
                                                <th>Remark
                                                </th>
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
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO") %>' TabIndex="5" />
                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("NAME")%>
                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("ENROLLMENTNO") %>'
                                            Visible="false" />
                                    </td>
                                    <td>
                                        <%# Eval("ENROLLMENTNO")%>
                                    </td>
                                    <td>
                                        <%# Eval("SHORTNAME")%>
                                        <asp:Label ID="lblroom" runat="server" ToolTip='<%# Eval("ROOM_NO") %>'
                                            Visible="false" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRemark" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Remark for Late"
                                            CssClass="form-control" data-select2-enable="true" onchange="enableDisabled(this);">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </ItemTemplate>
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
                    var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

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
                        txtTot.value = hdfTot.value;
                    else
                        txtTot.value = 0;
                }

                //	function validateAssign()
                //	{
                //	    var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;	    
        //	    				
        //		if (txtTot == 0 || document.getElementById('<%= ddlAdmBatch.ClientID %>').selectedIndex == 0)
        //		{		
        //			alert('Please Select Atleast One Student for Bonafide Certificate');
        //			return false;
        //	    }
        //		else		
        //			return true;
        //	}		

    </script>
</asp:Content>
