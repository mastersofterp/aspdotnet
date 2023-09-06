<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlineAdmission.aspx.cs" Inherits="ADMINISTRATION_OnlineAdmission" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlListView .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        }
    </style>

    <%--    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updNotify"
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
    </div>--%>

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
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tab1">Other Admission Config</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tab2">NRI Admission Config</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab1">
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
                                                        <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Category of Form</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlCategory" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">General (INDIAN)</asp:ListItem>
                                                                <asp:ListItem Value="2">NRI / Sponsored / Foreign National</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                                                                Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Category"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Admission Batch</label>--%>
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
                                                                <%--<label>Admission Type</label>--%>
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
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Degree</label>--%>
                                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Degree."></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <%--<label>Branch</label>--%>
                                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="6" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                                Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>--%>
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

                                                         <asp:RegularExpressionValidator ID="revstarttime" runat="server" ControlToValidate="txtStartTime" Display="None"
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
                                                                <asp:RegularExpressionValidator ID="revendtime" runat="server" ControlToValidate="txtEndTime" Display="None"
                                                                ValidationGroup="Submit" ErrorMessage="Please Enter Valid End Time" ValidationExpression="(((0[1-9])|(1[0-2])):([0-5])([0-9])\s(A|P)M)">
                                                            </asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ControlToValidate="txtEndTime"
                                                                Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter End Time."></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Application Fee</label>
                                                            </div>
                                                            <asp:TextBox ID="txtApplicationFee" runat="server" onkeyup="IsNumeric(this);" CssClass="form-control" TabIndex="11" MaxLength="5"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvApplication" runat="server" ControlToValidate="txtApplicationFee"
                                                                Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Application Fee."></asp:RequiredFieldValidator>
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
                                                            <%--   <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Details"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Status</label>
                                                                <div style="margin-top: 10px">
                                                                    <asp:CheckBox ID="chkStatus" runat="server" Text="&nbsp;Active" TextAlign="Right" ToolTip="Please Select Status." />
                                                                    <%--<asp:RegularExpressionValidator ID="rgev" runat="server" ControlToValidate="chkStatus" Display="None"></asp:RegularExpressionValidator>--%>
                                                                    <%-- <asp:CheckBoxList ID="chkStatus" runat="server" TextAlign="Right" AppendDataBoundItems="true" RepeatDirection="Horizontal" RepeatColumns="2">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="2">In Active</asp:ListItem>
                                            </asp:CheckBoxList>--%>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="chkStatus" ErrorMessage="Please Select Status." ToolTip="Please Select Status"
                                                InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
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
                                                                            <th>College Name
                                                                            </th>
                                                                            <th>Degree
                                                                            </th>
                                                                            <th>Branch
                                                                            </th>
                                                                            <th>Fee
                                                                            </th>
                                                                            <th>Age
                                                                            </th>
                                                                            <th>Details
                                                                            </th>
                                                                            <th>Admission Type
                                                                            </th>
                                                                            <th>Active Status
                                                                            </th>
                                                                            <%--<th>
                                                            Form Categort
                                                        </th>--%>
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
                                                                        <%# Eval("COLLEGE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DEGREENAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BRANCHNAME")%>
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("FEES")%>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("AGE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DETAILS")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ADM_TYPE")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# (Convert.ToString(Eval("ACTIVE_STATUS") )== "Active" ?System.Drawing.Color.Green:System.Drawing.Color.Red)%>'></asp:Label>
                                                                    </td>
                                                                    <%-- <td>
                                                    <%# Eval("FORM_CATEGORY") %>
                                                </td--%>
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

                                    <div class="tab-pane" id="tab2">
                                        <div>
                                            <asp:UpdateProgress ID="updNRI" runat="server" AssociatedUpdatePanelID="updNRIConfig"
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
                                        <asp:UpdatePanel ID="updNRIConfig" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Admission Batch</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmBatch_NRI" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_NRI_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAdmBatch_NRI"
                                                                Display="None" ValidationGroup="Submit_NRI" InitialValue="0" ErrorMessage="Please Select Admission Batch."></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Admission Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmType_NRI" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmType_NRI_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAdmType_NRI"
                                                                Display="None" ValidationGroup="Submit_NRI" InitialValue="0" ErrorMessage="Please Select Admission Type."></asp:RequiredFieldValidator>
                                                        </div>



                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Program Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlProgrammeType_NRI" runat="server" TabIndex="4" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlProgrammeType_NRI_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlProgrammeType_NRI"
                                                                Display="None" ValidationGroup="Submit_NRI" InitialValue="0" ErrorMessage="Please Select Programme Type."></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSchool_NRI" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_NRI_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSchool_NRI"
                                                                Display="None" ValidationGroup="Submit_NRI" InitialValue="0" ErrorMessage="Please Select School." Visible="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Degree</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree_NRI" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_NRI_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDegree_NRI"
                                                                Display="None" ValidationGroup="Submit_NRI" InitialValue="0" ErrorMessage="Please Select Degree."></asp:RequiredFieldValidator>
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Start Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <div class="input-group-addon" id="Div5">
                                                                    <div class="fa fa-calendar"></div>
                                                                </div>
                                                                <asp:TextBox ID="txtStartDate_NRI" runat="server" TabIndex="7" CssClass="form-control" ToolTip="Please Enter Start Date"></asp:TextBox>

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtStartDate_NRI" PopupButtonID="Div5" Enabled="true">
                                                                </ajaxToolKit:CalendarExtender>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDate_NRI"
                                                                    Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtStartDate_NRI"
                                                                    Display="None" ValidationGroup="Submit_NRI" ErrorMessage="Please Enter Start Date."></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Start Time</label>
                                                            </div>
                                                            <asp:TextBox ID="txtStartTime_NRI" runat="server" TabIndex="8" CssClass="form-control" ToolTip="Please Enter Start Time."></asp:TextBox>

                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartTime_NRI"
                                                                Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                                MaskType="Time" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtStartTime_NRI"
                                                                Display="None" ValidationGroup="Submit_NRI" ErrorMessage="Please Enter Start Time"></asp:RequiredFieldValidator>
                                                              <asp:RegularExpressionValidator ID="rfvtxtStartTime_NRI" runat="server" ControlToValidate="txtStartTime_NRI" Display="None"
                                                                ValidationGroup="Submit_NRI" ErrorMessage="Please Enter Valid Start Time" ValidationExpression="(((0[1-9])|(1[0-2])):([0-5])([0-9])\s(A|P)M)">
                                                               </asp:RegularExpressionValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>End Date</label>
                                                            </div>
                                                            <div class="input-group">
                                                                <div class="input-group-addon" id="Div6">
                                                                    <div class="fa fa-calendar text-blue"></div>
                                                                </div>
                                                                <asp:TextBox ID="txtEndDate_NRI" runat="server" TabIndex="9" ToolTip="Please Enter End Date." CssClass="form-control"></asp:TextBox>

                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtEndDate_NRI" PopupButtonID="Div6" Enabled="true">
                                                                </ajaxToolKit:CalendarExtender>

                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtEndDate_NRI"
                                                                    Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                    CultureTimePlaceholder="" Enabled="True" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEndDate_NRI"
                                                                    Display="None" ValidationGroup="Submit_NRI" ErrorMessage="Please Enter End Date"></asp:RequiredFieldValidator>
                                                                  <asp:RegularExpressionValidator ID="rfvtxtEndDate_NRI" runat="server" ControlToValidate="txtEndDate_NRI" Display="None"
                                                                ValidationGroup="Submit_NRI" ErrorMessage="Please Enter Valid End Time" ValidationExpression="(((0[1-9])|(1[0-2])):([0-5])([0-9])\s(A|P)M)">
                                                            </asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>End Time</label>
                                                            </div>
                                                            <asp:TextBox ID="txtEndTime_NRI" runat="server" TabIndex="10" CssClass="form-control" ToolTip="Please Enter End Time."></asp:TextBox>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtEndTime_NRI"
                                                                Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                                MaskType="Time" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtEndTime_NRI"
                                                                Display="None" ValidationGroup="Submit_NRI" ErrorMessage="Please Enter End Time."></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Application Fee</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAppFee_NRI" runat="server" CssClass="form-control" TabIndex="11" MaxLength="5"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtAppFee_NRI"
                                                                Display="None" ValidationGroup="Submit_NRI" ErrorMessage="Please Enter Application Fee."></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Details/Remarks</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRemark_NRI" runat="server" TabIndex="12" CssClass="form-control"></asp:TextBox>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvDetails" runat="server" ControlToValidate="txtDetails"
                                            Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Details"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Status</label>
                                                                <div style="margin-top: 10px">
                                                                    <asp:CheckBox ID="chkActive_NRI" runat="server" Text="&nbsp;Active" TextAlign="Right" ToolTip="Please Select Status." />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit_NRI" runat="server" Text="Submit" OnClick="btnSubmit_NRI_Click" CssClass="btn btn-primary" ValidationGroup="Submit_NRI" />
                                                    <asp:Button ID="btnCancel_NRI" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_NRI_Click" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" ValidationGroup="Submit_NRI" />
                                                </div>

                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlNRI" runat="server">
                                                        <asp:ListView ID="lvNRI" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Notification Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divadmissionlist_NRI">
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

                                                                            <th>Degree
                                                                            </th>
                                                                            <th>Fee
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
                                                                        <asp:ImageButton ID="btnEdit_NRI" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                                            CommandArgument='<%# Eval("CONFIGID")%>' OnClick="btnEdit_NRI_Click" AlternateText="Edit Record" ToolTip="Edit Record" />
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
                                                                        <%# Eval("DEGREENAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("FEES")%>
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
                                        </asp:UpdatePanel>
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
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
    </script>
</asp:Content>

