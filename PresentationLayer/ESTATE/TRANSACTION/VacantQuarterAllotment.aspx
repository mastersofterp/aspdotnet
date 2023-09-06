<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VacantQuarterAllotment.aspx.cs" Inherits="ESTATE_Transaction_VacantQuarterAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <asp:UpdatePanel ID="updQtr" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">QUARTER ALLOTMENT </h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="col-md-12">
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlrdb" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            View Allotted List
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-6">
                                                    <asp:CheckBox ID="chkList" runat="server" AutoPostBack="true" Checked="false" Text="View Quarter Allotted List"
                                                        OnCheckedChanged="chkList_CheckedChanged"  />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div id="pnlDetails" runat="server" visible="false">
                                <div class="col-md-12">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Quarter Details
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Quarter Type<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlQrtType" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                        TabIndex="2">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvQrtType" runat="server" ControlToValidate="ddlQrtType"
                                                        ErrorMessage="Please Select Quarter Type" InitialValue="0" ValidationGroup="allotment" Display="None"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Energy Consumer Number<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlQrtName" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                        TabIndex="3">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvQrtName" runat="server" ControlToValidate="ddlQrtName"
                                                        ErrorMessage="Please Select Quarter Name" InitialValue="0" ValidationGroup="allotment"
                                                        Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Applicant Type :</label>
                                                    <asp:RadioButtonList ID="rdbApplicantType" runat="server" AutoPostBack="true" TabIndex="4"
                                                        OnSelectedIndexChanged="rdbApplicantType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Selected="True">All Emp</asp:ListItem>
                                                        <asp:ListItem Value="1" >Applicant</asp:ListItem>
                                                        <asp:ListItem Value="2">Temporary Emp</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="form-group row">

                                                <%--Added on 21/03/2022--%>

                                                <div class="form-group col-md-4">
                                                   <label>Department :</label>
                                                   <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  Enabled="true" Visible="true"
                                                        CssClass="form-control" TabIndex="15" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                                   <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                   </asp:DropDownList>                                                                        
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Employee<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" CssClass="form-control" TabIndex="5">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmployee"
                                                        ErrorMessage="Please Select Employee" InitialValue="0" ValidationGroup="allotment" Display="None"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>



                                                <%--Added on 21/03/2022--%>

                                                <%--<div class="form-group col-md-4">
                                                   <label>Department :</label>
                                                   <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  Enabled="true" Visible="true"
                                                        CssClass="form-control" TabIndex="15" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                                   <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                   </asp:DropDownList>                                                                        
                                                </div>--%>




                                                <div class="col-md-4">
                                                    <label>Search Employee<span style="color: red;"></span>:</label>
                                                    <asp:TextBox ID="txtEmployee" runat="server" CssClass="form-control" TabIndex="6"
                                                        placeholder="Enter Character to Search" ToolTip="Enter Employee Name"
                                                        OnTextChanged="txtEmployee_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:HiddenField ID="hfEmployeeId" runat="server" />
                                                    <ajaxToolKit:AutoCompleteExtender ID="autAgainstAc" runat="server" TargetControlID="txtEmployee"
                                                        MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                        ServiceMethod="GetEmployeeName" OnClientShowing="clientShowing" OnClientItemSelected="GetEmpName">
                                                    </ajaxToolKit:AutoCompleteExtender>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Designation :</label>
                                                    <asp:Label ID="lblDesig" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnDesig" runat="server" />
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Department :</label>
                                                    <asp:Label ID="lblDepart" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdndept" runat="server" />
                                                    <asp:ValidationSummary ID="vsumaddmeter" runat="server" ValidationGroup="allotment" ShowMessageBox="true" ShowSummary="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                Allotment Details
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Allot Order No.<span style="color: red;">*</span>:</label>
                                                        <asp:TextBox ID="txtallotorderno" runat="server" CssClass="form-control" MaxLength="30" TabIndex="7"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtallotorderno"
                                                            FilterType="Custom, LowercaseLetters,  Numbers, UppercaseLetters" ValidChars="/-()\&">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfvallotno" runat="server" ControlToValidate="txtallotorderno"
                                                            ErrorMessage="Please Enter Allot Order No." ValidationGroup="allotment" Display="None"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Office Order Date<span style="color: red;">*</span>:</label>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtorderdate" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
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
                                                            <div class="input-group-addon">
                                                                <asp:ImageButton runat="Server" ID="imgorderdt" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                                <%--<asp:Image ID="imgorderdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Allotment Date<span style="color: red;">*</span>:</label>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtallotmentdate" runat="server" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvallotmentDt" runat="server" ControlToValidate="txtallotmentdate"
                                                                ErrorMessage="Please Select Allotment Date " ValidationGroup="allotment" Display="None"
                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:CalendarExtender ID="calextenderallotdt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtallotmentdate" PopupButtonID="imgallotdt" Enabled="True">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" TargetControlID="txtallotmentdate" Mask="99/99/9999" MaskType="Date"
                                                                AcceptAMPM="True" ErrorTooltipEnabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" />
                                                            <div class="input-group-addon">
                                                              <%--  <asp:Image ID="imgallotdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                                <asp:ImageButton runat="Server" ID="imgallotdt" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Occupied Date :</label>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtOccuDate" runat="server" CssClass="form-control" TabIndex="10"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOccuDate"
                                                                PopupButtonID="imgOccDt" Enabled="True">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtOccuDate" Mask="99/99/9999"
                                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDecimalPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" CultureThousandsPlaceholder="" />
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgOccDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>No Of Family Members :</label>
                                                        <asp:TextBox ID="txtNoMembers" runat="server" MaxLength="2" CssClass="form-control" TabIndex="11"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMembers" runat="server" TargetControlID="txtNoMembers"
                                                            FilterType="Numbers">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <label>Remark<%--<span style="color: red;">*</span>--%>:</label>
                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="12"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvtxtpermanentaddress" runat="server" ControlToValidate="txtpermanentaddress"
                                                                  ErrorMessage="Please Enter Permanent Address " Display="None" ValidationGroup="consumer" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        <div class="text-center">
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" ValidationGroup="allotment"
                                                                TabIndex="12" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="13" />
                                                            <asp:TextBox ID="txtquarterRent" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="text-center">
                                    <asp:Button ID="btnReprt" runat="server" Text="Waiting List Report" CssClass="btn btn-info" Visible="false" OnClick="btnReprt_Click"
                                        ToolTip="Waiting List of Applicants." />
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:Repeater ID="RepApplicant" runat="server" Visible="false">
                                        <HeaderTemplate>
                                            <table id="tbitems" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>QUARTER TYPE
                                                        </th>
                                                        <th>BLOCK NAME
                                                        </th>
                                                        <th>QUARTER NUMBER
                                                        </th>
                                                        <th>ENERGY CONSUMER NUMBER
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
                                                    <asp:Label ID="lblQrtType" runat="server" Text='<%#Eval("QUARTER_TYPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBlockName" runat="server" Text='<%#Eval("BLOCKNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQrtNumber" runat="server" Text='<%#Eval("QUARTER_NO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQrtName" runat="server" Text='<%#Eval("QRTNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDetail" runat="server" Text="Allot" CommandArgument='<%#Eval("IDNO") %>' CssClass="btn btn-primary"
                                                        OnClick="btnDetail_Click" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </table>
                                        </FooterTemplate>
                                    </asp:Repeater>

                                    <asp:Repeater ID="repAllotted" runat="server" Visible="false">
                                        <HeaderTemplate>
                                            <table id="tbitems" class="table table-bordered table-hover">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>NAME
                                                        </th>
                                                        <th>QUARTER TYPE
                                                        </th>
                                                        <th>BLOCK NAME
                                                        </th>
                                                        <th>QUARTER NUMBER
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
                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQrtType" runat="server" Text='<%#Eval("QUARTER_TYPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBlockName" runat="server" Text='<%#Eval("BLOCKNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQrtNumber" runat="server" Text='<%#Eval("QRTNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblQrtName" runat="server" Text='<%#Eval("STATUS")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnView" runat="server" Text="View " CommandArgument='<%#Eval("QA_ID") %>' CssClass="btn btn-primary"
                                                        CommandName='<%#Eval("EMPTYPE_ID") %>' OnClick="btnView_Click" />

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
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function GetEmpName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtEmployee').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfEmployeeId').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployee').value = Name[0];
            //document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployee').onchange();




        }

    </script>

</asp:Content>

