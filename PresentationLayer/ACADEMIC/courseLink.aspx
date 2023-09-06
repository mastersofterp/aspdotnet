<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="courseLink.aspx.cs" Inherits="Registration_courseLink" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Updpnldetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SectionWise Course Entry</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Term</label>
                                        </div>
                                        <asp:Label ID="lblCurrentSession" runat="server" Font-Bold="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Program</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="courseLink" AppendDataBoundItems="true"
                                            ToolTip="Select Scheme">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server"
                                            ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please select scheme."
                                            ValidationGroup="courseLink" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" ValidationGroup="courseLink"
                                            ToolTip="Select Section"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server"
                                            ControlToValidate="ddlSection" Display="None" ErrorMessage="Please select section."
                                            ValidationGroup="courseLink" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>No. Of Subjects</label>
                                        </div>
                                        <asp:TextBox ID="txtTotChk" runat="server" CssClass="form-control" Enabled="false" ValidationGroup="courseLink" ToolTip="Total Subject Selected" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                    OnClick="btnSubmit_Click" ValidationGroup="courseLink" ToolTip="Submit"
                                    CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" CausesValidation="False" ToolTip="Cancel"
                                    CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="courseLink" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                <asp:Label ID="lblstatus" runat="server" SkinID="Errorlbl" /></td>
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvCourseLink" runat="server" OnItemDataBound="lvCourseLink_ItemDataBound">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>City List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" /></th>
                                                    <th style="display: none">CCode</th>
                                                    <th>Course Name</th>
                                                    <th>Program Name</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("CNO") %>' /></td>
                                            <td style="display: none">
                                                <asp:Label ID="lblCCODE" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("COURSENO")%>' /></td>
                                            <td><%# Eval("COURSENAME")%></td>
                                            <td><%# Eval("SCHEMENAME")%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("CNO") %>' /></td>
                                            <td style="display: none">
                                                <asp:Label ID="lblCCODE" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("COURSENO")%>' /></td>
                                            <td><%# Eval("COURSENAME")%></td>
                                            <td><%# Eval("SCHEMENAME")%></td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');
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
    </script>
</asp:Content>
