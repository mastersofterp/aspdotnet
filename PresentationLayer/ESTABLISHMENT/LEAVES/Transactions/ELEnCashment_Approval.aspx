<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ELEnCashment_Approval.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_ELEnCashment_Approval"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EL ENCASHMENT APPROVAL</h3>
                        </div>

                        <div class="box-body">
                            <%--<asp:Panel ID="pnlAdd" runat="server">--%>
                            <div id="pnlAdd" runat="server">
                                <asp:UpdatePanel ID="updAdd" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>College Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" data-select2-enable="true"
                                                        ToolTip="Please Select College" CssClass="form-control" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="2"
                                                            ToolTip="Enter From Date" AutoPostBack="true" OnTextChanged="txtFromDt_TextChanged" />
                                                        <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                            Display="None" ErrorMessage="Please Enter Holiday Date" ValidationGroup="submit"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter Holiday Date"
                                                            InvalidValueMessage="Holiday Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="submit" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="3"
                                                            ToolTip="Enter To Date"
                                                            OnTextChanged="txtToDt_TextChanged" AutoPostBack="true" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                                            Display="None" ErrorMessage="Please Enter Holiday Date" ValidationGroup="submit"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter Holiday Date"
                                                            InvalidValueMessage="Holiday Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="submit" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="row">
                                                                <div class="form-group col-sm-12">
                                                                    <div class="form-group col-sm-2">
                                                                        <label>College Name :</label>
                                                                    </div>
                                                                    <div class="form-group col-sm-4">
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                        <%--<div class="row">
                                                                <div class="form-group col-sm-12">
                                                                    <div class="form-group col-sm-2">
                                                                        <label>From Date :</label>
                                                                    </div>
                                                                    <div class="form-group col-sm-4">
                                                                      
                                                                    </div>
                                                                    <div class="form-group col-sm-2">
                                                                        <label>To Date :</label>
                                                                    </div>
                                                                    <div class="form-group col-sm-4">
                                                                      
                                                                    </div>
                                                                </div>
                                                            </div>--%>
                                        <div class="col-12">
                                            <asp:Panel ID="PanelList" runat="server">
                                                <asp:Repeater ID="lvEnCashApproval" runat="server">
                                                    <HeaderTemplate>
                                                        <div class="sub-heading">
	                                                        <h5>EnCashment List</h5>
                                                        </div>
                                                        <table id="table2" class="table table-striped table-bordered nowrap display" style="width:100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cHead" runat="server" Checked="true"
                                                                            onclick="totAllIDs(this)" />
                                                                    </th>
                                                                    <%-- <th>Sr No.
                                                                                        </th>--%>
                                                                    <th>College Name
                                                                    </th>
                                                                    <th>Emp. Name
                                                                    </th>
                                                                    <th>Emp Code
                                                                    </th>
                                                                    <th>RFID No.
                                                                    </th>
                                                                    <th>Apply Date
                                                                    </th>
                                                                    <th>Days
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chsr" runat="server" ToolTip='<%# Eval("SRNO") %>' />
                                                            </td>
                                                            <%-- <td>
                                                                                    <%# Eval("SRNO")%>
                                                                                </td>--%>
                                                            <td>
                                                                <%#Eval("COLLEGE_NAME")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("NAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("EMPCODE")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("RFIDNO")%>
                                                            </td>

                                                            <td>
                                                                <%#Eval("APPDT")%>
                                                            </td>
                                                            <td>
                                                                <%#Eval("TOTAL_DAYS")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody></table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <div id="DivNote" runat="server">
                                                    <div class="form-group col-sm-12">
                                                        <div class="text-center">
                                                            <p style="color: Red; font-weight: bold">
                                                                No Record Found..!!                                                                
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-primary" TabIndex="4"
                                                OnClick="btnApprove_Click" ToolTip="Click to Approve" />
                                            <asp:ValidationSummary ID="vlsApprove" runat="server" ValidationGroup="submit"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnApprove" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <%--</asp:Panel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function totAllIDs(cHead) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (cHead.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;

                    }

                }
            }
        }
    </script>
</asp:Content>
