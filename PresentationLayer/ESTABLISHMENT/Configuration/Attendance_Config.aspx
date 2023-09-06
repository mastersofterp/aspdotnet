<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Attendance_Config.aspx.cs" Inherits="ESTABLISHMENT_Attendance_Config"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="pnlupdate" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ATTENDANCE CONFIGURATION</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select College & Staff Type</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" ToolTip="Select College"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="config"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Select Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstaff" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlstaff_SelectedIndexChanged" ToolTip="Select Staff Type">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlstaff"
                                                Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="config"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlStatus" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Criteria for Attendance Configuration</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvStatus" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" CssClass="d-block text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Status</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Condition
                                                        </th>
                                                        <th>From
                                                        </th>
                                                        <th>To
                                                        </th>
                                                        <th>Allowed Up To
                                                        </th>
                                                        <th>No.of leaves
                                                        </th>
                                                        <%--<th width="12%">
                                                                                        Leave Type
                                                                                    </th>--%>
                                                    </tr>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </thead>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblStatusno" runat="server" ToolTip='<%# Eval("STATUSNO")%>' Visible="false"></asp:Label>
                                                    <%# Eval("STATUS_DESC")%>
                                                                                &nbsp;&nbsp;
                                                                                <asp:Label ID="lblCrDed" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFrom" runat="server"
                                                        Text='<%# Eval("FROM_TIME")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTo" runat="server"
                                                        Text='<%# Eval("TO_TIME")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAllowedUpTo" runat="server" onkeyup="return validateNumeric(this)"
                                                        onkeydown="return validateNumeric(this)" Text='<%# Eval("ALLOWED_UP_TO")%>'></asp:TextBox>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNoOfLeaves" runat="server" onkeyup="return validateNumeric(this)"
                                                        onkeydown="return validateNumeric(this)" Text='<%# Eval("NO_LEAVE")%>'></asp:TextBox>

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>No. of Days allowed Before Current date For OD Slip</label>
                                            </div>
                                            <asp:TextBox ID="txtODDays" runat="server" CssClass="form-control"
                                                ToolTip="Enter No. of Days allowed Before Current date For OD Slip"
                                                onkeypress="return CheckNumeric(event,this);" TabIndex="3"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>LWP Leave No</label>
                                            </div>
                                            <asp:DropDownList ID="ddlLWP" runat="server" CssClass="form-control" ToolTip="Select LWP Leave Number" data-select2-enable="true" TabIndex="4"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>No. of Days allowed Before Current date For OD application</label>
                                            </div>
                                            <asp:TextBox ID="txtODAppDays" runat="server" CssClass="form-control" TabIndex="5"
                                                ToolTip="Enter No. of Days allowed Before Current date For OD application"
                                                onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Comp-Off Leave No</label>
                                            </div>
                                            <asp:DropDownList ID="ddlComp" runat="server" CssClass="form-control" ToolTip="Select Comp-off Leave Number" data-select2-enable="true" TabIndex="6"
                                                AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12" id="trShiftmedical" runat="server" visible="false">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>No. of Shift Available</label>
                                        </div>
                                        <asp:TextBox ID="txtAvaiShift" runat="server" CssClass="form-control" TabIndex="7"
                                            Enabled="False" ToolTip="Number of Shift Available" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Medical Leave No</label>
                                        </div>
                                        <asp:DropDownList ID="ddlMedical" runat="server" ToolTip="Select Medical Leave Number" CssClass="form-control" data-select2-enable="true" TabIndex="8"
                                            AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnAddAnotherShift" runat="server" Text="Add Another Shift" TabIndex="9"
                                        CssClass="btn btn-primary" ToolTip="Click here to Add Another Shift" OnClick="btnAddAnotherShift_Click" />
                                    <%--ValidationGroup="config"--%>
                                    <asp:Button ID="btnShiftReport" runat="server" Text="Shift Report" TabIndex="10"
                                        CssClass="btn btn-primary" ToolTip="Click here for Shift Report" OnClick="btnShiftReport_Click" />
                                    <asp:Button ID="btnRestrict" runat="server" ValidationGroup="config" Text="Restrict Leave taken"
                                        CssClass="btn btn-primary" ToolTip="Click here to Restrict Leave Taken" TabIndex="10"
                                        OnClick="btnRestrict_Click" Visible="false" />

                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="config" Text="Submit" TabIndex="11"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="12"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valConfig" ValidationGroup="config" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <script type="text/javascript" language="javascript">

                function CheckNumeric(event, obj) {
                    var k = (window.event) ? event.keyCode : event.which;
                    //alert(k);
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                        obj.style.backgroundColor = "White";
                        return true;
                    }
                    if (k > 45 && k < 58) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter numeric Value');
                        obj.focus();
                    }
                    return false;
                }
                function validateNumeric(txt) {
                    if (isNaN(txt.value)) {

                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = '';
                        txt.focus = true;
                        alert("Only Numeric Characters allowed !");
                        return false;
                    }
                    else
                        return true;
                    //alert("validation");
                }

                function validateAlphabet(txt) {
                    var expAlphabet = /^[A-Za-z]+$/;
                    if (txt.value.search(expAlphabet) == -1) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = '';
                        txt.focus = true;
                        alert("Only Alphabets allowed!");
                        return false;
                    }
                    else
                        return true;
                }


            </script>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShiftReport"/>
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>
