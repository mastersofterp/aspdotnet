<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_ServiceBook_Report.aspx.cs" Inherits="PayRoll_Pay_ServiceBook_Report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Employee ServiceBook Report</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSelection" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select College and Employee</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="upEmployee" runat="server">
                            <ContentTemplate>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" ToolTip="Select College" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please select College" ValidationGroup="ServiceBookReport"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Employee Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" TabIndex="2" ToolTip="Select Employee Name">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" ErrorMessage="Please select  Employee Name " ValidationGroup="ServiceBookReport"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>

                    <asp:Panel ID="Panel1" runat="server">
                        <div class="panel panel-info">
                            <div class="panel panel-heading">
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            Select
                                <asp:CheckBox ID="chkAll" runat="server" Text="All" onclick="return checkAll();" />
                                            &nbsp; Criteria
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-body">
                                <div class="col-md-12">
                                    <div class="form-group col-md-6">
                                        <div class="form-group col-md-6" id="trDept" runat="server" visible="false">
                                            <label>Department :</label>
                                            <asp:Label ID="lblDepartment" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-12 checkbox-list-colum">
                                            <div class="checkbox-list-box">
                                                <asp:CheckBox ID="chkPersonalMemoranda" runat="server" Text="Personal Memoranda" CssClass="select-all-checkbox"
                                                    Checked="true" TabIndex="3" Visible="false" />
                                                <asp:CheckBox ID="chkEducationalQualification" runat="server" Text="Qualification Information" TabIndex="5" CssClass="select-all-checkbox" Visible="false" />
                                                <asp:CheckBox ID="chkDeptExamAndOtherDetails" runat="server" Text="Departmental Exam Information" TabIndex="7" CssClass="select-all-checkbox" Visible="false" />
                                                <asp:CheckBox ID="ChkNomination" runat="server" Text="Employee Nomination Information" TabIndex="9" CssClass="select-all-checkbox" Visible="false" />
                                                <asp:CheckBox ID="chkTraning" runat="server" Text="Training/Short Term Course/Conference Attended" TabIndex="11" CssClass="select-all-checkbox" Visible="false" />
                                                <asp:CheckBox ID="chkTrainingConducted" runat="server" Text="Training/Short Term Course/Conference Conducted" TabIndex="13" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkIncetmentTermination" runat="server" Text="Other Details Entries Like Increment termination etc" TabIndex="15" CssClass="select-all-checkbox" Visible="false" />
                                                <asp:CheckBox ID="chkAdministrativeResponsibilities" runat="server" Text="Administrative Responsibilities" TabIndex="17" CssClass="select-all-checkbox" Visible="false" />
                                                <asp:CheckBox ID="chkFamilyParticulars" runat="server" Text="Family Information" TabIndex="4" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkMatter" runat="server" Text="Matter" TabIndex="6" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkPreviousQualifyingService" runat="server" Text="Previous Experience" TabIndex="8" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="ChkDetailsOfLoansAndAdvances" runat="server" Text="Loans And Advances Information" TabIndex="10" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkLeaveRecords" runat="server" Text="Leave Records" TabIndex="12" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkPayRevisionOrPromotion" runat="server" Text="Pay Revision Information" TabIndex="14" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkPublicationDetails" runat="server" Text="Publication Details" TabIndex="16" CssClass="select-all-checkbox" Visible="false" />
                                                <asp:CheckBox ID="chkInvitedTalks" runat="server" Text="Invited Talks" TabIndex="18" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkConsultancy" runat="server" Text="Consultancy" TabIndex="19" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkAccomplishment" runat="server" Text="Accomplishment" TabIndex="20" CssClass="select-all-checkbox" Visible="false" />
                                                <asp:CheckBox ID="chkMembership" runat="server" Text="Membership in Professional Body" TabIndex="21" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkStaffFunded" runat="server" Text="Funded Project" TabIndex="22" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkPatent" runat="server" Text="Patent" TabIndex="23" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkExp" runat="server" Text="Experience" TabIndex="24" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkProfessional" runat="server" Text="Professional Course Certification" TabIndex="25" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkAvishkar" runat="server" Text="Avishkar" TabIndex="26" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkResearch" runat="server" Text="Research" TabIndex="27" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkRevenue" runat="server" Text="Revenue Generated" TabIndex="28" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkCurrent" runat="server" Text="Current Appointment Status" TabIndex="29" CssClass="select-all-checkbox" Visible="false"/>
                                                <asp:CheckBox ID="chkAward" runat="server" Text="Award" TabIndex="30" CssClass="select-all-checkbox" Visible="false"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divMsg" runat="server">
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" ValidationGroup="ServiceBookReport" TabIndex="19"
                            OnClick="btnShowReport_Click" CssClass="btn btn-primary" ToolTip="Click here to Show Report" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="20"
                            OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBookReport"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                    </div>

                    <script type="text/javascript" language="javascript">
                        // Move an element directly on top of another element (and optionally
                        // make it the same size)
                        function Cover(bottom, top, ignoreSize) {
                            var location = Sys.UI.DomElement.getLocation(bottom);
                            top.style.position = 'absolute';
                            top.style.top = location.y + 'px';
                            top.style.left = location.x + 'px';
                            if (!ignoreSize) {
                                top.style.height = bottom.offsetHeight + 'px';
                                top.style.width = bottom.offsetWidth + 'px';
                            }
                        }
                        function checkAll() {
                            if (document.getElementById('ctl00_ContentPlaceHolder1_chkAll').checked) {
                                var countp = document.getElementsByTagName('ctl00_ContentPlaceHolder1_pnlCheck');
                                var Count = document.getElementsByTagName("input");
                                for (var r = 0; r < Count.length; r++) {
                                    if (Count[r].type == "checkbox") {
                                        Count[r].checked = true;
                                    }
                                }

                            }
                            else {
                                var countp = document.getElementsByTagName('ctl00_ContentPlaceHolder1_pnlCheck');
                                var Count = document.getElementsByTagName("input");
                                for (var r = 0; r < Count.length; r++) {
                                    if (Count[r].type == "checkbox" && Count[r].id != "ctl00_ContentPlaceHolder1_chkPersonalMemoranda") {
                                        Count[r].checked = false;
                                    }
                                }
                            }
                        }
                    </script>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
