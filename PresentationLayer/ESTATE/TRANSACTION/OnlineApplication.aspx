<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlineApplication.aspx.cs"
    Inherits="ESTATE_Transaction_OnlineApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlge" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ONLINE APPLICATION FORM</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Applicant Details
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <div id="tdApp1" runat="server">
                                                        <label>Select Applicant<label id="tdApp2" runat="server">:</label></label>
                                                    </div>
                                                    <div id="tdApp3" runat="server">
                                                        <asp:DropDownList ID="ddlApplicant" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlApplicant_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <div id="tdApp4" runat="server">
                                                        <label>Application Date<label id="tdApp5" runat="server">:</label></label>
                                                    </div>
                                                    <div id="tdApp6" runat="server">
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtApplicationDate" runat="server" align="left" CssClass="form-control" TabIndex="2" Enabled="false"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtApplicationDate" PopupButtonID="imgApp" Enabled="True">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtApplicationDate"
                                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="True" />
                                                            <div class="input-group-addon">
                                                              <%--  <asp:Image ID="imgApp" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" Visible="false" />--%>
                                                                  <asp:ImageButton runat="Server" ID="imgApp" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Applicant Name :</label>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TabIndex="3" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Designation :</label>
                                                    <asp:TextBox ID="txtDesig" runat="server" CssClass="form-control" TabIndex="4" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Department :</label>
                                                    <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" TabIndex="5" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Date of Joining :</label>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtdatejoing" runat="server" align="left" CssClass="form-control" TabIndex="6" ReadOnly="true"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtdatejoing" PopupButtonID="imgcal" Enabled="false">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtdatejoing"
                                                            Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True" />
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgcal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4" runat="server" style="display: none">
                                                    <label>Pay Band :</label>
                                                    <asp:TextBox ID="txtPayBand" runat="server" CssClass="form-control" TabIndex="7" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Email ID :</label>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TabIndex="8" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Quarter Type<span style="color: red;"></span>:</label>
                                                    <asp:DropDownList ID="ddlquartertype" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="9">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                   <%-- <asp:RequiredFieldValidator ID="rfvQType" runat="server" ControlToValidate="ddlquartertype" InitialValue="0"
                                                        ErrorMessage="Please Select Quarter Type." ValidationGroup="Submit" Display="None"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Contact No :</label>
                                                    <asp:TextBox ID="txtContNo" runat="server" CssClass="form-control" TabIndex="10" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Remark :</label>
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TabIndex="11" TextMode="MultiLine"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>Permanent Address :</label>
                                                    <asp:TextBox ID="txtPAdd" runat="server" CssClass="form-control" TabIndex="12" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <label>No. of Family Members<span style="color: red;">*</span>:</label>
                                                    <asp:TextBox ID="txtMembers" runat="server" CssClass="form-control" TabIndex="13" MaxLength="2" ValidationGroup="Submit"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMem" runat="server" ControlToValidate="txtMembers" ValidationGroup="Submit"
                                                        Display="None" ErrorMessage="Please Enter No. of Family Members.">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtMembers"
                                                        FilterType="Numbers">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="text-center">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Submit"
                                                        TabIndex="14" CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="btnReset_Click" TabIndex="15" />&nbsp  
                                                    <asp:ValidationSummary ID="vsApplication" runat="server" ValidationGroup="Submit" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="form-group row table-responsive">
                                        <asp:ListView ID="lvOnApp" runat="server">
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <h4 class="box-title">Application Entry List</h4>
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>EDIT
                                                                </th>
                                                                <th>EMPLOYEE NAME
                                                                </th>
                                                                <th>QUARTER APPLIED
                                                                </th>
                                                                <th>TOTAL MEMBERS
                                                                </th>
                                                                <th>REMARK
                                                                </th>
                                                                <th>STATUS
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("APPID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                            OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("QUARTER_TYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TOTAL_MEMBERS")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMARK")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STATUS").ToString() == "0" ? "FORWARD" : "COMPLETE"%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>                                           
                                        </asp:ListView>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="text-center">
                                        <div class="vista-grid_datapager">
                                            <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender" PagedControlID="lvOnApp" PageSize="10">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="&lt;&lt;" PreviousPageText="&lt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="true" />
                                                    <asp:NumericPagerField ButtonCount="10" ButtonType="Link" CurrentPageLabelCssClass="Current" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" LastPageText="&gt;&gt;" NextPageText="&gt;" RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowLastPageButton="true" ShowNextPageButton="true" ShowPreviousPageButton="false" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>



    <script type="text/javascript">
        function GetRetemployeeNo(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtSearch').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_hfInvNo').value = idno[1];
        }

        function CheckAlphabet(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>
</asp:Content>


