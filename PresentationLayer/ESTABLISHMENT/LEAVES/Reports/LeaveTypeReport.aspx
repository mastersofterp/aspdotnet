<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeaveTypeReport.aspx.cs"
    Inherits="ESTABLISHMENT_LEAVES_Reports_LeaveTypeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">LEAVE PD/PDL STATUS</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                       <%-- Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>--%>
                                        <asp:Panel ID="pnlAdd" runat="server">
                                            <div class="panel panel-info">
                                               <%-- <div class="panel panel-heading">Select Criteria for Leave PD/PDL Status</div>--%>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4" id="Div2" runat="server">
                                                            <label>College <span style="color: #FF0000">*</span>:</label>
                                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                                CssClass="form-control" ToolTip="Select College" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                                Display="None" ErrorMessage="Please select College" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-4" id="trddldept" runat="server">
                                                            <label>Select Department :</label>
                                                            <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control" TabIndex="2" AutoPostBack="True" ToolTip="Select Department"
                                                                OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-md-4" id="trddlstaff" runat="server">
                                                            <label>Staff Type:</label>
                                                            <asp:DropDownList ID="ddlstafftype" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control" TabIndex="3" AutoPostBack="True" ToolTip="Select Staff"
                                                                OnSelectedIndexChanged="ddlstafftype_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4">
                                                            <label></label>
                                                            <asp:RadioButtonList ID="rblAllParticular" runat="server"
                                                                RepeatDirection="Horizontal" TabIndex="4"
                                                                OnSelectedIndexChanged="rblAllParticular_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="&nbsp&nbsp All Employees&nbsp&nbsp" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="&nbsp&nbsp Particular Employee" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="form-group col-md-4" id="tremp" runat="server">
                                                            <label>Select Employee <span style="color: #FF0000">*</span>:</label>
                                                            <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="5"
                                                                ToolTip="Select Employee">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                                Display="None" ErrorMessage="Please select Employee" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4">
                                                            <label>From Date <span style="color: #FF0000">*</span>:</label>
                                                             <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                     <i id="imgCalFromDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                                 </div>
                                                                <asp:TextBox ID="txtFromdt" runat="server" onblur="return checkdate(this);" Style="z-index: 0;"
                                                                    CssClass="form-control" TabIndex="6" ToolTip="Enter From Date"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                                    Format="dd/MM/yyyy" PopupButtonID="imgCalFromDate" TargetControlID="txtFromdt">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="mefrmdt" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                                                <asp:RequiredFieldValidator ID="rfvfdate" runat="server" ControlToValidate="txtFromdt"
                                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                                    ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <label>To Date <span style="color: #FF0000">*</span>:</label>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                      <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtTodt" runat="server" MaxLength="7" Style="z-index: 0;"
                                                                    CssClass="form-control" TabIndex="7" ToolTip="Enter To Date" />
                                                                <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                                                </asp:RequiredFieldValidator>
                                                                <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                                    Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" TargetControlID="txtTodt" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-4" id="tr1" runat="server">
                                                            <label>Leave Type :</label>
                                                            <asp:RadioButtonList ID="rdbleavestatus" runat="server"
                                                                RepeatDirection="Horizontal" TabIndex="8">
                                                                <asp:ListItem Enabled="true" Selected="True" Text="&nbsp&nbsp PD &nbsp&nbsp&nbsp&nbsp" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="&nbsp&nbsp PDL" Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="box-footer">
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Leaveapp" TabIndex="9"
                                        CssClass="btn btn-info" OnClick="btnReport_Click" ToolTip="Click here to Show Report" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10"
                                        CssClass="btn btn-danger" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                                    <div class="text-center">
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                                                <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                                <div class="text-center">
                                                    <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                                    <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

