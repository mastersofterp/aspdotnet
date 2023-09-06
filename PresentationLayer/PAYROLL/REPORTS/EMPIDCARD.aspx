<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="EMPIDCARD.aspx.cs" Inherits="EMPIDCARD"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE ID CARD</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>
                            <asp:Panel ID="pnluser" runat="server">
                                <div class="panel panel-info">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Select Staff Type and Department</h5>
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
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" TabIndex="1" ToolTip="Select College">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaffName" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Select Staff Type" OnSelectedIndexChanged="ddlStaffName_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlStaffName"
                                                    Display="None" ErrorMessage="Please Select Staff" ValidationGroup="payroll"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                              <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>ID Card Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlIDCardType" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Select ID Card Type" OnSelectedIndexChanged="ddlIDCardType_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlIDCardType"
                                                  Display="None" ErrorMessage="Please Select ID Card Type" ValidationGroup="payroll"
                                                 InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" style="visibility:hidden">
                                                <div class="label-dynamic">
                                                    <label>Department</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Select Department" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Visible="false">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepartment"
                                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="payroll"
                                                                InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>
                                           
                                            <div class="form-group col-lg-3 col-md-6 col-12"  style="visibility:hidden">
                                                <div class="label-dynamic">
                                                    <label>Order By</label>
                                                </div>
                                                <asp:DropDownList ID="ddlOrderBy" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="4" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Select Order By" OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged" Visible="false">
                                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="0">IDNO</asp:ListItem>
                                                    <asp:ListItem Value="1">SEQ_NO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                           <asp:Panel ID="pnlButton" runat="server">
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnBothSide" runat="server" Text="Show" ValidationGroup="payroll" CssClass="btn btn-primary"
                                        OnClick="btnBothSide_Click" ToolTip="Get front and backside of Id Card" TabIndex="5" />
                                    <asp:Button ID="btnSave" runat="server" Text="Front Side" ValidationGroup="payroll" CssClass="btn btn-primary"
                                        OnClick="btnSave_Click" ToolTip="Get front of Id Card" TabIndex="6" Visible="false" />
                                    <%-- <asp:Button ID="btnBackSide" runat="server" Text="Back Side" ValidationGroup="payroll" CssClass="btn btn-info"
                                                    OnClick="btnBackSide_Click" ToolTip="Get backside of Id Card" TabIndex="6" Visible="true"/>&nbsp;--%>
                                    <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" ValidationGroup="payroll"
                                                     Width="80px" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />--%>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" TabIndex="7" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlPayhead" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvPayhead" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Employee Name</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" />Employee Name
                                                        </th>
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
                                                    <asp:CheckBox ID="chkID" runat="server" AlternateText="Check to allocate Payhead"
                                                        ToolTip='<%# Eval("IDNO")%>' />
                                                    <%# Eval("NAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function totalAppointment(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>
