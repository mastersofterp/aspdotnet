<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPBranchCounselling.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPBranchCounselling" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
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

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BRANCH COUNSELING</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcadyear" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--<asp:RequiredFieldValidator ID="rfvAdmissionBatch" runat="server" ErrorMessage="Please Select Admission Batch" InitialValue="0" ControlToValidate="ddlListType" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Test Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEntrance" TabIndex="3" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEnterance" runat="server" ControlToValidate="ddlEntrance"
                                            Display="None" ErrorMessage="Please Select Test Name" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" TabIndex="2" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="submit" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree" InitialValue="0" ControlToValidate="ddlDegree" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch" InitialValue="0" ControlToValidate="ddlBranch" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                </div>


                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Round</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRound" TabIndex="5" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Round-1</asp:ListItem>
                                            <asp:ListItem Value="2">Round-2</asp:ListItem>
                                            <asp:ListItem Value="3">Round-3</asp:ListItem>
                                            <asp:ListItem Value="4">Round-4</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvround" runat="server" ErrorMessage="Please Select Round" InitialValue="0" ControlToValidate="ddlRound" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Confirmation Letter Print  Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgCalStartDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="6" />
                                            <%--  <asp:Image ID="imgCalStartDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:CompareValidator ID="valStartDateType" runat="server" ControlToValidate="txtDate"
                                                Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                        </div>

                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>

                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="7" onchange="return ValidateDate()"></asp:TextBox>
                                            <%--  <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtenderFromDate" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:CompareValidator ID="valFromDateType" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />

                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter a FromDate" InitialValue="0" ControlToValidate="txtFromDate" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgClaToDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>

                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="8" onchange="return ValidateDate()"></asp:TextBox>
                                            <%--  <asp:Image ID="imgClaToDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtenderToDate" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgClaToDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:CompareValidator ID="valToDateType" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a ToDate" InitialValue="0" ControlToValidate="txtToDate" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Letter</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlLetter" TabIndex="5" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Letter " InitialValue="0" ControlToValidate="ddlLetter" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>



                                    <%--                           <div class="form-group col-lg-3 col-md-6 col-12" >
                                        <div class="label-dynamic">
                                            <sup> </sup>
                                            <label>Select List Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlListType" TabIndex="4" CssClass="form-control" data-select2-enable="true" runat="server" AutoPostBack="true"
                                            ValidationGroup="submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Confirm Student List</asp:ListItem>
                                            <asp:ListItem Value="2">Waiting Student List</asp:ListItem>
                                            <asp:ListItem Value="3">Confirm-Waiting Student List</asp:ListItem>
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       
                                    </div>--%>
                                </div>
                            </div>
                        </div>


                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnShow_Click" />
                            <asp:Button runat="server" ID="btnOfferletter" Text="Generate Confirm Letter" CssClass="btn btn-primary" Visible="false" />
                            <asp:Button runat="server" ID="btnSendEmail" Text="Send Mail" CssClass="btn btn-primary" Visible="false" />
                            <asp:Button runat="server" ID="btnDownload" Text="Download" Visible="false" ToolTip="Download" CssClass="btn btn-primary" />
                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                        </div>

                        <div class="col-md-12">
                            <%--       <asp:Panel ID="pnllvSh" runat="server">--%>
                            <asp:ListView ID="lvBranch_Counselling" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Student Details</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divadmissionlist">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" /></th>
                                                <th>Application No </th>
                                                <th>Candidate Name</th>
                                                <th>Gender</th>
                                                <th>Date of Birth</th>
                                                <th>Category</th>
                                                <th>Overall Rank </th>
                                                <th>Category Rank</th>
                                                <th>Branch Pref</th>
                                                <th>Alloted Pref</th>
                                                <th>Waiting</th>

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
                                            <%--   <%# Container.DataItemIndex + 1%>--%>
                                            <asp:CheckBox ID="ChkOffer" runat="server" ToolTip='<%# Eval("APPLICATION_ID") %>' />
                                        </td>
                                        <td><%# Eval("APPLICATION_ID") %></td>
                                        <td><%# Eval("CANDIDATE_NAME") %></td>
                                        <td><%# Eval("GENDER") %></td>
                                        <td><%# Eval("DOB","{0:dd-MMM-yyyy}")%></td>
                                        <td><%# Eval("CATEGORY") %></td>
                                        <td><%# Eval("OVERALL_MERIT") %></td>
                                        <td><%# Eval("CAT_MERIT") %></td>
                                        <%-- <td><%# Eval("BRANCH_PREF") %></td>--%>
                                        <td>
                                            <asp:Label ID="lablepref" runat="server" Text='<%# Eval("BRANCH_PREF").ToString().Replace(",", "<br />") %>'></asp:Label></td>

                                        <%-- <td><%# Eval("ALLOTED_PREF") %></td>--%>
                                        <td>
                                            <asp:Label ID="lblalloted" runat="server" Text='<%# Eval("ALLOTED_PREF").ToString().Replace(",", "<br />") %>'></asp:Label></td>

                                        <%--<td><%# Eval("STATUS") %></td>--%>
                                        <td>
                                            <asp:Label ID="lblBranchNames" runat="server" Text='<%# Eval("STATUS").ToString().Replace(",", "<br />") %>'></asp:Label></td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <%-- </asp:Panel>--%>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnOfferletter" />
            <asp:PostBackTrigger ControlID="btnSendEmail" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function totAllSubjects(headchk) {
            debugger;
            var sum = 0;
            var frm = document.forms[0]
            try {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            // SumTotal();
                            // var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                            //// alert(j);
                            // sum += parseFloat(j);
                            if (e.disabled == false) {
                                e.checked = true;
                            }
                        }
                        else {
                            if (e.disabled == false) {
                                e.checked = false;
                                headchk.checked = false;
                            }
                        }

                        // x = sum.toString();
                    }

                }
            }
            catch (err) {
                alert("Error : " + err.message);
            }
        }
    </script>
    <%--   <script>
        function ValidateDate() {
            var Fromdate = document.getElementById('txtFromDate').value;
            var Todate = document.getElementById('txtToDate').value;

            var From_date = moment(Fromdate, 'DD/MM/YYYY');
            var To_date = moment(Todate, 'DD/MM/YYYY');

            var currentDate = moment(); // Get the current date

            if (From_date.isAfter(currentDate, 'day')) {
                alert("Do not select future dates for From Date!");
                document.getElementById('txtFromDate').value = '';
                return false;
            } else if (To_date.isBefore(From_date, 'day')) {
                alert("Do not select dates less than From Date for To Date!");
                document.getElementById('txtToDate').value = '';
                return false;
            }
            return true;
        }

    </script>--%>


    <script>
        function ValidateDate() {
            var Fromdate = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var Todate = document.getElementById('<%=txtToDate.ClientID%>').value;
            var From_date = moment(Fromdate, 'DD/MM/YYYY');
            var To_date = moment(Todate, 'DD/MM/YYYY');

            var currentDate = moment(); // Get the current date

            if (From_date.isAfter(currentDate)) { // Compare with the current date
                alert("From Date Cannot Be Future Date!");
                document.getElementById('<%=txtFromDate.ClientID%>').value = '';
                return;
            }
            else if (To_date.isBefore(From_date)) { // Compare with the from date
                alert("To Date Cannot be Less Than From Date!");
                document.getElementById('<%=txtToDate.ClientID%>').value = '';
        return;
    }
}

    </script>


</asp:Content>

