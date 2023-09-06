<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CreateBundle.aspx.cs" Inherits="ACADEMIC_EXAMINATION_CreateBundle" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExam"
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
    <asp:UpdatePanel ID="updExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BUNDLE CREATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>College & Scheme</label>--%>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AutoPostBack="True" AppendDataBoundItems="true" ToolTip="Please Select Scheme"
                                            TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged ="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Scheme Name " InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" TabIndex="2" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Date Of Exam</label>--%>
                                            <asp:Label ID="lblDYDateofexam" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                         <asp:DropDownList ID="ddlDate" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="form-control" TabIndex="2" data-select2-enable="true" OnSelectedIndexChanged="ddlDate_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="ddlDate"
                                            Display="None" ErrorMessage="Please Select Date" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-4 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Slot</label>--%>
                                            <asp:Label ID="lblDYExamslot" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSlot" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="6"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlSlot_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%--<asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ErrorMessage="Please Select Slot"
                                            ControlToValidate="ddlSlot" Display="None" ValidationGroup="submit" InitialValue="0" />
                                        
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>No. of Answer Books per Bundle</label>--%>
                                            <asp:Label ID="lblDYAnswerBook" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtStudPerBundle" CssClass="form-control" runat="server" Text="30" OnTextChanged="txtStudPerBundle_TextChanged" AutoPostBack="true" TabIndex="7"
                                            onkeyup="validateNumeric(this);" MaxLength="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter No. of students per Bundle"
                                            ControlToValidate="txtStudPerBundle" Display="None" ValidationGroup="submit" />
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Cousre - Branch - Sem</label>--%>
                                            <asp:Label ID="lblDYCourseBranchSem" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="8"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ErrorMessage="Please Select Course"
                                            ControlToValidate="ddlCourse" Display="None" ValidationGroup="submit" InitialValue="0" />
                                      
                                    </div>
                                   <%-- <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Please select Session,Date & Slot for Course selection</span>  </p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Please Select Session,Date & Slot for Date-wise Bundle List</span>  </p>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" TabIndex="9"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                 <asp:Button ID="btnDateWiseReport" runat="server" Text="Date-Wise Bundle List" TabIndex="11"
                                    OnClick="btnDateWiseReport_Click" ValidationGroup="submit" CssClass="btn btn-primary progress-button" />

                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="submit" TabIndex="10"
                                    OnClick="btnReport_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnStickerReport" runat="server" Text="Bundle Slip Report" ValidationGroup="submit"
                                    OnClick="btnStickerReport_Click" CssClass="btn btn-info" />
                               
                                <asp:Button ID="btnStickerOnScreenReport" runat="server" Text="Bundle Slip Report(On Screen)" ValidationGroup="submit"
                                    OnClick="btnStickerOnScreenReport_Click" CssClass="btn btn-info" Visible="false" />
                                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                <asp:Button ID="btnExcelReport" runat="server" Text="Online Evaluation Excel" Visible="false"
                                    OnClick="btnExcelReport_Click" ValidationGroup="ExcelReport" CssClass="btn btn-info progress-button" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="12"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="report" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="DateWiseReport" />
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="ExcelReport" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvBundleList" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5><b>Bundle List</b></h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Bundle No.
                                                    </th>
                                                    <th>Programme / Branch
                                                    </th>
                                                    <th>Course Code
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Reg No. From
                                                    </th>
                                                    <th>Reg No. To
                                                    </th>
                                                    <th>Bundle Book Count
                                                    </th>
                                                    <th>Status
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
                                            <td>
                                                <asp:Label ID="lblBundleNo" runat="server" Text='<%# Eval("BUNDLE_SRNO")%>' ToolTip='<%# Eval("BUNDLE_SRNO")%>' />
                                            </td>
                                            <td>

                                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("SHORTNAME")%>' ToolTip='<%# Eval("BRANCHNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCoursrNo" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNAME")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRegNoFrom" runat="server" Text='<%# Eval("REGNOFROM")%>' ToolTip='<%# Eval("REGNOFROM")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRegNoTo" runat="server" Text='<%# Eval("REGNOTO")%>' ToolTip='<%# Eval("REGNOTO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblBundleCount" runat="server" Text='<%# Eval("COUNT")%>' ToolTip='<%# Eval("COUNT")%>' />
                                            </td>
                                            <td>
                                                <%# Eval("BUNDLE_STATUS")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCourse" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>

</asp:Content>

