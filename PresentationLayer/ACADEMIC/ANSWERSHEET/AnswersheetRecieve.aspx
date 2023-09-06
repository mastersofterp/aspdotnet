<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="AnswersheetRecieve.aspx.cs" Inherits="ACADEMIC_ANSWERSHEET_AnswersheetRecieve" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script>
        $(function () {

            $('#table2').DataTable({

            });
        });

    </script>--%>


    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
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
                            <h3 class="box-title">ANSWERSHEET COLLECTION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:RadioButtonList ID="rblDetails" runat="server" RepeatDirection="Horizontal"  AutoPostBack="true" OnSelectedIndexChanged="rblDetails_SelectedIndexChanged">
                                            <asp:ListItem Text="All Selection" Selected="True" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Date Wise " Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <div id="answerpnl1" class="row" visible="true" runat="server">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                           <%-- <label>College & Scheme</label>--%>
                                             <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="save" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvShowClg" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                           <%-- <label>Session</label>--%>
                                             <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valSessin" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please select Session" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please select Session" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="ExamDate" visible="false" runat="server">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                           <%-- <label>Exam Date</label>--%>
                                             <asp:Label ID="lblDYtxtExamDate" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                       <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgID" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtExamDate" runat="server" TabIndex="4" ValidationGroup="submit"
                                                Value='<%# Eval("DATE")%>' />
                                            <ajaxToolKit:CalendarExtender ID="calID" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgID" TargetControlID="txtExamDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meIssueDate" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                TargetControlID="txtExamDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mrvId" runat="server" ControlExtender="meIssueDate"
                                                ControlToValidate="txtExamDate" Display="None" EmptyValueMessage="Please Enter Issuer Date"
                                                ErrorMessage="Please Enter Issuer Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid"
                                                IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Submit" />
                                            <asp:RequiredFieldValidator ID="rfvIssuerDate" runat="server" ControlToValidate="txtExamDate"
                                                Display="None" ErrorMessage="Please select AnsSheet Issuer Date." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdIssuerdate" runat="server" Value='<%# Eval("DATE")%>' />
                                        </div>
                                    </div>
                                </div>
                                <div class="row " id="answerPnl2" visible="true" runat="server">
                                    
                                   
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please select Degree" ValidationGroup="submit1"
                                            InitialValue="0" SetFocusOnError="true" />
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

                                        <asp:RequiredFieldValidator ID="valBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please select Branch" ValidationGroup="submit1"
                                            InitialValue="0" SetFocusOnError="true" />
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

                                        <asp:RequiredFieldValidator ID="valScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please select Scheme" ValidationGroup="submit1"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                              <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Semester</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valSemester" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please select Semester" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please select Semester" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYtxtExam" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Exam</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlExam" AppendDataBoundItems="true" runat="server" AutoPostBack="true"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="6" OnSelectedIndexChanged="ddlExam_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valExam" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please select Exam" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlExam"
                                            Display="None" ErrorMessage="Please select Exam" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                              <asp:Label ID="lblDYtxtExamType" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Exam Type</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" AppendDataBoundItems="true" runat="server"
                                            CssClass="form-control" data-select2-enable="true" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">RE-Valuation</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="valExamType" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please select Exam Type" ValidationGroup="submit"
                                            InitialValue="0" SetFocusOnError="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please select Exam Type" ValidationGroup="show"
                                            InitialValue="0" SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>
                             <div class="col-12 btn-footer" id="DateButton" visible="false" runat="server">
                                    <asp:Button ID="btnShow1" TabIndex="8" runat="server" Text="Show"
                                        ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnShow1_Click" />
                                    <asp:Button ID="btnUpdate" TabIndex="9" runat="server" Text="Update"
                                        CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnReport1" runat="server" Text="Report" TabIndex="10"
                                        CssClass="btn btn-info" OnClick="btnReport1_Click" ValidationGroup="submit"/>
                                    <asp:Button ID="btnCancelDate" runat="server" Text="Cancel" TabIndex="11" CssClass="btn btn-warning" OnClick="btnCancelDate_Click" />
                          
                               </div>
                             <div class="col-12 btn-footer" id="AllSelectionButton" runat="server">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="12"
                                        CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="show" />

                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="13"
                                        CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="14" Visible="false"
                                        CssClass="btn btn-info" OnClick="btnReport_Click1" ValidationGroup="submit" />
                                 <%--   <asp:Button ID="btnEvaluationReport1" runat="server" Text="Eval Report" TabIndex="15" CssClass="btn btn-info" OnClick="btnEvaluationReport1_Click" ValidationGroup="submit" />--%>    <%-- Commented n 23-2-23--%>
                                    <asp:Button ID="btnClear" runat="server" Text="Cancel" TabIndex="16" CssClass="btn btn-warning" OnClick="btnClear_Click" />
                               <%--  Commented on 23-2-23 by Injamam--%>
                                   <%-- <asp:ValidationSummary ID="valSummary" DisplayMode="List" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="submit" />
                                 <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="show" />--%>
                                </div>
                             <%--  Added on 23-2-23 by Injamam--%>
                                    <asp:ValidationSummary ID="valSummary" DisplayMode="List" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="submit" />
                                 <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="show" />
                             <asp:Panel ID="pnlStudent" runat="server">
                                <%-- <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;" visible="false">--%>
                                             <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="table2">
                                            <asp:Repeater ID="lvStudents" runat="server">
                                                <HeaderTemplate>
                                                    <%--<div class="btn-footer">
                                                        <h5>SESSION</h5>
                                                    </div>--%>
                                                              <thead class="bg-light-blue" >
                                                            <tr id="itemPlaceholder" runat="server" >
                                                                <th>Sr No
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th>Registered Student
                                                                </th>
                                                                <th>AnswerSheet Received
                                                                </th>
                                                                <th>AnswerSheet Submitted
                                                                </th>
                                                                <th>Date 
                                                                </th>
                                                                <th>Remark
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="tr1" runat="server" />
                                                        </tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>

                                                        <td>
                                                            <asp:Label ID="lblsR" runat="server" Font-Bold="true" />

                                                            <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                                            <asp:HiddenField runat="server" ID="hdfExamType" Value='<%# Eval("ExamType") %>' />

                                                        </td>
                                                        <td>


                                                            <%# Eval("COURSENAME")%>
                                                            <asp:HiddenField runat="server" ID="hdfcoursename" Value='<%# Eval("COURSENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtRegStud"
                                                                Text='<%# Eval("TOT_STUD")%>' Enabled="false" />

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnsRced" runat="server" Value='<%# Eval("TOT_ANS_RECD")%>' ToolTip='<%# Eval("TOT_STUD")%>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FteAnsrecd" runat="server"
                                                                FilterType="Numbers" TargetControlID="txtAnsRced" />
                                                            <asp:RangeValidator ID="rvansered" runat="server" ControlToValidate="txtAnsRced"
                                                                ValidationGroup="MrksheetRcev" SetFocusOnError="true" EnableClientScript="true"></asp:RangeValidator>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtAnsSub"
                                                                Text='<%# Eval("SUBMIT_STAFF")%>' />

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtExamDate" runat="server" TabIndex="4" ValidationGroup="submit"
                                                                Value='<%# Eval("DATE")%>' />
                                                            <ajaxToolKit:CalendarExtender ID="calID" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                PopupButtonID="imgID" TargetControlID="txtExamDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meIssueDate" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                                TargetControlID="txtExamDate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mrvId" runat="server" ControlExtender="meIssueDate"
                                                                ControlToValidate="txtExamDate" Display="None" EmptyValueMessage="Please Enter Issuer Date"
                                                                ErrorMessage="Please Enter Issuer Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid"
                                                                IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Submit" />
                                                            <asp:RequiredFieldValidator ID="rfvIssuerDate" runat="server" ControlToValidate="txtExamDate"
                                                                Display="None" ErrorMessage="Please select AnsSheet Issuer Date." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <asp:HiddenField ID="hdIssuerdate" runat="server" Value='<%# Eval("DATE")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRemark" runat="server" Value='<%# Eval("REMARK")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblsR" runat="server" Font-Bold="true" />

                                                            <%#(((RepeaterItem)Container).ItemIndex+1).ToString()%>
                                                            <asp:HiddenField runat="server" ID="hdfExamType" Value='<%# Eval("ExamType") %>' />

                                                        </td>
                                                        <td>


                                                            <%# Eval("COURSENAME")%>
                                                            <asp:HiddenField runat="server" ID="hdfcoursename" Value='<%# Eval("COURSENO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtRegStud"
                                                                Text='<%# Eval("TOT_STUD")%>' Enabled="false" />

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnsRced" runat="server" Value='<%# Eval("TOT_ANS_RECD")%>' ToolTip='<%# Eval("TOT_STUD")%>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FteAnsrecd" runat="server"
                                                                FilterType="Numbers" TargetControlID="txtAnsRced" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtAnsSub"
                                                                Text='<%# Eval("SUBMIT_STAFF")%>' />

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtExamDate" runat="server" TabIndex="4" ValidationGroup="submit"
                                                                Value='<%# Eval("DATE")%>' />
                                                            <ajaxToolKit:CalendarExtender ID="calID" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                PopupButtonID="imgID" TargetControlID="txtExamDate" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meIssueDate" runat="server" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate"
                                                                TargetControlID="txtExamDate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mrvId" runat="server" ControlExtender="meIssueDate"
                                                                ControlToValidate="txtExamDate" Display="None" EmptyValueMessage="Please Enter Issuer Date"
                                                                ErrorMessage="Please Enter Issuer Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid"
                                                                IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Submit" />
                                                            <asp:RequiredFieldValidator ID="rfvIssuerDate" runat="server" ControlToValidate="txtExamDate"
                                                                Display="None" ErrorMessage="Please select AnsSheet Issuer Date." ValidationGroup="MrksheetRcev"></asp:RequiredFieldValidator>
                                                            <asp:HiddenField ID="hdIssuerdate" runat="server" Value='<%# Eval("DATE")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRemark" runat="server" Value='<%# Eval("REMARK")%>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                           
                                                </FooterTemplate>
                                            </asp:Repeater>
                                             </table>
                                     <%--</div>--%>
                                        </asp:Panel>
                       
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnReport1" />
          <%--  <asp:PostBackTrigger ControlID="btnEvaluationReport1" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
</asp:Content>
