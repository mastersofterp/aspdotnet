<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnswerSheetIssue.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_ANSWERSHEET_AnswerSheetIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>--%>

    <%--   <script src="../../jquery/jquery-1.10.2.js"></script>--%>
    <script language="javascript" type="text/javascript">

        function validateAnswersheet(txt) {

            try {
                if (txt.value != '') {
                    var totalsheet = document.getElementById('ctl00_ContentPlaceHolder1_lblTotBal').value;
                    if (parseFloat(txt.value) <= parseFloat(totalsheet)) {
                        var remainsheet = totalsheet - txt.value;
                        document.getElementById('<%=txtBalance.ClientID%>').value = remainsheet;
                    }
                    else {

                        alert('value must not be greater than total');
                        document.getElementById('<%=txtBalance.ClientID%>').value = 0;
                    }

                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>
   <%-- <script>
        $(function () {

            $('#table2').DataTable({

            });
        });

    </script>--%>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
   <%-- <p class="page_help_text">
        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
    </p>--%>
    <style>
        #ctl00_ContentPlaceHolder1_ceStartDate_popupDiv, ctl00_ContentPlaceHolder1_CalendarExtender2_popupDiv {
            z-index: 100;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSession"
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
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ANSWERSHEET ISSUE & RECEIVE FOR EVALUATION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    
                               <%-- <div class="row">--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                             <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>session</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please select Session" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please select Session" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                             <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>session</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please select Session" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valSession1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please select Session" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="valDegree1" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please select Degree" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please select Degree" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="valBranch1" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please select Branch" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please select Branch" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="4" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="valScheme1" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please select Scheme" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please select Scheme" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                           <%-- <label>Semester</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valSemester1" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please select Semester" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valSemester" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please select Semester" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                          <%--  <label>Exam Type</label>--%>
                                             <asp:Label ID="lblDYtxtExamType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">RE-Valuation</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valExamType1" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please select Exam Type" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valExamType" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please select Exam Type" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Exam </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExam" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="7" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valExam1" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please select Exam" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valExam" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please select Exam" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                          <%--  <label>Course Name </label>--%>
                                            <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourses" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="8" OnSelectedIndexChanged="ddlCourses_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="valCourses1" runat="server" ControlToValidate="ddlCourses"
                                            Display="None" ErrorMessage="Please select Course Name" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="valCoures" runat="server" ControlToValidate="ddlCourses"
                                            Display="None" ErrorMessage="Please select Course Name" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                               <%-- </div>--%>
                                <div runat="server" id="tblFacdetai" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Evaluator Name</label>
                                            </div>
                                            <asp:HiddenField ID="hdissuerid" runat="server" />
                                            <asp:DropDownList ID="ddlFaculty" AppendDataBoundItems="True" runat="server" ValidationGroup="submit"
                                                Width="90%" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty"
                                                Display="None" ErrorMessage="Please Enter Faculty Name" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Issuer Name</label>
                                            </div>
                                            <asp:TextBox ID="txtIssuerName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="11" />

                                            <asp:RequiredFieldValidator ID="rfvIssuerName" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter Issuer Name" ControlToValidate="txtIssuerName"
                                                Display="None" ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Total Received</label>
                                            </div>
                                            <asp:TextBox ID="lblTotRecv" runat="server" CssClass="form-control" MaxLength="100" Enabled="false" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Total Balance</label>
                                            </div>
                                            <asp:TextBox ID="lblTotBal" runat="server" CssClass="form-control" MaxLength="100" Enabled="false" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Quantity Issued</label>
                                            </div>
                                            <asp:TextBox ID="txtAnsIssue" runat="server" CssClass="form-control" MaxLength="100" TabIndex="12" onblur="return validateAnswersheet(this);" />
                                            <asp:RequiredFieldValidator ID="rfvAnsIssue" runat="server" ControlToValidate="txtAnsIssue"
                                                Display="None" ErrorMessage="Please Enter Answersheet Issue" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterType="Numbers" TargetControlID="txtAnsIssue" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Quantity Balance</label>
                                            </div>
                                            <asp:TextBox ID="txtBalance" runat="server" CssClass="form-control" MaxLength="100" TabIndex="13" onkeydown="javascript:return false;" />
                                            <asp:RequiredFieldValidator ID="rfvbalance" runat="server" ControlToValidate="txtBalance"
                                                Display="None" ErrorMessage="Please Calculate Qty Balance Sheet." ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                FilterType="Numbers" TargetControlID="txtBalance" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Bundle No</label>
                                            </div>
                                            <asp:TextBox ID="txtBundle" runat="server" CssClass="form-control" MaxLength="100" TabIndex="14" />
                                            <asp:RequiredFieldValidator ID="rfvBundle" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter Bundle No" ControlToValidate="txtBundle"
                                                Display="None" ValidationGroup="submit" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                FilterType="Numbers" TargetControlID="txtBundle" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Issue Date</label>
                                            </div>
                                            <div class="form-group input-group" style="z-index: 1000;">
                                                <div class="input-group-addon" style="z-index: 1000;">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="15" ValidationGroup="submit"
                                                    Value='<%# Eval("ISSUER_DATE")%>' />
                                                <ajaxToolKit:CalendarExtender ID="calID" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgID" TargetControlID="txtIssueDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meIssueDate" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtIssueDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mrvId" runat="server" ControlExtender="meIssueDate"
                                                    ControlToValidate="txtIssueDate" Display="None" EmptyValueMessage="Please Enter Issuer Date"
                                                    ErrorMessage="Please Enter Issuer Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid"
                                                    IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Submit" />
                                                <asp:RequiredFieldValidator ID="rfvIssuerDate" runat="server" ControlToValidate="txtIssueDate"
                                                    Display="None" ErrorMessage="Please select Issuer Date." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <asp:HiddenField ID="hdIssuerdate" runat="server" Value='<%# Eval("ISSUER_DATE") %>' />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="ReceiveDT" visible="false" style="z-index: 1">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <asp:Label ID="lblReceiveDT" runat="server" Style="font-family: Sans-Serif; font-size: small;" Text="Receive Date : " Visible="false"></asp:Label>
                                            </div>
                                            <div class="form-group input-group" style="z-index: 1000;">
                                                <div class="input-group-addon" style="z-index: 1000;">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtRecdDate" runat="server" CssClass="form-control" TabIndex="4" ValidationGroup="submit"
                                                    Value='<%# Eval("RECEIVER_DATE")%>' OnTextChanged="txtRecdDate_TextChanged" AutoPostBack="true" Style="z-index: 100;" />
                                                <ajaxToolKit:CalendarExtender ID="CalID1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgID1" TargetControlID="txtRecdDate" />
                                                <ajaxToolKit:MaskedEditExtender ID="meReceiverDate" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtRecdDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mrvId1" runat="server" ControlExtender="meReceiverDate"
                                                    ControlToValidate="txtRecdDate" Display="None" EmptyValueMessage="Please Enter Receiver Date"
                                                    ErrorMessage="Please Enter Receiver Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid"
                                                    IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Submit" />
                                                <asp:RequiredFieldValidator ID="rfvRecdDate" runat="server" ControlToValidate="txtRecdDate"
                                                    Display="None" ErrorMessage="Please select Receiver Date." ValidationGroup="submit" Enabled="false"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="ReceiverName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <asp:Label ID="lblReceiveName" runat="server" Style="font-family: Sans-Serif; font-size: small;" Text="Receiver Name : " Visible="false"></asp:Label>
                                            </div>
                                            <asp:TextBox ID="txtReceiveName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2" />
                                            <asp:RequiredFieldValidator ID="RfvReceiveName" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter Receiver Name" ControlToValidate="txtReceiveName"
                                                Display="None" ValidationGroup="submit" Enabled="false" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" MaxLength="100" TextMode="MultiLine" TabIndex="16" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <asp:TextBox ID="txtIssuerId" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2" Visible="false" />
                                            <asp:Label ID="lblRecdId" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="9"
                                        CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="show" />
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="12"
                                        ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="13"
                                        CssClass="btn btn-info" OnClick="btnReport_Click" ValidationGroup="submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="15" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="valSummary" DisplayMode="List" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="submit" />

                                    &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="show" />
                                </div>

                                <asp:Panel ID="pnlSession" runat="server" Width="100%">
                                    <div class="col-12">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">
                                            <asp:Repeater ID="lvStudentsIssuer" runat="server">
                                                <HeaderTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Marksheet Issuer Faculty List</h5>
                                                    </div>

                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action </th>
                                                            <th>Course Name </th>
                                                            <th>Name Of Evaluator </th>
                                                            <th>Bundle No. </th>
                                                            <th>Quantity </th>
                                                            <th>Issue Date </th>
                                                            <th>Receive Date </th>
                                                        </tr>
                                                    </thead>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ISSUERID") %>' ImageUrl="~/images/Edit.png" OnClick=" btnEdit_Click" ToolTip="Edit Record" AlternateText="Edit Record" />
                                                        </td>
                                                        <td><%# Eval("COURSENAME")%>
                                                            <asp:HiddenField ID="hdCourseno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                        </td>
                                                        <td><%# Eval("EVALUATOR")%>
                                                            <asp:HiddenField ID="hdfEvaluator" runat="server" Value='<%# Eval("EVALUATOR") %>' />
                                                        </td>
                                                        <td><%# Eval("BUNDLE_NO")%>
                                                            <asp:HiddenField ID="hdBundle" runat="server" Value='<%# Eval("BUNDLE_NO") %>' />
                                                        </td>
                                                        <td><%# Eval("QUANTITY")%>
                                                            <asp:HiddenField ID="hdQty" runat="server" Value='<%# Eval("QUANTITY") %>' />
                                                        </td>
                                                        <td><%# Eval("ISSUER_DATE", "{0:dd/M/yyyy}")%>
                                                            <asp:HiddenField ID="hdIssuerDate" runat="server" Value='<%# Eval("ISSUER_DATE") %>' />
                                                        </td>
                                                        <td><%# Eval("RECEIVER_DATE", "{0:dd/M/yyyy}")%>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("RECEIVER_DATE") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%--<asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ISSUERID") %>' ImageUrl="~/images/edit.gif" OnClick=" btnEdit_Click" AlternateText="Edit Record" ToolTip="Edit Record" />--%>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ISSUERID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="1" />
                                                        </td>
                                                        <td><%# Eval("COURSENAME")%>
                                                            <asp:HiddenField ID="hdCourseno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                        </td>
                                                        <td><%# Eval("EVALUATOR")%>
                                                            <asp:HiddenField ID="hdfEvaluator" runat="server" Value='<%# Eval("EVALUATOR") %>' />
                                                        </td>
                                                        <td><%# Eval("BUNDLE_NO")%>
                                                            <asp:HiddenField ID="hdBundle" runat="server" Value='<%# Eval("BUNDLE_NO") %>' />
                                                        </td>
                                                        <td><%# Eval("QUANTITY")%>
                                                            <asp:HiddenField ID="hdQty" runat="server" Value='<%# Eval("QUANTITY") %>' />
                                                        </td>
                                                        <td><%# Eval("ISSUER_DATE", "{0:dd/M/yyyy}")%>
                                                            <asp:HiddenField ID="hdIssuerDate" runat="server" Value='<%# Eval("ISSUER_DATE") %>' />
                                                        </td>
                                                        <td><%# Eval("RECEIVER_DATE", "{0:dd/M/yyyy}")%>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("RECEIVER_DATE") %>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                          
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                    </div>
                </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />

        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>



