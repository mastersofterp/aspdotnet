<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SectionMaster.aspx.cs" Inherits="FileMovementTracking_Master_SectionMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <%--<asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SECTION MASTER</h3>
                </div>


                <div class="box-body">

                    <asp:Panel ID="pnlSection" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Create Section</h5>
                            </div>
                            <div class="row">


                                <div class="col-lg-8 col-md-12 col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Department </label>
                                            </div>

                                            <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server" TabIndex="1" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Department" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                              <asp:RequiredFieldValidator ID="rfvDept" runat="server" ErrorMessage="Please Select Department"
                                                            ControlToValidate="ddlDepartment" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Receiver Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlReceiver" CssClass="form-control" runat="server" TabIndex="1" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Receiver Name" AutoPostBack="true" OnSelectedIndexChanged="ddlReceiver_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlR" runat="server" ErrorMessage="Please Select Receiver Name."
                                                ControlToValidate="ddlReceiver" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Section User Name </label>
                                            </div>
                                            <asp:TextBox ID="txtSecName" runat="server" MaxLength="80" TabIndex="1" CssClass="form-control"
                                                onkeypress="return CheckAlphaNumeric(event,this);" ToolTip="Enter Section Name" />
                                            <asp:RequiredFieldValidator ID="rfvSecName" runat="server" ControlToValidate="txtSecName"
                                                Display="None" ErrorMessage="Please Enter Section Name." ValidationGroup="Submit">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12" id="divReceivingHead" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Receiving Head Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReceiverHead" CssClass="form-control" runat="server" TabIndex="1"
                                                AppendDataBoundItems="true" ToolTip="Select Receiving Head Name">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvddlRHead" runat="server" ErrorMessage="Please Select Receiver Head Name."
                                                            ControlToValidate="ddlReceiverHead" InitialValue="0"
                                                            Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        </div>

                                         <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="1" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" OnClientClick="return ValidateFields()" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="1" OnClick="btnReport_Click" CssClass="btn btn-info"
                            CausesValidation="false" ToolTip="Click here to Show Report" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click" CssClass="btn btn-warning"
                            CausesValidation="false" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="Submit" />

                    </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-12 col-12">
                                    <asp:Panel ID="pnlRole" runat="server">
                                        <asp:ListView ID="lvRoles" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Role Name List</h5>
                                                    </div>
                                                    <div style="height: 180px; overflow: auto">
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>
                                                                        <%--  <asp:CheckBox ID="chkHead" runat="server" Checked="false" onclick="totAllSubjects(this)" />      --%>                                                                                   
                                                                    </th>
                                                                    <th>Role Name
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" Checked="false" ToolTip='<%# Eval("ROLE_ID")%>' TabIndex="1" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROLENAME") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>



                   
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvSection" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Section Name Entry List</h5>
                                    </div>
                                    <div id="lgv1">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>EDIT
                                                    </th>
                                                    <th>SECTION NAME
                                                    </th>
                                                    <th>RECEIVER NAME
                                                    </th>
                                                    <%-- <th>RECEIVING HEAD NAME
                                                            </th>--%>
                                                    <th>DEPARTMENT
                                                    </th>
                                                    <%--   <th>
                                                             POST FROM DATE
                                                             </th>
                                                            <th>
                                                                POST TO DATE
                                                            </th>  --%>
                                                    <th>USER ROLES 
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("SECTION_ID")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        <td>
                                            <%# Eval("SECTION_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("RECEIVER_NAME")%>
                                        </td>
                                        <%-- <td>
                                                    <%# Eval("RECEIVER_HEAD_NAME")%>
                                                </td>--%>
                                        <td>
                                            <%# Eval("DEPTNAME")%>
                                        </td>
                                        <%-- <td>
                                                        <%# Eval("POST_FROM_DATE","{0:dd-MMM-yyyy}")%>
                                                    </td>
                                                         <td>
                                                        <%# Eval("POST_TO_DATE","{0:dd-MMM-yyyy}")%>
                                                    </td>--%>
                                        <td>
                                            <%# Eval("USER_ROLES")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%-- </ContentTemplate>
    </asp:UpdatePanel> --%>
    
    <script type="text/javascript">

        function ValidateFields() {
            document.getElementById("ctl00_ContentPlaceHolder1_btnSubmit").onclick = function () {
                //disable
                this.disabled = true;
            }
        }

        </script>
</asp:Content>

