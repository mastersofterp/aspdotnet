<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="quarter_vacant.aspx.cs"
    Inherits="ESTATE_Transaction_quarter_vacant" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CompareValue() {
            var Mat1 = document.getElementById('<%=txtallotedQty.ClientID%>').value;
            var Mat2 = document.getElementById('<%=txtshortQty.ClientID %>').value;
            if (parseInt(Mat2) > parseInt(Mat1)) {

                alert("Shorter Quantity greater than Allot Quantity")
                document.getElementById('<%=txtshortQty.ClientID %>').value = '';
                return false;
            }
            else {

            }
        }
    </script>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">VACATE QUARTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Vacate Quarter
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Resident Type<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlVacatortype" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                        CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVacatortype_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvVacator" runat="server" ControlToValidate="ddlVacatortype"
                                                        ErrorMessage="Please Select Vacator Type." SetFocusOnError="true" Display="None"
                                                       ValidationGroup="materialvacant"></asp:RequiredFieldValidator>
                                                    <asp:ValidationSummary ID="valsEmployee" runat="server" ValidationGroup="vacator" DisplayMode="List" ShowSummary="false"
                                                        ShowMessageBox="true" />
                                                    <asp:ValidationSummary ID="valsummaterial" runat="server" ValidationGroup="materialvacant" DisplayMode="List" ShowSummary="false"
                                                        ShowMessageBox="true" />
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Name of the Vacator<span style="color: red;">*</span>:</label>
                                                    <asp:DropDownList ID="ddlvacatorName" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlvacatorName_SelectedIndexChanged" TabIndex="2">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvvacatorName" runat="server" ControlToValidate="ddlvacatorName"
                                                        ErrorMessage="Please Select Name of Vacator." SetFocusOnError="true" Display="None"
                                                        ValidationGroup="vacator"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Quarter Type :</label>
                                                    <asp:DropDownList ID="ddlquarterType" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="3">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvquartertype" runat="server" ControlToValidate="ddlquarterType"
                                                        ErrorMessage="Please Select Quarter Type." SetFocusOnError="true" Display="None"
                                                        ValidationGroup="vacator"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Quarter No :</label>
                                                    <asp:DropDownList ID="ddlquarterNo" runat="server" AppendDataBoundItems="true"
                                                        CssClass="form-control" TabIndex="4">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlquarterNo"
                                                        ErrorMessage="Please Select Quarter No." SetFocusOnError="true" Display="None"
                                                        ValidationGroup="vacator"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-info"></div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Office Order No<span style="color: red;">*</span> :</label>
                                                        <asp:TextBox ID="txtvacationOrdNo" runat="server" CssClass="form-control" TabIndex="5" MaxLength="30"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtvacationOrdNo"
                                                            FilterType="Custom, LowercaseLetters,  Numbers, UppercaseLetters" ValidChars="-/\  ">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Date of Vacation<span style="color: red;">*</span>:</label>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtDtOfVacant" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvdtvacant" runat="server" ControlToValidate="txtDtOfVacant" SetFocusOnError="true"
                                                                ErrorMessage="Please Select Vacant date." Display="None" ValidationGroup="vacator"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ControlToCompare="txtoffceOrderDate" ControlToValidate="txtDtOfVacant" Display="None"
                                                                ErrorMessage="Office Order Date Should be less than Vacation Date."
                                                                ID="CompareValidator1" Operator="GreaterThan" Type="Date" runat="server" ValidationGroup="vacator" />
                                                            <ajaxToolKit:CalendarExtender ID="calextorderdt" runat="server"
                                                                TargetControlID="txtDtOfVacant" PopupButtonID="imgorderdt"
                                                                Enabled="True" Format="dd/MM/yyyy">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="msedatebirth1" runat="server" TargetControlID="txtDtOfVacant"
                                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" />
                                                            <div class="input-group-addon">
                                                                <%--<asp:Image ID="imgorderdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                                 <asp:ImageButton runat="Server" ID="imgorderdt" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Office Order Date :</label>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtoffceOrderDate" runat="server" CssClass="form-control" TabIndex="7"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtenderdt" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtoffceOrderDate" PopupButtonID="imgorddtd"
                                                                Enabled="True">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" TargetControlID="txtoffceOrderDate"
                                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" />
                                                            <div class="input-group-addon">
                                                               <%-- <asp:Image ID="imgorddtd" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                                               <asp:ImageButton runat="Server" ID="imgorddtd" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div>
                                                <div class="row" id="DivchlsubmsnMaterial" runat="server" visible="false">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-info"></div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <asp:CheckBox ID="chlsubmsnMaterial" runat="server" TabIndex="8"
                                                            Text="Submission of Shortage Assets?"
                                                            OnCheckedChanged="chlsubmsnMaterial_CheckedChanged" AutoPostBack="true" Visible="false" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="fdsMaterial" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-info"></div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label>Enter the Name of Shortage&nbsp; Assets (if any)</label>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Assets :</label>
                                                        <asp:DropDownList ID="ddlMaterial" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                            CssClass="form-control" OnSelectedIndexChanged="ddlMaterial_SelectedIndexChanged" TabIndex="9">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMaterial"
                                                            ErrorMessage="Please Select Material." Display="None" ValidationGroup="materialvacant"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Alloted Qty<span style="color: red;">*</span>:</label>
                                                        <asp:TextBox ID="txtallotedQty" runat="server" CssClass="form-control" TabIndex="10"
                                                            ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtallotedQty"
                                                            ErrorMessage="Please Select Allot Quantity." Display="None" ValidationGroup="materialvacant"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Short Qty<span style="color: red;">*</span>:</label>
                                                        <asp:TextBox ID="txtshortQty" runat="server" CssClass="form-control" TabIndex="11" AutoPostBack="false"
                                                            MaxLength="1"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQuantity" runat="server" TargetControlID="txtshortQty"
                                                            FilterType="Numbers">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtshortQty"
                                                            ErrorMessage="Please Select Short Quantity." Display="None" ValidationGroup="materialvacant"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <label>Fine in Rs<span style="color: red;">*</span>:</label>
                                                        <asp:TextBox ID="txtfine" runat="server" CssClass="form-control" onClick="CompareValue();" MaxLength="10"
                                                            TabIndex="12"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeFine" runat="server" TargetControlID="txtfine"
                                                            ValidChars="0123456789.">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtfine"
                                                            ErrorMessage="Please Enter Fine in Ruppees Short Quantity." Display="None" ValidationGroup="materialvacant"
                                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <label>Remark :</label>
                                                        <asp:TextBox ID="txtfineRemark" runat="server" CssClass="form-control" TabIndex="13"
                                                            onClick="CompareValue();" TextMode="MultiLine"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <br />
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Add Assets" CssClass="btn btn-primary" TabIndex="14"
                                                            OnClick="btnSubmit_Click" ValidationGroup="vacator" />
                                                        <asp:Button ID="btnClear" runat="server" Text="Reset" OnClick="btnClear_Click" CssClass="btn btn-primary"
                                                            TabIndex="15" />
                                                    </div>
                                                </div>

                                                <div class="row" id="fldshortmat" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="table-responsive">
                                                            <asp:Panel ID="pnlshortage" runat="server">
                                                                <asp:Repeater ID="rpt_shortMaterials" runat="server"
                                                                    OnItemCommand="rpt_shortMaterials_ItemCommand">
                                                                    <HeaderTemplate>
                                                                        <table class="table table-bordered table-hover">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>DELETE
                                                                                    </th>
                                                                                    <th>ASSETS
                                                                                    </th>
                                                                                    <th>ALLOT QTY
                                                                                    </th>
                                                                                    <th>SHORT QTY
                                                                                    </th>
                                                                                    <th>FINE
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton_Edit" runat="server" CommandArgument='<%#Eval("IDNO")%>'
                                                                                    CommandName="Delete" ImageUrl="../../IMAGES/Delete.gif" ToolTip="Update this  Investigation" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblmaterialName" runat="server" Text=' <%#Eval("MNAME")%>' ToolTip='<%#Eval("MATERIAL_ID")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblallotQty" runat="server" Text=' <%#Eval("ALLOT_QTY")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblshorQty" runat="server" Text=' <%#Eval("SHORT_QTY")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblfine" runat="server" Text=' <%#Eval("FINE")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                    </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-info"></div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        <div class="text-center">
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="16" OnClick="btnSave_Click"
                                                                ValidationGroup="vacator"  CssClass="btn btn-primary"/>
                                                          
                                                            <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="btn btn-warning" TabIndex="17"
                                                                OnClick="btnCancel_Click" />
                                                        </div>
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
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>

    <%--<script type="text/javascript" language="javascript">
        
            function OnChangeOfTxtAmount() 
            {
        
            var vartxtAmount1 = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtallotedQty').value);
            var vartxtAmount2 = parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtshortQty').value);
       
            
            alert(vartxtAmount1);
            alert(vartxtAmount2);
           
            
            if ((vartxtAmount1 > vartxtAmount2)
                alert('value exceeded');
           }
         </script>  --%>
</asp:Content>

