<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPGenerateOfferLetter.aspx.cs" Inherits="ACADEMIC_DAIICTPostAdmission_ADMPGenerateOfferLetter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <style>
        .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">GENERATE OFFER LETTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:RadioButtonList ID="rdobtnoffer" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdobtnoffer_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Selected="True">Generate Offer Letter  &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">Lock Offer Letter &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="3">Send Offer Letter &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcadyear" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                            ValidationGroup="submit">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Test Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEntrance" TabIndex="3" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" ValidationGroup="submit">
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

                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Select List Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlListType" TabIndex="4" CssClass="form-control" data-select2-enable="true" runat="server" AutoPostBack="true"
                                        ValidationGroup="submit">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Confirm Student List</asp:ListItem>
                                        <asp:ListItem Value="2">Waiting Student List</asp:ListItem>
                                        <asp:ListItem Value="3">Confirm-Waiting Student List</asp:ListItem>
                                    </asp:DropDownList>

                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Offer Letter Print  Date</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-addon" id="imgCalStartDate">
                                            <span class="fa fa-calendar text-blue"></span>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="6" />

                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                            TargetControlID="txtDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:CompareValidator ID="valStartDateType" runat="server" ControlToValidate="txtDate"
                                            Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                            SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                    </div>
                                </div>

                                <div class="row" runat="server" visible="true" id="divhideoffer">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Category</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlCategory" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ErrorMessage="Please Select Category" InitialValue="0" ControlToValidate="ddlCategory"
                                            ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>

                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="7" onchange="return ValidateDate(this)"></asp:TextBox>

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtenderFromDate" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:CompareValidator ID="valFromDateType" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                            <asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ErrorMessage="Please Select From Date" ControlToValidate="txtFromDate"
                                                ValidationGroup="submit" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgClaToDate">
                                                <span class="fa fa-calendar text-blue"></span>
                                            </div>

                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="8" onchange="return ValidateDate(this)"></asp:TextBox>

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtenderToDate" runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgClaToDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:CompareValidator ID="valToDateType" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please enter a valid date." Operator="DataTypeCheck"
                                                SetFocusOnError="true" Type="Date" CultureInvariantValues="true" />
                                            <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ErrorMessage="Please Select To Date" ControlToValidate="txtToDate"
                                                ValidationGroup="submit" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Letter</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlLetter" TabIndex="5" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvddlLetter" runat="server" ErrorMessage="Please Select Letter" InitialValue="0" ControlToValidate="ddlLetter"
                                            ValidationGroup="submit" Display="None">
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
                                        <asp:RequiredFieldValidator ID="rfvddlRound" runat="server" ErrorMessage="Please Select Round" InitialValue="0" ControlToValidate="ddlRound" ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPayType" runat="server" visible="true">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment Type</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:DropDownList ID="ddlPaymentType" TabIndex="5" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvddlPaymentType" runat="server" ErrorMessage="Please Select Payment Type" InitialValue="0" ControlToValidate="ddlPaymentType"
                                            ValidationGroup="submit" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>



                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnShow_Click" />
                                <asp:Button runat="server" ID="btnOfferletter" Text="Generate Offer Letter" CssClass="btn btn-primary" OnClick="btnOfferletter_Click" Visible="false" ValidationGroup="submit" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="1" OnClick="btnSubmit_Click" Visible="false" ValidationGroup="submit" />
                                <asp:Button runat="server" ID="btnSendEmail" Text="Send Mail" CssClass="btn btn-primary" Visible="false" OnClick="btnSendEmail_Click" ValidationGroup="submit"  />
                                <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"  />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                            </div>

                            <%--     <div class="col-12">--%>

                            <asp:ListView ID="lvOffeerLetter" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Notification Details</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" id="divadmissionlist">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" /></th>
                                                <th>Applicant Name</th>
                                                <th>Application ID</th>
                                                <th>Program/ Branch</th>
                                                <th>User Email ID</th>
                                                <th>CRL</th>
                                                <th>Overall Merit No</th>
                                                <th>Category Wise Merit No</th>
                                                <th>Preview</th>
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
                                            <asp:CheckBox ID="ChkOffer" runat="server" ToolTip='<%# Eval("Application_Id") %>'
                                                Checked='<%# Convert.ToString(Eval("OFFER_LETTER_FILE_NAME"))== string.Empty?false:true %>'
                                                Enabled='<%# Convert.ToString(Eval("ISLOCKED"))== "True" && rdobtnoffer.SelectedValue != "3" ?false:true %>' />


                                        </td>
                                        <td><%# Eval("CANDIDATE_NAME") %>
                                            <asp:HiddenField ID="hdnOfferid" Visible="false" runat="server" Value='<%# Eval("GENERATE_OFFTER_LETTER_ID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblApplicationId" runat="server" Text='<%# Eval("Application_Id") %>' />
                                        </td>
                                        <td><%# Eval("LONGNAME") %>
                                            <asp:HiddenField ID="hdnUserNo" Visible="false" runat="server" Value='<%# Eval("USERNO") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblemail" runat="server" Text='<%# Eval("EMAILID") %>' /></td>
                                        <td><%# Eval("INDIA_RANK") %></td>
                                        <td><%# Eval("MERIT_NO") %></td>
                                        <td><%# Eval("CATEGORY_WISE_MERIT_NO") %></td>
                                        <td>
                                            <asp:UpdatePanel ID="updPreview" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnpreview" runat="server" CommandArgument='<%# Eval("OFFER_LETTER_FILE_NAME") %>'
                                                        data-target="#PassModel" TabIndex="1" Text="Offer Letter" OnClick="btnpreview_Click"
                                                        data-toggle="modal" Visible='<%# Convert.ToString(Eval("OFFER_LETTER_FILE_NAME"))== string.Empty?false:true %>'
                                                        ToolTip='<%# Eval("OFFER_LETTER_FILE_NAME") %>' CssClass="btn btn-primary" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnpreview" />

                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <%-- </div>--%>
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
            <%--<asp:PostBackTrigger ControlID="btnCancel" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

    <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlOfferLetter"
        TargetControlID="lnkPreview" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
    <asp:LinkButton ID="lnkPreview" runat="server" TabIndex="1"></asp:LinkButton>
    <asp:Panel ID="pnlOfferLetter" runat="server" CssClass="modalPopup" Visible="false">
        <div class="header">
            Document
        </div>
        <div class="body">
            <iframe runat="server" width="680px" height="600px" id="iframeView"></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" TabIndex="1" />
        </div>
    </asp:Panel>

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

                    }

                }
            }
            catch (err) {
                alert("Error : " + err.message);
            }
        }
    </script>

    <script>
        function ValidateDate() {
            var Fromdate = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var Todate = document.getElementById('<%=txtToDate.ClientID%>').value;
            var From_date = moment(Fromdate, 'DD/MM/YYYY');
            var To_date = moment(Todate, 'DD/MM/YYYY');

            var _FromDate = new Date(From_date);
            var _Todate = new Date(To_date);

            var currentDate = new Date(); // Get the current date

            if (_FromDate < currentDate.setHours(0, 0, 0, 0)) { // Compare only the date part
                alert("Do not select Back Date!");
                document.getElementById('<%=txtFromDate.ClientID%>').value = '';
        return;
    }
    else if (_Todate < _FromDate) {
        alert("Do not select Date less than From Date!");
        document.getElementById('<%=txtToDate.ClientID%>').value = '';
        return;
    }
}



    </script>


</asp:Content>

