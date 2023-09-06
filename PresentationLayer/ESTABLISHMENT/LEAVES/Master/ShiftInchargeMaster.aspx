<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ShiftInchargeMaster.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_ShiftInchargeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">

        function totAllSubjects(headchk) {
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
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SHIFT INCHARGE MASTER</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSelect" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                        TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Incharge"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                        TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaffType"
                                        Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Incharge"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>*</sup>--%>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                        TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" hidden>
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Ward Number</label>
                                    </div>
                                    <asp:TextBox ID="txtWardNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Incharge"
                                CssClass="btn btn-outline-primary" OnClick="btnSave_Click" />
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" ValidationGroup="Incharge"
                                CssClass="btn btn-outline-primary" OnClick="btnRemove_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Incharge"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" Width="338px" />
                        </div>

                    </asp:Panel>

                    <div class="col-12 mt-3 mb-3">
                        <asp:Panel ID="pnlIncharge" runat="server">
                            <asp:ListView ID="lvIncharge" runat="server">
                                <EmptyDataTemplate>
                                    <div class="text-center">
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                    </div>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Employee List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />

                                                        Select</th>
                                                    <th>Employee Name
                                                    </th>
                                                    <%-- <th style="width: 20%">
                                                                              Ward No.
                                                                            </th>--%>
                                                </tr>
                                                <thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <asp:CheckBox ID="chkIncharge" runat="server" AlternateText="Check to select incharge"
                                                ToolTip='<%# Eval("INCHARGEEMPLOYEEIDNO")%>' />

                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <%--<td style="width: 20%">                                                                                   
                                                                     <%# Eval("WARD_NO")%>
                                                                </td>  --%>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="altitem">
                                        <td>
                                            <asp:CheckBox ID="chkIncharge" runat="server" AlternateText="Check to select incharge"
                                                ToolTip='<%# Eval("INCHARGEEMPLOYEEIDNO")%>' />
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <%--<td style="width: 20%">
                                                                   
                                                                     <%# Eval("WARD_NO")%>
                                                                </td> --%>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

