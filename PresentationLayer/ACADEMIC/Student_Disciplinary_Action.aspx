<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Student_Disciplinary_Action.aspx.cs" Inherits="ACADEMIC_Student_Disciplinary_Action" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDiscipline"
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
    <asp:UpdatePanel ID="updDiscipline" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title"><b>DISCIPLINARY ACTION</b></h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>College</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCollege" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCollege" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>College</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Enrollment No :</label>
                                        </div>
                                        <asp:TextBox ID="txtStudentRollNo" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2"
                                            ToolTip="Please Enter Enrollment No" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStudentRollNo" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Enrollment No" InitialValue="" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtStudentRollNo" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Enter Enrollment No" InitialValue="" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_Click"
                                    Text="Show Details" ValidationGroup="show" />
                            </div>
                            <div runat="server" id="divStudentDetails" visible="false" class="form-group col-md-12">
                                <div class="sub-heading">
                                    <h5>Student Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-4">
                                        <div class="label-dynamic">
                                            <label>Student Name :</label>
                                        </div>
                                        <asp:Label runat="server" ID="lblStudentName" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:Label runat="server" ID="lblEnrollmentNo" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="label-dynamic">
                                            <asp:Label runat="server" ID="lblDYddlSemester" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:Label runat="server" ID="lblSemester" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="label-dynamic">
                                            <asp:Label runat="server" ID="lblDYddlDegree" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:Label runat="server" ID="lblDegree" CssClass="form-control"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 form-group">
                                        <div class="label-dynamic">
                                            <label>Branch :</label>
                                        </div>
                                        <asp:Label runat="server" ID="lblBranch" CssClass="form-control"></asp:Label>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                TabIndex="3" ToolTip="Please Select From Date" CssClass="form-control" Style="z-index: 0;" />
                                            <%-- <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                            AlternateText="Select Date" Style="cursor: pointer" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="dvcal1" />
                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Select From Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <%--   <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                        ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Start Date"
                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                        TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                        ValidationGroup="submit" SetFocusOnError="True" />--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="submit" TabIndex="4"
                                                ToolTip="Please Select To Date" CssClass="form-control" Style="z-index: 0;" />
                                            <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                            AlternateText="Select Date" Style="cursor: pointer" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="dvcal2" />
                                            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Select To Date" ControlToValidate="txtToDate" Display="None"
                                                ValidationGroup="submit" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <%-- <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                        ControlToValidate="txtToDate" EmptyValueMessage="Please Select To Date"
                                        InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                        TooltipMessage="Please Select To Date" EmptyValueBlurredText="Empty"
                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Remark :</label>
                                        </div>
                                        <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" MaxLength="500"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                            TargetControlID="txtRemark" FilterType="Custom" FilterMode="InvalidChars"
                                            InvalidChars="~`!@#$%^*()_+=/:;<>?'{}[]\|-&&;'" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRemark"
                                            Display="None" ErrorMessage="Please Enter Remark" InitialValue="" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnSubmit_Click" Enabled="false" />

                                <asp:Button ID="btnReport" runat="server" Text="Download PDF" CssClass="btn btn-info" OnClick="btnReport_Click"
                                    ValidationGroup="report" />
                                <asp:Button ID="btnExcel" runat="server" Text="Download EXCEL" CssClass="btn btn-info" OnClick="btnExcel_Click"
                                    ValidationGroup="report" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="true"
                                    ValidationGroup="submit" ShowSummary="false" />
                                <asp:ValidationSummary ID="vs2" runat="server" ShowMessageBox="true"
                                    ValidationGroup="show" ShowSummary="false" />
                                <asp:ValidationSummary ID="vs3" runat="server" ShowMessageBox="true"
                                    ValidationGroup="report" ShowSummary="false" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlStudents" runat="server" Visible="false">
                                    <h4>Disciplinary Action Student List</h4>
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-hover table-bordered table-striped display" id="divsessionlist" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th style="text-align: center;">Edit
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>From Date 
                                                        </th>
                                                        <th>To Date 
                                                        </th>
                                                        <th>Remark
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif"
                                                        CommandArgument='<%# Eval("DISCIPLINE_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFromDate" runat="server" Text='<%#Eval("FROMDATE", "{0:dd/MM/yyyy}")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblToDate" runat="server" Text='<%#Eval("TODATE", "{0:dd/MM/yyyy}")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                        </tbody></table>
                                    </FooterTemplate>--%>
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



 <%--   <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divsessionlist').DataTable({
                scrollX: 'true'
            });
        }

    </script>--%>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
