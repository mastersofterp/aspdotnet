<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Month_Lock.aspx.cs"
    Inherits="ESTATE_Transaction_Month_Lock" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function concheck() {

            var result = confirm("Do you want to update?");
            if (result == true) {

            }
            else {
                return false;
            }
        }

    </script>

    <asp:UpdatePanel ID="updReport" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MONTHLY LOCKING</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Monthly Lock
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Select Locking Month<span style="color: red;">*</span>:</label>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtselectdate" runat="server" TabIndex="1" MaxLength="7" CssClass="form-control"
                                                            OnTextChanged="txtselectdate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calextenderdatebirth" runat="server" Enabled="True" Format="MM/yyyy"
                                                            PopupButtonID="imgCal" TargetControlID="txtselectdate" />
                                                        <div class="input-group-addon">
                                                           <%-- <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                              <asp:ImageButton runat="Server" ID="imgcal" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Due Date<span style="color: red;">*</span>:</label>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtDueDate" runat="server" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgDue" TargetControlID="txtDueDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                            AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDueDate" />
                                                        <div class="input-group-addon">
                                                            <%--<asp:Image ID="imgDue" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                            <asp:ImageButton runat="Server" ID="imgDue" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Last Date<span style="color: red;">*</span>:</label>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtLastDate" runat="server" TabIndex="3" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgLast" TargetControlID="txtLastDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                            AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtLastDate" />
                                                        <div class="input-group-addon">
                                                            <%--<asp:Image ID="imgLast" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                            <asp:ImageButton runat="Server" ID="imgLast" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label><span style="color: red">Lock/Unlock</span>:</label>
                                                    <br />
                                                    <asp:CheckBox ID="chkmonthlock" runat="server" Text="Lock For Energy" TabIndex="4"
                                                        OnCheckedChanged="chkmonthlock_CheckedChanged" AutoPostBack="true" />
                                                    <asp:CheckBox ID="chkunlockenergy" runat="server" Text="Unlock For Energy " TabIndex="5"
                                                        OnCheckedChanged="chkunlockenergy_CheckedChanged" AutoPostBack="true" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="text-center">
                                                        <asp:Button ID="btnmonthlock" runat="server" OnClick="btnmonthlock_Click" OnClientClick="concheck();" TabIndex="6"
                                                            Text="Submit" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnreset" runat="server" OnClick="btnreset_Click" Text="Reset" CssClass="btn btn-warning" TabIndex="7" />
                                                        <asp:Button ID="btnProcessBill" runat="server" OnClick="btnProcessBill_Click" Text="Process Bill" CssClass="btn btn-success"
                                                            Enabled="false" TabIndex="8" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlList" runat="server">
                                                        <div class="table-responsive">
                                                            <asp:ListView ID="lvLock" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="lgv1">
                                                                        <h4 class="box-title">Lock/Unlock Entry List
                                                                        </h4>
                                                                        <table class="table table-bordered table-hover">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>EDIT
                                                                                    </th>
                                                                                    <th>BILL MONTH
                                                                                    </th>
                                                                                    <th>LOCKING MONTH
                                                                                    </th>
                                                                                    <th>DUE DATE
                                                                                    </th>
                                                                                    <th>LAST DATE
                                                                                    </th>
                                                                                    <th>STATUS
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
                                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                                CommandArgument='<%# Eval("IDNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                OnClick="btnEdit_Click" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MONTH")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("LOCKING_DATE","{0:MMM-yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DUE_DATE","{0:dd-MMM-yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("LAST_DATE","{0:dd-MMM-yyyy}")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("STATUS")%>
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
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

