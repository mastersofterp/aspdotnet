<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FacultyDiary.aspx.cs" Inherits="ACADEMIC_FacultyDiary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFac"
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

    <asp:UpdatePanel ID="updFac" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollegeScheme" runat="server" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlCollegeScheme_SelectedIndexChanged"
                                            AutoPostBack="True" TabIndex="1" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            AutoPostBack="True" TabIndex="1" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Operator</label>
                                        </div>
                                        <asp:DropDownList ID="ddlOptVal" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control"
                                            AutoPostBack="True" TabIndex="1">
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem Selected="True">&gt;=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>=</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Percentage</label>
                                        </div>
                                        <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" TabIndex="1" MaxLength="2"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                            FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ " TargetControlID="txtPercentage" />
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtFromDate1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit" TabIndex="1" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="txtFromDate1" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Attendance Start Date" IsValidEmpty="false"
                                                InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" ErrorMessage="Start Date is Invalid (Enter dd/mm/yyyy Format)"
                                                TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="submit" SetFocusOnError="True" />
                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" SetFocusOnError="True"
                                                ValidationGroup="submit" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtToDate1" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="submit" TabIndex="1" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="txtToDate1" />

                                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                ControlToValidate="txtToDate" EmptyValueMessage="Please Enter Attendance End Date"
                                                InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" IsValidEmpty="false"
                                                TooltipMessage="Please Enter Attendance End Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />

                                            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                ControlToValidate="txtToDate" Display="None"
                                                ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="btn-footer">
                            <asp:Button ID="btnShow" OnClientClick="return ValidateButton(this)" TabIndex="1" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                            <asp:Button ID="btnCancel" TabIndex="1" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                        </div>

                        <div class="col-12">
                            <asp:ListView ID="lvStudent" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Student List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Roll No</th>
                                                    <th>REGNO</th>
                                                    <th>Student Name</th>
                                                    <th>Attendance Percentage</th>
                                                    <th>Remark
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <asp:Label ID="lblRollNo" runat="server" Text='<%# Eval("ROLL NO") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDENT NAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAttendancePercentage" runat="server" Text='<%# Eval("ATTENDANCE PERCENTAGE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TabIndex="1" TextMode="MultiLine" Rows="1" Text='<%# Eval("REMARK")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                        <div class="btn-footer">
                            <asp:Button ID="btnSubmit" Visible="false" TabIndex="1" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReport" Visible="false" TabIndex="1" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="btnReport_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function ValidateButton() {


            if ($("#ctl00_ContentPlaceHolder1_ddlCollegeScheme").val() == 0) {
                alert("Please select College & Scheme !!");
                $("#ctl00_ContentPlaceHolder1_ddlCollegeScheme").focus();
                return false;
            }


            if ($("#ctl00_ContentPlaceHolder1_ddlSession").val() == 0) {
                alert("Please select Session !!");
                $("#ctl00_ContentPlaceHolder1_ddlSession").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_ddlSemester").val() == 0) {
                alert("Please select Semester !!");
                $("#ctl00_ContentPlaceHolder1_ddlSemester").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtPercentage").val() == "") {
                alert('Please Enter Percentage !!');
                $("#ctl00_ContentPlaceHolder1_txtPercentage").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtFromDate").val() == "") {
                alert('Please Enter Start Date !!');
                $("#ctl00_ContentPlaceHolder1_txtFromDate").focus();
                return false;
            }

            if ($("#ctl00_ContentPlaceHolder1_txtToDate").val() == "") {
                alert('Please Enter To Date !!');
                $("#ctl00_ContentPlaceHolder1_txtToDate").focus();
                return false;
            }
        };
    </script>
</asp:Content>


