<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="quarter_allotment.aspx.cs"
    Inherits="ESTATE_Transaction_quarter_allotment" Title="" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table cellpadding="2" cellspacing="2" width="100%">

        <tr>
            <td>Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="updQtr" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">QUARTER ALLOTMENT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Quarter Allotment
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row" id="pnlDetails" runat="server" visible="false">
                                                <div class="col-md-12">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-info">
                                                            <div class="panel panel-heading">Applicant Details</div>
                                                            <div class="panel panel-body">
                                                                <div class="form-group row">
                                                                    <div class="col-md-6">
                                                                        <label>Applicant Name :</label>
                                                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <label>Designation :</label>
                                                                        <asp:Label ID="lblDesig" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-6">
                                                                        <label>Department :</label>
                                                                        <asp:Label ID="lblDepart" runat="server"></asp:Label>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <label>Quarter Type Applied :</label>
                                                                        <asp:Label ID="lblQrtTyp" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <div class="panel panel-info">
                                                            <div class="panel panel-heading">Quarter Details</div>
                                                            <div class="panel panel-body">
                                                                <div class="form-group row">
                                                                    <div class="col-md-4">
                                                                        <label>Energy Consumer Number :</label>
                                                                        <asp:DropDownList ID="ddlquarterno" runat="server" AppendDataBoundItems="true" AutoPostBack="false" CssClass="form-control" TabIndex="4">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvQNo" runat="server" ControlToValidate="ddlquarterno" ErrorMessage="Please Select Quarter No. "
                                                                            ValidationGroup="addmeter" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        <asp:ValidationSummary ID="vsumaddmeter" runat="server" ValidationGroup="addmeter" ShowMessageBox="true" ShowSummary="false" />
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Allot Order No<span style="color: red;">*</span> :</label>
                                                                        <asp:TextBox ID="txtallotorderno" runat="server" CssClass="form-control" MaxLength="30" TabIndex="5"></asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtallotorderno"
                                                                            FilterType="Custom, LowercaseLetters,  Numbers, UppercaseLetters" ValidChars="/-()\">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="rfvallotno" runat="server" ControlToValidate="txtallotorderno"
                                                                            ErrorMessage="Please Enter Allot Order No." ValidationGroup="allotment"
                                                                            Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Office Order Date<span style="color: red;">*</span> :</label>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <asp:Image ID="imgorderdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                            </div>
                                                                            <asp:TextBox ID="txtorderdate" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfcofficeOrderdt" runat="server" ControlToValidate="txtorderdate"
                                                                                ErrorMessage="Please Select Office Order Date " ValidationGroup="allotment" Display="None"
                                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                            <ajaxToolKit:CalendarExtender ID="calextorderdt" runat="server" Format="dd/MM/yyyy"
                                                                                TargetControlID="txtorderdate" PopupButtonID="imgorderdt" Enabled="True">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <ajaxToolKit:MaskedEditExtender ID="imgorderdt2" runat="server" TargetControlID="txtorderdate"
                                                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                CultureTimePlaceholder="" Enabled="True" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-4">
                                                                        <label>Allotment Date<span style="color: red;">*</span> :</label>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <asp:Image ID="imgallotdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                            </div>
                                                                            <asp:TextBox ID="txtallotmentdate" runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvallotmentDt" runat="server" ControlToValidate="txtallotmentdate"
                                                                                ErrorMessage="Please Select Allotment Date " ValidationGroup="allotment" Display="None"
                                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                            <ajaxToolKit:CalendarExtender ID="calextenderallotdt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtallotmentdate" PopupButtonID="imgallotdt" Enabled="True">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" TargetControlID="txtallotmentdate" Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                CultureTimePlaceholder="" Enabled="True" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <label>Occupied Date :</label>
                                                                        <div class="input-group date">
                                                                            <div class="input-group-addon">
                                                                                <asp:Image ID="imgOccDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                            </div>
                                                                            <asp:TextBox ID="txtOccuDate" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                                                            <%--  <asp:RequiredFieldValidator ID="rfvOccDt" runat="server" ControlToValidate="txtOccuDate" ErrorMessage="Please Select Allotment Date "  ValidationGroup="allotment" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOccuDate" PopupButtonID="imgOccDt" Enabled="True">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtOccuDate" Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                CultureTimePlaceholder="" Enabled="True" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-12 text-center">
                                                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click"
                                                                            ValidationGroup="allotment" TabIndex="9" />
                                                                        <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="10" />
                                                                        <asp:TextBox ID="txtquarterRent" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="text-center">
                                                        <asp:Button ID="btnReprt" runat="server" Text="Waiting List Report" OnClick="btnReprt_Click" ToolTip="Waiting List of Applicants." CssClass="btn btn-info" TabIndex="1" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ID="imgExcel" runat="server" ToolTip="Export to excel" ImageUrl="~/IMAGES/excel.jpg" Height="35px" Width="35px" OnClick="imgExcel_Click" TabIndex="2" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:Repeater ID="RepApplicant" runat="server">
                                                            <HeaderTemplate>
                                                                <table id="tbitems" class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>APPLICANT 
                                                                            </th>
                                                                            <th>QUATER TYPE
                                                                            </th>
                                                                            <th>NO. OF MEMBERS
                                                                            </th>
                                                                            <th>REMARK
                                                                            </th>
                                                                            <th>STATUS
                                                                            </th>
                                                                            <th>VIEW
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                    </tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>' ToolTip='<%#Eval("EMPID")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblQrtTyp" runat="server" Text=' <%#Eval("QUARTER_TYPE")%>' ToolTip='<%#Eval("QTNO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTotMem" runat="server" Text=' <%#Eval("TOTAL_MEMBERS")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblRemark" runat="server" Text=' <%#Eval("REMARK")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" Text=' <%#Eval("STATUS").ToString() == "0" ? "WAITING" : "ALLOTED"%>' ToolTip='<%#Eval("STATUS") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnDetail" runat="server" Text="Details" ToolTip='<%#Eval("STATUS") %>'
                                                                            CommandArgument='<%#Eval("EMPID") %>' OnClick="btnDetail_Click" CssClass="btn btn-primary" TabIndex="3" />
                                                                        <%--Enabled='<%#Eval("STATUS").ToString() == "0" ? true : false %>'--%>
                                                    
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody>
                                                            </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <div style="width: 60%; padding-left: 10px;" id="divGrid" runat="server" visible="false">
                                                        <table cellpadding="2" cellspacing="2" width="70%">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gvWaitingList" CssClass="vista-grid" HeaderStyle-BackColor="ActiveBorder"
                                                                        runat="server" Height="10px" OnRowDataBound="gvWaitingList_RowDataBound" EnableEventValidation="false">
                                                                    </asp:GridView>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="imgExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

