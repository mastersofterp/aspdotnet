<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BranchCounsellingActivity.aspx.cs" Inherits="ACADEMIC_BranchCounsellingActivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                     <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Phd Students Registration Form Activity</a>
                            </li>
                            <%-- <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">APPLICANT MARKS/DOCUMENT VERIFICATION</a>
                                        </li>--%>
                        </ul>



                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">

                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPhdactivity"
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
                                <asp:UpdatePanel ID="updPhdactivity" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12" id="divGeneralInfo" runat="server">
                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Admission Batch</label>--%>
                                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtAdmBatch" runat="server" ReadOnly="true" ViewStateMode="Enabled" />--%>
                                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" ToolTip="Please Select Admission Batch" ReadOnly="true" ViewStateMode="Enabled"
                                                            CssClass="form-control" data-select2-enable="true">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Start Date</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtStartDate" runat="server" TabIndex="3" CssClass="form-control" AutoComplete="off" />
                                                            <%-- <asp:Image ID="imgCalStartDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                            <asp:RequiredFieldValidator ID="valStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                Display="None" ErrorMessage="Please enter start date." SetFocusOnError="true"
                                                                ValidationGroup="submit" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                                TargetControlID="txtStartDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>

                                                            <asp:CompareValidator ID="valStartDateType"
                                                                runat="server"
                                                                ControlToValidate="txtStartDate"
                                                                ControlToCompare="txtEndDate"
                                                                Display="Dynamic"
                                                                ErrorMessage="Please enter a valid date."
                                                                Operator="LessThan"
                                                                SetFocusOnError="true"
                                                                Type="Date"
                                                                CultureInvariantValues="true"
                                                                ValidationGroup="submit" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>End Date</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtEndDate" runat="server" TabIndex="4" CssClass="form-control" AutoComplete="off" />
                                                            <%--<asp:Image ID="imgCalEndDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                                TargetControlID="txtEndDate" PopupButtonID="imgCalEndDate" Enabled="true" EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="valEndDate" runat="server" ControlToValidate="txtEndDate"
                                                                Display="None" ErrorMessage="Please enter end date." SetFocusOnError="true" ValidationGroup="submit" />
                                                            <asp:CompareValidator ID="valEndDateType"
                                                                runat="server"
                                                                ControlToValidate="txtEndDate"
                                                                ControlToCompare="txtStartDate"
                                                                Display="Dynamic"
                                                                ErrorMessage="Please enter a valid date."
                                                                Operator="GreaterThan"
                                                                SetFocusOnError="true"
                                                                Type="Date"
                                                                ValidationGroup="submit"
                                                                CultureInvariantValues="true" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" >
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Registration Fees</label>
                                                            <asp:TextBox ID="txtFees" runat="server" TabIndex="1" ToolTip="Please Enter Registragtion Fees." MaxLength="6" AutoComplete="off" CssClass="form-control" style="margin-top:5px"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ajaxFees" runat="server" TargetControlID="txtFees" FilterMode="ValidChars" ValidChars="0123456789."></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Activity Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                                            <label data-on="Started" data-off="Stopped" for="rdActive"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <%--  <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validate();"
                                                OnClick="btnSubmit_Click" TabIndex="12" CssClass="btn btn-primary" ValidationGroup="submit" />--%>
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validate();"
                                                            TabIndex="12" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="13"
                                                            CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="submit" />

                                                    </div>


                                                    <div class="col-12">

                                                        <asp:Panel ID="pnlForeign" runat="server" ScrollBars="Auto">
                                                            <asp:ListView ID="lvForeign" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid">
                                                                        <h6>PHD Students Registration Form Activity</h6>
                                                                        <table class="table table-hover table-bordered">
                                                                            <tr class="bg-light-blue">
                                                                                <th>Edit</th>
                                                                                <th>Admission Batch </th>
                                                                                <th>Start Date </th>
                                                                                <th>End Date </th>
                                                                                <th>Registration Fees </th>
                                                                                <th>Status</th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnForeignEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("ACTIVITYNO") %>' ImageUrl="~/Images/edit.png" OnClick="btnForeignEdit_Click" TabIndex="10" ToolTip="Edit Record" />
                                                                        </td>
                                                                        <td><%# Eval("ADMBATCH")%></td>
                                                                        <td><%# (Eval("FROM_DATE").ToString() != string.Empty) ? (Eval("FROM_DATE","{0:dd-MMM-yyyy}")) : Eval("FROM_DATE" ,"{0:dd-MMM-yyyy}") %></td>
                                                                        <td><%# (Eval("TO_DATE").ToString() != string.Empty) ? (Eval("TO_DATE", "{0:dd-MMM-yyyy}")) : Eval("TO_DATE", "{0:dd-MMM-yyyy}")%></td>
                                                                        <td>
                                                                            <%#Eval("REGISTRATION_FEES") %>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("STATUS"))==1 ? "Started" : "Stopped" %>' ForeColor=' <%# Convert.ToInt32(Eval("STATUS"))==1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>

                            <%-- <div class="tab-pane fade" id="tab_2">
                    

                           </div>--%>
                        </div>
                    </div>
                    <div id="divMsg" runat="server">
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }


        function validate() {


            $('#hfdActive').val($('#rdActive').prop('checked'));





            var idtxtweb = $("[id$=txtStartDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Start Date', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }


            var idtxtweb = $("[id$=txtEndDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter End Date', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;

            }
            var fees = document.getElementById('<%=txtFees.ClientID%>').value;
            if (fees == "")
            {
                alert('Please Enter Registration Fees.');
                document.getElementById('<%=txtFees.ClientID%>').focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>



</asp:Content>
