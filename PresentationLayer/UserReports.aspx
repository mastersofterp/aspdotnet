<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UserReports.aspx.cs" Inherits="UserReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlUser"
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
    <asp:UpdatePanel ID="updpnlUser" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><b>USER REPORTS</b></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="col-md-12">

                                    <div class="form-group col-md-6">
                                        <asp:RadioButtonList ID="rdUser" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdUser_SelectedIndexChanged">
                                            <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;&nbsp;Active&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;&nbsp;&nbsp;&nbsp;Deactive</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div id="divrb" runat="server" visible="false">
                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-4">
                                                <asp:RadioButton ID="rbDate" GroupName="a" runat="server" AutoPostBack="true" OnCheckedChanged="rbDate_CheckedChanged" Text="&nbsp;&nbsp;&nbsp;&nbsp;Total Users as on date" ValidationGroup="submit" />
                                            </div>
                                            <div class="form-group col-md-12" id="divDate" runat="server" visible="false">
                                                <div class="form-group col-md-4">
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="ceActiveDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtdate" PopupButtonID="imgExamDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" TargetControlID="txtdate"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" MaskType="Date" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mvDate" runat="server" EmptyValueMessage="Please Select Date"
                                                            ControlExtender="meDate" ControlToValidate="txtdate" IsValidEmpty="false"
                                                            InvalidValueMessage=" Date is invalid" Display="None" ErrorMessage="Please Select Date"
                                                            InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtdate"
                                                            Display="None" ErrorMessage="Please Select Date" InitialValue="0" ValidationGroup="submit"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-12">
                                                <asp:RadioButton ID="rbRangeDate" GroupName="a" runat="server" AutoPostBack="true" OnCheckedChanged="rbRangeDate_CheckedChanged" Text=" &nbsp;&nbsp;&nbsp;&nbsp;Total Users between date" ValidationGroup="submit" />
                                            </div>

                                            <div class="form-group col-md-12">
                                                <div id="divRangeDate" runat="server" visible="false">
                                                    <%--<div class="form-group col-md-1" style="margin-top: 8px;">From </div>--%>
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar text-blue" id="fromCal"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtFromdate" runat="server" CssClass="form-control" />
                                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                PopupButtonID="fromCal" TargetControlID="txtFromdate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                TargetControlID="txtFromdate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                                ControlToValidate="txtFromdate" Display="None" EmptyValueMessage="Please enter From Date"
                                                                ErrorMessage="Please Select Active From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromdate"
                                                                Display="None" ErrorMessage="Please Select Date" InitialValue="0" ValidationGroup="submit"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <%--<div class="form-group col-md-1" style="margin-top: 8px; text-align: center;">to </div>--%>
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar text-blue" id="ToCal"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" />
                                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                                PopupButtonID="ToCal" TargetControlID="txtTodate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999"
                                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                TargetControlID="txtTodate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please enter To  Date"
                                                                ErrorMessage="Please Select Active To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                                            <asp:CompareValidator ID="Cmpactive" runat="server"
                                                                ControlToCompare="txtFromdate" ControlToValidate="txtTodate" ValidationGroup="Date"
                                                                ErrorMessage="Must be greater" Operator="GreaterThan" Type="Date" SetFocusOnError="True" Display="Dynamic" BorderStyle="Double"></asp:CompareValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                    <%-- <div id="divDeDeactive" runat="server" visible="false">
                                      <div class="form-group col-md-12">
                                        <div class="form-group col-md-6">
                                            <asp:RadioButton ID="rbDeactiveDate" GroupName="b" runat="server" AutoPostBack="true" OnCheckedChanged="rbDeactiveDate_CheckedChanged" Text="&nbsp;&nbsp;&nbsp;&nbsp;Total Users as on date" ValidationGroup="submit" />
                                        </div>
                                             <div class="form-group col-md-12" id="divDeActiveDate" runat="server" visible="false">
                                        <div class="form-group col-md-4">
                                          <div class="input-group">
                                               <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                  </div>
                                             <asp:TextBox ID="txtDeactivedate" runat="server" CssClass="form-control" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            PopupButtonID="imgAttendanceDate" TargetControlID="txtDeactivedate"  />
                                        <ajaxToolKit:MaskedEditExtender ID="meDeactiveDate" runat="server" Mask="99/99/9999"
                                            MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            TargetControlID="txtDeactivedate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvDeactiveDate" runat="server" ControlExtender="meDeactiveDate"
                                            ControlToValidate="txtDeactivedate" Display="None" EmptyValueMessage="Please enter Deactive Date"
                                            ErrorMessage="Please Select Deactive Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                        </div>
                                       </div>
                                                 </div>
                                      </div>

                                       <div class="form-group col-md-12">
                                         <div class="form-group col-md-6">
                                            <asp:RadioButton ID="rbDeActiveRangeDate" GroupName="b" runat="server" AutoPostBack="true" OnCheckedChanged="rbDeActiveRangeDate_CheckedChanged" Text="&nbsp;&nbsp;&nbsp;&nbsp;Total Users between date" ValidationGroup="submit" />
                                        </div>

                                       <div class="form-group col-md-12">
                                           <div id="divDeActiveRangeDate" runat="server" visible="false">
                                        <div class="form-group col-md-4">
                                             <div class="input-group">
                                                   <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                  </div>
                                             <asp:TextBox ID="txtDeactiveFromdate" runat="server" CssClass="form-control" />
                                              <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                            PopupButtonID="imgAttendanceDate" TargetControlID="txtDeactiveFromdate" />
                                        <ajaxToolKit:MaskedEditExtender ID="meDeactiveFromDate" runat="server" Mask="99/99/9999"
                                            MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            TargetControlID="txtDeactiveFromdate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvDeactiveFromDate" runat="server" ControlExtender="meDeactiveFromDate"
                                            ControlToValidate="txtDeactiveFromdate" Display="None" EmptyValueMessage="Please enter Deactive From Date"
                                            ErrorMessage="Please Select Deactive From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                           </div>
                                        </div>
                                           <div class="form-group col-md-1"> to </div>
                                         <div class="form-group col-md-4">
                                           <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                  </div>
                                             <asp:TextBox ID="txtDeactiveTodate" runat="server" CssClass="form-control" />
                                         <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                            PopupButtonID="imgAttendanceDate" TargetControlID="txtDeactiveTodate"  />
                                        <ajaxToolKit:MaskedEditExtender ID="meDeactiveToDate" runat="server" Mask="99/99/9999"
                                            MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            TargetControlID="txtDeactiveTodate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvDeactiveToDate" runat="server" ControlExtender="meDeactiveToDate"
                                            ControlToValidate="txtDeactiveTodate" Display="None" EmptyValueMessage="Please enter Deactive To  Date"
                                            ErrorMessage="Please Select Deactive To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                        <asp:CompareValidator ID="CmpDeactive" runat="server"
                                        ControlToCompare="txtDeactiveFromdate" ControlToValidate="txtDeactiveTodate" ValidationGroup = "Date"
                                        ErrorMessage="Must be greater" Operator="GreaterThan" Type="Date" SetFocusOnError="True" Display="Dynamic" BorderStyle="Double"></asp:CompareValidator>
                                          </div>
                                        </div>
                                               </div>
                                       </div>

                                       </div>

                                    </div>--%>

                                    <div class="box-footer col-md-12">

                                        <p class="text-center">
                                            <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" Visible="false" ValidationGroup="Date" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnExcel" runat="server" Text="Excel Report" OnClick="btnExcel_Click" Visible="false" ValidationGroup="Date" CssClass="btn btn-primary" />
                                            <%--<asp:Button ID="btnUserCountExcel" runat="server" Text="Month-wise User Count(Excel)" Visible="false" OnClick="btnUserCountExcel_Click" ValidationGroup="Date" CssClass="btn btn-primary" />--%>
                                            <asp:Button ID="btnExcelMonthWiseCount" runat="server" Text="Month-wise Count(Excel)" Visible="false" ValidationGroup="Date" CssClass="btn btn-primary" OnClick="btnExcelMonthWiseCount_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false" CssClass="btn btn-warning" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" DisplayMode="List" ValidationGroup="Date" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" DisplayMode="List" ValidationGroup="submit" />
                                        </p>
                                    </div>


                                </div>

                            </div>

                        </div>

                        <div class="box-footer">
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnExcelMonthWiseCount" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        $("#EndDate").change(function () {
            var startDate = document.getElementById("StartDate").value;
            var endDate = document.getElementById("EndDate").value;

            if ((Date.parse(startDate) >= Date.parse(endDate))) {
                alert("End date should be greater than Start date");
                document.getElementById("EndDate").value = "";
            }
        });
    </script>


</asp:Content>

