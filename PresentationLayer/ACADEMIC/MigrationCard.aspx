<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MigrationCard.aspx.cs" Inherits="ACADEMIC_MigrationCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../includes/prototype.js" type="text/javascript"></script>

    <script src="../includes/scriptaculous.js" type="text/javascript"></script>

    <script src="../includes/modalbox.js" type="text/javascript"></script>
    
            <script type="text/javascript">
                function RunThisAfterEachAsyncPostback() {
                    RepeaterDiv();

                }

                function RepeaterDiv() {
                    $(document).ready(function () {

                        $(".display").dataTable({
                            "bJQueryUI": true,
                            "sPaginationType": "full_numbers"
                        });

                    });

                }
            </script>
           

            <script type="text/javascript">
                RunThisAfterEachAsyncPostback();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
            </script>--%>
    <div id="divMsg" runat="server"></div>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Migration Card Details</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Search Student By Registration No. </label>
                                </div>

                                <div class="input-group date">
                                    <asp:TextBox ID="txtStudent" runat="server" MaxLength="100" TabIndex="1" CssClass="form-control" />
                                    <div class="input-group-addon">
                                        <a href="#" title="Search Student for Modification" onclick="Modalbox.show($('divdemo2'), {title: this.title, width: 600,overlayClose:false});return false;">
                                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/search.png" TabIndex="1"
                                                AlternateText="Search Student by  Name or Reg No." ToolTip="Search Student by Name or Reg No." />
                                        </a>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblMsg" runat="server" SkinID="Errorlbl"></asp:Label>

                    <asp:UpdatePanel ID="updSelection" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlDetails" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Student Details</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmbatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAdmbatch" runat="server" ControlToValidate="ddlAdmbatch"
                                                Display="None" ValidationGroup="Show" InitialValue="0"
                                                ErrorMessage="Please Select Admission batch"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Name</label>
                                            </div>
                                            <asp:TextBox ID="txtName" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Select Name"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                                Display="None" ValidationGroup="Report"
                                                ErrorMessage="Please Select Name"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Father's Name</label>
                                            </div>
                                            <asp:TextBox ID="txtFatherName" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890,!@#$%^&*()_+" />
                                            <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ControlToValidate="txtFatherName"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Select Father Name"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Registration Number</label>
                                            </div>
                                            <asp:TextBox ID="txtRegistrationNo" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College last attended</label>
                                            </div>
                                            <asp:TextBox ID="txtlastSchool" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastSchool" runat="server" ControlToValidate="txtlastSchool"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Select School/College/University"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree/Course Last admitted</label>
                                            </div>
                                            <asp:TextBox ID="txtLastDegree" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastDegree" runat="server" ControlToValidate="txtLastDegree"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Roll No of Last Examination passed</label>
                                            </div>
                                            <asp:TextBox ID="txtLastRoll" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                TargetControlID="txtLastRoll" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars=",!@#$%^&*()_+" />
                                            <asp:RequiredFieldValidator ID="rfvLastRoll" runat="server" ControlToValidate="txtLastRoll"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Select Roll No &  Year"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Year of Last Examination passed</label>
                                            </div>
                                            <asp:TextBox ID="txtlastExmYear" runat="server" MaxLength="4" CssClass="form-control" onchange="CheckFutureDate(this);"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers"
                                                TargetControlID="txtlastExmYear">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvLastExmYear" runat="server" ControlToValidate="txtlastExmYear"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Select Year"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Complete Postal address</label>
                                            </div>
                                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                                Display="None" ValidationGroup="Show"
                                                ErrorMessage="Please Select Year"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Mobile phon</label>
                                            </div>
                                            <asp:TextBox ID="txtMobile" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Email id</label>
                                            </div>
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="35" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                                ErrorMessage="*" ControlToValidate="txtEmail"
                                                ValidationGroup="vgSubmit" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                runat="server" ErrorMessage="Please Enter Valid Email ID"
                                                ValidationGroup="Show" ControlToValidate="txtEmail"
                                                CssClass="requiredFieldValidateStyle"
                                                ForeColor="Red"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                            </asp:RegularExpressionValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Migration Certificate Applied</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdoMigration" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Original</asp:ListItem>
                                                <asp:ListItem Value="2">Duplicate</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Card Serial No</label>
                                            </div>
                                            <asp:TextBox ID="txtCardSerialNo" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Show" />

                                    <asp:Button ID="btnPrintCard" runat="server" Text="Print card" CssClass="btn btn-inf" OnClick="btnPrintCard_Click" ValidationGroup="Report" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Report" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div id="divdemo2" style="display: none; height: 550px">
                        <asp:UpdatePanel ID="updEdit" runat="server">
                            <ContentTemplate>
                                <table cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td>Search Criteria:
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                                            <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="Reg No" GroupName="edit"
                                                Checked="True" />
                                            <%--<asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" GroupName="edit" />
                                <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                                
                                <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit" Checked="True" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Search String :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" />
                                            <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                                <ProgressTemplate>
                                                    <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                                    Loading.. Please Wait!
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" width="100%">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <LayoutTemplate>
                                                    <div class="vista-grid">
                                                        <div class="titlebar">
                                                            Login Details
                                                        </div>
                                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                            <thead>
                                                                <tr class="header">
                                                                    <th style="width: 20%">Name
                                                                    </th>
                                                                    <th style="width: 20%">IdNo
                                                                    </th>
                                                                    <th style="width: 20%">Reg No.
                                                                    </th>
                                                                    <th style="width: 30%">Branch
                                                                    </th>
                                                                    <th style="width: 10%">Semester
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                    <div class="listview-container">
                                                        <div id="demo-grid" class="vista-grid">
                                                            <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td style="width: 20%">
                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <%# Eval("idno")%>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <%# Eval("ENROLLNO")%>
                                                        </td>
                                                        <td style="width: 30%">
                                                            <%# Eval("longname")%>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <%# Eval("semesterno")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </td>
                                    </tr>
                                </table>

                            </ContentTemplate>

                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript" language="javascript">

        function submitPopup(btnsearch) {
            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name"
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "enrollmentno";

            searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
            __doPostBack(btncancelmodal, '');
            return true;
        }

        function CheckFutureDate(id) {
            //Created By Mr. Manish Walde
            // Return today's date and time
            ////var date = new Date();
            //var today = $.datepicker.formatDate("dd-mm-yy", date);

            // returns the month (from 0 to 11)
            ////var month = date.getMonth() + 1;
            //month = month - 1;
            //typeof month;

            ////if (month.toString().length == 1)
            ////    month = "0" + month;

            // returns the day of the month (from 1 to 31)
            //var day = date.getDate();
            //if (String(day).length == 1)
            //    day = "0" + day;

            // returns the year (four digits)

            var enterYear = id.value;
            if (String(enterYear).length < 4) {
                alert('Year is not in correct format.');
                id.value = '';

                id.focus();
            }
            else {
                // Return today's date and time
                var date = new Date();

                // returns the year (four digits)
                var year = date.getFullYear();

                if (enterYear > year) {
                    alert('Future date cannot be entered as last exam passed.');
                    id.value = '';

                    id.focus();
                }
            }
        }
    </script>

    <%--     </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnPrintCard" />
        </Triggers>
        </asp:UpdatePanel>--%>
</asp:Content>

