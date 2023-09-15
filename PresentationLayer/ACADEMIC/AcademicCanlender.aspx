<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AcademicCanlender.aspx.cs" Inherits="ACADEMIC_AcademicCanlender" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>
    <script>
        $(function () {

            $('#table2').DataTable({

            });
        });

    </script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <style>
        #ctl00_ContentPlaceHolder1_ceStartDate_popupDiv, ctl00_ContentPlaceHolder1_CalendarExtender2_popupDiv {
            z-index: 100;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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

    <asp:UpdatePanel ID="updSession" runat="server">
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
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdm" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            TabIndex="1" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" ToolTip="Select Addmission Batch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdm"
                                            Display="None" ErrorMessage="Please select Academic Year" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree1" runat="server" Font-Bold="True" TabIndex="2" AppendDataBoundItems="true"
                                            data-select2-enable="true" AutoPostBack="true" ToolTip="Select Degree">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Activity Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlActivityType" AppendDataBoundItems="true" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlActivityType_SelectedIndexChanged"
                                            data-select2-enable="true" TabIndex="3" ToolTip="Select Activity Type">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlActivityType"
                                            Display="None" ErrorMessage="Please select Activity Type" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div id="ExamDate" class="form-group col-lg-3 col-md-6 col-12" runat="server" style="display: none">
                                        <div class="label-dynamic">
                                            <label><span style="color: red;">*</span>Date Duration</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="Div2" runat="server">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtDateDuration" runat="server" ValidationGroup="submit"
                                                TabIndex="4" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="idPopup" TargetControlID="txtDateDuration" Animated="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtDateDuration" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtDateDuration" Display="None" EmptyValueMessage="Please Enter Date"
                                                ErrorMessage="Please Enter Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtDateDuration" Display="None" EmptyValueMessage="Please Enter Date"
                                                ErrorMessage="Please Enter Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Report" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtDateDuration" Display="None" EmptyValueMessage="Please Enter Date"
                                                ErrorMessage="Please Enter Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Consolidate" Enabled="false" />
                                            <asp:HiddenField ID="hdIssuerdate" runat="server" Value='<%# Eval("DATE")%>' />
                                        </div>
                                    </div>

                                    <div id="Div3" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Date Duration</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="idPopup" runat="server">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit"
                                                TabIndex="5" Value='<%# Eval("DATE")%>' />
                                            <ajaxToolKit:CalendarExtender ID="cetxtFromDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="idPopup" TargetControlID="txtFromDate" Animated="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Report" />
                                            <ajaxToolKit:MaskedEditValidator ID="meFromConsole" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Consolidate" Enabled="false" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFromDate" Display="None"
                                                ErrorMessage="Please Select From Date" ValidationGroup="submit" SetFocusOnError="True" InitialValue="">
                                            </asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("DATE")%>' />
                                        </div>
                                    </div>

                                    <div id="Div4" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Date Duration</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="idPopuptodate" runat="server">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodate" runat="server" TabIndex="6" ValidationGroup="submit" Value='<%# Eval("DATE")%>' />
                                            <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy" PopupButtonID="idPopuptodate"
                                                TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />

                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Report" />

                                            <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                            <ajaxToolKit:MaskedEditValidator ID="meToConsole" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Consolidate" Enabled="false" />
                                            <asp:RequiredFieldValidator ID="rfvdatenft" runat="server" ControlToValidate="txtTodate" Display="None"
                                                ErrorMessage="Please Select To Date" ValidationGroup="submit" SetFocusOnError="True" InitialValue="">
                                            </asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("DATE")%>' />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" id="lblperfrom" runat="server">
                                            <sup>* </sup>
                                            <label>Activity</label>
                                        </div>
                                        <asp:TextBox ID="txtActivity" TabIndex="7" runat="server" MaxLength="200" TextMode="MultiLine" ToolTip="Enter Activity"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtActivity" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Activity" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" id="Div5" runat="server">
                                            <sup>* </sup>
                                            <label>Genaral Schedule</label>
                                        </div>
                                        <asp:TextBox ID="txtGenaralSchedule" TabIndex="8" runat="server" MaxLength="200" TextMode="MultiLine" ToolTip="Enter Genaral Schedule "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfGenaralSchedule" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Genaral Schedule" ControlToValidate="txtGenaralSchedule"
                                            Display="None" ValidationGroup="submit" />
                                    </div>

                                    <div class="col-12 btn-footer">

                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="9" CausesValidation="true"
                                            ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ToolTip="Submit" />

                                        <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="10" OnClick="btnReport_Click"
                                            CssClass="btn btn-info" ToolTip="Show Report" CausesValidation="true" />

                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            TabIndex="10" OnClick="btnCancel_Click1" CssClass="btn btn-warning" />


                                        <asp:ValidationSummary ID="valSummary" DisplayMode="List" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="submit" />

                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="pnlSession" runat="server">
                                            <asp:ListView ID="lvAcademicCalander" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr class="bg-light-blue">
                                                                <th>Edit </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYlvAdmBatch" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <th>Activity Type</th>
                                                                <th>Activity</th>
                                                                <th>From Date Duration</th>
                                                                <th>To Date Duration</th>
                                                                <th>General Schedule </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="bg-pista text-darkgray">

                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SRNO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="11" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("ACADEMIC_YEAR_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ACTIVITY_TYPE")%>
                                                        </td>

                                                        <td>
                                                            <%# Eval("ACTIVITY")%>  

                                                        </td>
                                                        <td>
                                                            <%# Eval("DATE_DUTRATION")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TO_DATE_DURATION") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("GENERAL_SCHEDULE")%>
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
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>





    <div>
        <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
    </div>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

