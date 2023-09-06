<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NewStudentEntryReport.aspx.cs" Inherits="ACADEMIC_NewStudentEntryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="2"
                                            ToolTip="Please Select Admission Batch" AppendDataBoundItems="true"
                                            AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="true"
                                            ValidationGroup="Show" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                From Date
                                                 <asp:CompareValidator ID="cvleaving" runat="server" ControlToValidate="txtFDate"
                                                     EnableClientScript="False" ErrorMessage="Please Enter From Date (mm/dd/yyyy)."
                                                     Operator="DataTypeCheck" Type="Date" ValidationGroup="Show"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="rfvleaving" runat="server" ControlToValidate="txtFDate"
                                                    Display="None" ErrorMessage="Please Select From Date" SetFocusOnError="True"
                                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            </label>

                                        </div>

                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgleaving">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFDate" runat="server"></asp:TextBox>
                                            <%-- <asp:Image ID="imgleaving" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                            <ajaxToolKit:CalendarExtender ID="celeaving" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgleaving" TargetControlID="txtFDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meleaving" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <%--   <ajaxToolKit:MaskedEditValidator ID="mvleaving" runat="server" ControlExtender="meleaving"
                                                                        ControlToValidate="txtleaving" Display="None" EmptyValueMessage="Please Select Date of Leaving  the College"
                                                                        ErrorMessage="Please Select Date of Leaving  the College" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Print">                                                      
                                                                    </ajaxToolKit:MaskedEditValidator>--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>
                                                To Date
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTdate"
                                                    EnableClientScript="False" ErrorMessage="Please enter To Date (mm/dd/yyyy)."
                                                    Operator="DataTypeCheck" Type="Date" ValidationGroup="Print"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTdate"
                                                    Display="None" ErrorMessage="Please Select To Date" SetFocusOnError="True"
                                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            </label>

                                        </div>

                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgToDate">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTdate" runat="server"></asp:TextBox>
                                            <%-- <asp:Image ID="imgleaving" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgToDate" TargetControlID="txtTdate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                TargetControlID="txtTdate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <%--   <ajaxToolKit:MaskedEditValidator ID="mvleaving" runat="server" ControlExtender="meleaving"
                                                                        ControlToValidate="txtleaving" Display="None" EmptyValueMessage="Please Select Date of Leaving  the College"
                                                                        ErrorMessage="Please Select Date of Leaving  the College" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                        IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Print">                                                      
                                                                    </ajaxToolKit:MaskedEditValidator>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="butShow_Click"
                                    ValidationGroup="Show" />
                                <asp:Button ID="btnExcel" runat="server" Text="Report(Excel)" CssClass="btn btn-primary" OnClick="btnExcel_Click"
                                    ValidationGroup="Show" />
                                <asp:ValidationSummary ID="vsSelection" runat="server" ShowMessageBox="true" ShowSummary="false"
                                    DisplayMode="List" CssClass="btn btn-primary" ValidationGroup="Show" />
                            </div>
                            
                            <div class="col-12">
                                <asp:Panel ID="Panel3" runat="server">
                                    <asp:ListView ID="lvStudent" runat="server" EnableModelValidation="True" >
                                        <EmptyDataTemplate>
                                            <div>
                                                -- No Student Record Found --
                                            </div>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        
                                                        <th>Temporary Id</th>
                                                        <th><%--Enrollment No.--%>
                                                            <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Student Name</th>
                                                        <th>College</th>
                                                        <th>Degree</th>
                                                        <th>Fees Collection Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr >
                                                <td>
                                                    <asp:Label runat="server" ID="lblRegno" Text='<%#Eval("TEMPORARY_ID")%>' ToolTip='<%#Eval("TEMPORARY_ID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%#Eval("REGISTRATIONNO")%>
                                                </td>
                                                <td>
                                                    <%#Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("DEGREENAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("FEES COLLECTION STATUS")%>
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
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


