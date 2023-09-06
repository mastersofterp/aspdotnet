<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttendanceProcess_Configuration.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ESTABLISHMENT_LEAVES_Master_AttendanceProcess_Configuration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<asp:UpdatePanel ID="pnlupdate" runat="server">
        <ContentTemplate>--%>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ATTENDANCE PROCESS CONFIGURATION</h3>
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
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="1"  ToolTip="Please Select College">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" ValidationGroup="config"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstaff" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                               AppendDataBoundItems="true"  ToolTip="Select Staff Type">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlstaff"
                                                Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="config"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Process From :</label>
                                            </div>
                                            <asp:TextBox ID="txtProcessFrom" runat="server" TabIndex="3" ToolTip="Enter Process From" MaxLength="2" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtProcessFrom"
                                                Display="None" ErrorMessage="Please Enter Process From Day" ValidationGroup="config"
                                                SetFocusOnError="True" InitialValue=" ">
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtProcessFrom"
                                                Display="None" ErrorMessage="Please Enter Process From" ValidationGroup="config"
                                                SetFocusOnError="true" MinimumValue="0" MaximumValue="31" Type="Integer"></asp:RangeValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtProcessFrom" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Process To :</label>
                                            </div>
                                           <asp:TextBox ID="txtProcessTo" runat="server" TabIndex="4" ToolTip="Enter Process To" MaxLength="2" onkeypress="return CheckNumeric(event,this);" ></asp:TextBox>   
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProcessTo"
                                                Display="None" ErrorMessage="Please Enter Process To Day" ValidationGroup="config"
                                                SetFocusOnError="True" InitialValue=" ">
                                            </asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngProcessTo" runat="server" ControlToValidate="txtProcessTo"
                                                Display="None" ErrorMessage="Please Enter Process To" ValidationGroup="config"
                                                SetFocusOnError="true" MinimumValue="0" MaximumValue="31" Type="Integer"></asp:RangeValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTBTo" runat="server" TargetControlID="txtProcessTo" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="config" Text="Submit" TabIndex="11"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="12"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valConfig" ValidationGroup="config" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" />
                            </div>
                          </asp:Panel>
                            <asp:Panel ID="pnlList" runat="server">
                    <div class="col-12">
                        <asp:Repeater ID="lvAttProcess" runat="server">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>Attendance Process List</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Action
                                            </th>
                                            <th>College Name
                                            </th>
                                            <th>Staff Type
                                            </th>
                                            <th>Process From
                                            </th>
                                            <th>Process To
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" CommandArgument='<%# Eval("ConfigNo") %>'
                                            AlternateText="Edit Record" ToolTip='<%# Eval("ConfigNo") %>' TabIndex="16" />&nbsp;
                                                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("HNO") %>'
                                                    AlternateText="Delete Record" ToolTip='<%# Eval("ConfigNo") %>'
                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                    </td>
                                    <td>
                                        <%# Eval("COLLEGE_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("STAFFTYPE")%>
                                    </td>
                                    <td>
                                        <%# Eval("ProcessFromDay") %>
                                    </td>
                                    <td>
                                        <%# Eval("ProcessToDay") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </asp:Panel>
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
       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit"/>
        </Triggers>

    </asp:UpdatePanel>--%>
</asp:Content>