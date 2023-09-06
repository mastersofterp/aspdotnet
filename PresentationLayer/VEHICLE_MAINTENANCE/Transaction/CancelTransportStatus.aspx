<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CancelTransportStatus.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_CancelTransportStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CANCEL STUDENT TRANSPORT STATUS</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>College/School Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select College"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="false" TabIndex="1">
                                                <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvClg" runat="server" SetFocusOnError="true"
                                                Display="None" ErrorMessage="Please Select College."
                                                ValidationGroup="Submit" ControlToValidate="ddlCollege"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-5 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Transport Type</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdotrasnsporttyepe" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="C" Text="Transport Cancel &nbsp&nbsp" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="A" Text="Transport Apply  &nbsp&nbsp"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="13"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Submit" TabIndex="12"
                                    CssClass="btn btn-primary" ToolTip="Click here to Show" CausesValidation="true" OnClick="btnShow_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="14" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />
                                <%--&nbsp;<asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" TabIndex="14"
                            CssClass="btn btn-info" ToolTip="You can also select Hire Type." />--%>
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvStudentList" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Student Transport List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbAl" runat="server" TabIndex="7" AutoPostBack="true" onclick="totAllSubjects(this)" />
                                                        </th>
                                                        <th>Sr.No
                                                        </th>
                                                        <th>TAN/PAN 
                                                        </th>
                                                        <th>Student Name
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
                                                <asp:CheckBox ID="chkIdNo" runat="server" ToolTip='<%# Eval("IDNO") %>' AutoPostBack="true" />
                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />

                                            </td>
                                            <td>
                                                <%#Container.DataItemIndex+1 %>
                                            </td>
                                            <td>
                                                <%# Eval("ENROLLNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>


                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            var j = 0;
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                debugger;
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (j != 0) {
                            e.checked = true;
                        }
                        j++;
                    }
                    else
                        e.checked = false;
                }
            }

        }
        function Checkedfalse(headchk) {

        }
    </script>

</asp:Content>

