<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="OnlineAdmission_Groupwise.aspx.cs" Inherits="ADMINISTRATION_OnlineAdmission_Groupwise" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        #ctl00_ContentPlaceHolder1_pnlListView .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .lblappfees {
            font-weight: bold;
        }
    </style>


    <asp:UpdatePanel ID="updNotify" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">


                            <div>
                                <asp:UpdateProgress ID="UpdConfig" runat="server" AssociatedUpdatePanelID="updBatch"
                                    DynamicLayout="true" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <div id="preloader">
                                            <div id="loader-img">
                                                <div id="loader">
                                                </div>
                                                <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <asp:UpdatePanel ID="updBatch" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <asp:RadioButtonList ID="rdobtnnri" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdobtnnri_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True">INDIAN &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="1"> NRI  / FOREIGN NATIONAL &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>

                                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                    Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Admission Batch."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>

                                                    <asp:Label ID="blDYddlAdmType" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlAdmType" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmType_SelectedIndexChanged" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmType"
                                                    Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Admission Type."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Program Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlProgramme" runat="server" TabIndex="4" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramme_SelectedIndexChanged" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvProgrammeType" runat="server" ControlToValidate="ddlProgramme"
                                                    Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Programme Type."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divSchool" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSchool" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSchool" runat="server" ControlToValidate="ddlSchool"
                                                    Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select School." Visible="false"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divphd" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Admission Session</label>
                                                </div>
                                                <asp:DropDownList ID="ddlphd" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="false" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Autumn</asp:ListItem>
                                                    <asp:ListItem Value="2">Winter</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlphd" runat="server" ControlToValidate="ddlphd"
                                                    Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Admission Session."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>

                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>

                                                </div>

                                                <asp:ListBox ID="lstDegree" runat="server" CssClass="form-control multi-select-demo" AutoPostBack="true"
                                                    AppendDataBoundItems="true" SelectionMode="Multiple" TabIndex="7" OnSelectedIndexChanged="lstbxDegree_SelectedIndexChanged"></asp:ListBox>
                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="lstDegree"
                                                    Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Degree."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>

                                                </div>

                                                <asp:ListBox ID="lstbranch" runat="server" CssClass="form-control multi-select-demo"
                                                    AppendDataBoundItems="true" SelectionMode="Multiple" TabIndex="7"></asp:ListBox>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="lstbranch"
                                                    Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">

                                                    <asp:Label ID="lblgroup" runat="server" Font-Bold="true">Group</asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlgroup" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvgroup" runat="server" ControlToValidate="ddlgroup"
                                                    Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select School." Visible="false"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Start Date</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon" id="imgStartDate">
                                                        <div class="fa fa-calendar"></div>
                                                    </div>
                                                    <asp:TextBox ID="txtStartDate" runat="server" TabIndex="7" CssClass="form-control" ToolTip="Please Enter Start Date"></asp:TextBox>

                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtStartDate" PopupButtonID="imgStartDate" Enabled="true">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <ajaxToolKit:MaskedEditExtender ID="meStartdate" runat="server" TargetControlID="txtStartDate"
                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="rfvStartdate" runat="server" ControlToValidate="txtStartDate"
                                                        Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Start Date."></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Start Time</label>
                                                </div>
                                                <asp:TextBox ID="txtStartTime" runat="server" TabIndex="8" CssClass="form-control" ToolTip="Please Enter Start Time."></asp:TextBox>

                                                <ajaxToolKit:MaskedEditExtender ID="meStarttime" runat="server" TargetControlID="txtStartTime"
                                                    Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                    MaskType="Time" />
                                                <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime"
                                                    Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Start Time"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revstarttime" runat="server" ControlToValidate="txtStartTime"
                                                    ValidationGroup="Submit" ErrorMessage="Please Enter Valid Start Time" ValidationExpression="(((0[1-9])|(1[0-2])):([0-5])([0-9])\s(A|P)M)">

                                                </asp:RegularExpressionValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>End Date</label>
                                                </div>
                                                <div class="input-group">
                                                    <div class="input-group-addon" id="imgEndDate">
                                                        <div class="fa fa-calendar text-blue"></div>
                                                    </div>
                                                    <asp:TextBox ID="txtEndDate" runat="server" TabIndex="9" ToolTip="Please Enter End Date." CssClass="form-control"></asp:TextBox>

                                                    <ajaxToolKit:CalendarExtender ID="ceEndDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtEndDate" PopupButtonID="imgEndDate" Enabled="true">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <ajaxToolKit:MaskedEditExtender ID="meEndDate" runat="server" TargetControlID="txtEndDate"
                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="rfvEnddate" runat="server" ControlToValidate="txtEndDate"
                                                        Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter End Date"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>End Time</label>
                                                </div>
                                                <asp:TextBox ID="txtEndTime" runat="server" TabIndex="10" CssClass="form-control" ToolTip="Please Enter End Time."></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="meEndTime" runat="server" TargetControlID="txtEndTime"
                                                    Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                    MaskType="Time" />
                                                <asp:RegularExpressionValidator ID="revendtime" runat="server" ControlToValidate="txtEndTime"
                                                    ValidationGroup="Submit" ErrorMessage="Please Enter Valid End Time" ValidationExpression="(((0[1-9])|(1[0-2])):([0-5])([0-9])\s(A|P)M)">
                                                </asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ControlToValidate="txtEndTime"
                                                    Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter End Time."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblapp" runat="server" Text="Application Fee(INR)" CssClass="lblappfees"></asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtApplicationFee" runat="server" onkeyup="IsNumeric(this);" CssClass="form-control" TabIndex="11" MaxLength="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvApplication" runat="server" ControlToValidate="txtApplicationFee"
                                                    Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Application Fee."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divinr" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Equvivalent To INR</label>
                                                </div>
                                                <asp:TextBox ID="txteqvinr" runat="server" onkeyup="IsNumeric(this);" CssClass="form-control" TabIndex="11" MaxLength="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfveqvinr" runat="server" ControlToValidate="txteqvinr"
                                                    Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Equvivalent INR."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Age</label>
                                                </div>
                                                <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" TabIndex="12" ToolTip="Please Enter Age." onkeyup="IsNumeric(this);" MaxLength="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAge"
                                                    Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Age."></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Details/Remarks</label>
                                                </div>
                                                <asp:TextBox ID="txtDetails" runat="server" TabIndex="12" CssClass="form-control"></asp:TextBox>

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Status</label>
                                                    <div style="margin-top: 10px">
                                                        <asp:CheckBox ID="chkStatus" runat="server" Text="&nbsp;Active" TextAlign="Right" ToolTip="Please Select Status." />

                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="Submit" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                        <asp:ValidationSummary ID="vsSubmit" runat="server" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" ValidationGroup="Submit" />
                                    </div>

                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlListView" runat="server">
                                            <asp:ListView ID="lvConfiguration" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Notification Details</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divadmissionlist">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Start Date
                                                                </th>
                                                                <th>Start Time
                                                                </th>
                                                                <th>End Date
                                                                </th>
                                                                <th>End Time
                                                                </th>
                                                                <th>Adm.Batch
                                                                </th>
                                                                <th>Application Fee
                                                                </th>
                                                                <th>Group
                                                                </th>
                                                                <th>IS NRI
                                                                </th>
                                                                <th>
                                                                    Admission Session
                                                                </th>
                                                                <%--<th>College Name
                                                                </th>--%>
                                                                 <th>Branch
                                                                </th>

                                                                <th>Age
                                                                </th>
                                                                <th>Details
                                                                </th>
                                                                <th>Active Status
                                                                </th>

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td style="text-align: center;">
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                                CommandArgument='<%# Eval("CONFIGID")%>' OnClick="btnEdit_Click" AlternateText="Edit Record" ToolTip="Edit Record" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("STARTDATE") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STARTTIME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ENDDATE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ENDTIME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("BATCH")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FEES")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("GROUP_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("IS_NRI")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ADMISSION_SESSION")%>
                                                        </td>

                                                        <td>
                                                            <%# Eval("PROGRAM").ToString().Replace(",", "<br />")%>
                                                           <%-- <%# Eval("PROGRAM")%>--%>
                                                        </td>

                                                        <td>
                                                            <%#Eval("AGE") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("DETAILS")%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# (Convert.ToString(Eval("ACTIVE_STATUS") )== "Active" ?System.Drawing.Color.Green:System.Drawing.Color.Red)%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

    <script type="text/javascript">
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                });
            });
        });
    </script>
</asp:Content>
