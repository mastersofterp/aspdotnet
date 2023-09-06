    <%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TestMaster.aspx.cs" Inherits="ITLE_TestMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../Content/jquery.dataTables.js" type="text/javascript"></script>--%>


    <%--<script type="text/javascript" charset="utf-8">
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

    </script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .list-group .list-group-item .sub-label {
            float: initial;
        }



        .list-group .list-group-item .sub-label {
            float: initial;
        }

    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#Table3').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#Table3').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#Table3').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#Table3').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#Table3').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#Table3').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#Table3').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#Table3').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#Table3').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#Table3').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });


        //$(document).ready(function () {
        //    $("[id$='chkStud']").live('click', function () {
        //        $("[id$='cbHead']").attr('checked', this.checked);
        //    });
        //});
    </script>
    

    <script type="text/javascript">
        function toggleExpansion(divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";

            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";

            }
        }
        

        function CheckAll(checkid) {
            var updateButtons = $('#Table3 input[type=checkbox]');
            if ($(checkid).children().is(':checked')) {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", true);
                    }
                });
            }
            else {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", false);
                    }
                });
            }
        }
    </script>
    <script type="text/javascript">
        function SelectAllTrainer(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkSelect')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else

                            e.checked = false;
                    }
                }
            }
        }
            </script>
     
    <asp:UpdatePanel ID="updCreateQuestion" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEST MASTER</h3>
                        </div>

                        <div class="box-body">

                            <asp:Panel ID="pnlTestMaster" runat="server">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <asp:Panel ID="pnlTest" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12 col-lg-6" style="box-shadow: 0 0px 5px rgba(0,0,0,0.2); padding: 10px; border-radius: 5px;">
                                                            <div class="row">

                                                                <%--  <div class="form-group col-lg-3 col-md-6 col-12" id="trSession" runat="server" visible="true">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>session</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Session">
                                                                        <asp:ListItem Text="Please Select" Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>--%>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Test Name</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTestName" CssClass="form-control" runat="server" ValidationGroup="Submit"></asp:TextBox>
                                                                    <%--  <asp:RequiredFieldValidator ID="rvTestName" runat="server" ControlToValidate="txtTestName"
                                                                                                Display="None" ErrorMessage="Enter Test Name" SetFocusOnError="true"
                                                                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>


                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                        ControlToValidate="txtTestName" Display="none"
                                                                        ErrorMessage="Enter Test Name" SetFocusOnError="True"
                                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Start Date</label>
                                                                    </div>
                                                                    <div class="input-group date">
                                                                        <div class="input-group-addon">
                                                                            <i id="imgCalStartDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtStartDt" runat="server" CssClass="form-control" AutoPostBack="false"></asp:TextBox>

                                                                        <ajaxToolKit:CalendarExtender ID="ceStartDt" runat="server" Format="dd/MM/yyyy"
                                                                            TargetControlID="txtStartDt"
                                                                            PopupButtonID="imgCalStartDt" Enabled="true" EnableViewState="true">
                                                                        </ajaxToolKit:CalendarExtender>
                                                                        <ajaxToolKit:MaskedEditExtender ID="meeSchduleDt" runat="server" TargetControlID="txtStartDt"
                                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                                        <ajaxToolKit:MaskedEditValidator ID="mevSchduleDt" runat="server"
                                                                            ControlExtender="meeSchduleDt" ControlToValidate="txtStartDt"
                                                                            InvalidValueMessage="Schdule Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                            Display="None" TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty"
                                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="true">
                                                                        </ajaxToolKit:MaskedEditValidator>

                                                                      
                                                                            <asp:RequiredFieldValidator ID="rvStartDate" runat="server" ControlToValidate="txtStartDt" Display="none"
                                                                                ErrorMessage="Enter Start Date" SetFocusOnError="True" ValidationGroup="submit">
                                                                            </asp:RequiredFieldValidator>
                                                                       
                                                                    </div>
                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>End Date</label>
                                                                    </div>
                                                                    <div class="input-group date">
                                                                        <div class="input-group-addon">
                                                                            <i id="imgCalEndDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtEndDt" runat="server" CssClass="form-control" AutoPostBack="false" OnTextChanged="txtEndDt_TextChanged" />
                                                                        <%--<span class="input-group-addon"><i><a href="#"><i id="imgCalEndDt" class="fa fa-calendar"></i></span></a></i> --%>
                                                                        <ajaxToolKit:CalendarExtender ID="ceEndDt" runat="server" Format="dd/MM/yyyy"
                                                                            TargetControlID="txtEndDt" EnableViewState="true"
                                                                            PopupButtonID="imgCalEndDt" Enabled="true">
                                                                        </ajaxToolKit:CalendarExtender>
                                                                        <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server" TargetControlID="txtEndDt"
                                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                                        <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server" ControlExtender="meeFromdt"
                                                                            ControlToValidate="txtEndDt" TooltipMessage="Please Enter End Date"
                                                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                            Display="None" EmptyValueBlurredText="Empty"
                                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit"
                                                                            SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                                        <asp:RequiredFieldValidator ID="rvEndDate" runat="server" ControlToValidate="txtEndDt" Display="none"
                                                                            ErrorMessage="Enter End Date" SetFocusOnError="True" ValidationGroup="submit">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Test Start Time <small>(24 hour format)</small></label>
                                                                    </div>
                                                                    <asp:TextBox ID="TxtExamStartTime" runat="server" CssClass="form-control"
                                                                        Text=""></asp:TextBox>
                                                                    <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                                                        TargetControlID="TxtExamStartTime" WatermarkText="HH:MM:SS" />
                                                                    <%--WatermarkCssClass="watermarked"--%>
                                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtenderStartTime" runat="server" TargetControlID="TxtExamStartTime"
                                                                        Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                        OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" ErrorTooltipEnabled="True" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidatorStartTime" runat="server"
                                                                        ControlExtender="MaskedEditExtenderStartTime" ControlToValidate="TxtExamStartTime"
                                                                        IsValidEmpty="False" EmptyValueMessage="Time is required" InvalidValueMessage="Time is invalid"
                                                                        Display="none" TooltipMessage="Input a time in hh:mm:ss format" EmptyValueBlurredText=""
                                                                        InvalidValueBlurredMessage="24 Hour format" ValidationGroup="submit" />


                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Test End Time <small>(24 hour format)</small></label>
                                                                    </div>
                                                                    <asp:TextBox ID="TxtExamEndTime" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                                    <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server"
                                                                        TargetControlID="TxtExamEndTime" WatermarkText="HH:MM:SS" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtenderEndTime" runat="server" TargetControlID="TxtExamEndTime"
                                                                        Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                        OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" ErrorTooltipEnabled="True" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidatorEndTime" runat="server" ControlExtender="MaskedEditExtenderEndTime"
                                                                        ControlToValidate="TxtExamEndTime" IsValidEmpty="False" EmptyValueMessage="Time is required"
                                                                        InvalidValueMessage="Time is invalid" Display="none" TooltipMessage="Input a time in hh:mm:ss format"
                                                                        EmptyValueBlurredText="" InvalidValueBlurredMessage="24 Hour format" ValidationGroup="submit" />

                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Test Duration <small>(HH:MM:SS)</small></label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTestDuration" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeSchduleTime" runat="server" TargetControlID="txtTestDuration"
                                                                        Mask="99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                        OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="False" ErrorTooltipEnabled="True" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mevSchduleTime" runat="server" ControlExtender="meeSchduleTime"
                                                                        ControlToValidate="txtTestDuration" IsValidEmpty="False" EmptyValueMessage="Time is required"
                                                                        InvalidValueMessage="Time is invalid" Display="none" TooltipMessage="Input a time"
                                                                        EmptyValueBlurredText="" InvalidValueBlurredMessage="*" ValidationGroup="submit" />
                                                                    <%-- <asp:Label ID="lblTime" runat="server" Text="Tip: Type 'A' or 'P' to switch AM/PM"></asp:Label>--%>
                                                                    <asp:RequiredFieldValidator ID="rvTestDuration" runat="server" ControlToValidate="txtTestDuration" Display="none"
                                                                        ErrorMessage="Enter Test Duration" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>No of Attempts Allowed</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAttempts" runat="server" CssClass="form-control" MaxLength="1"
                                                                        Text="1" ToolTip="Enter 0 for unlimited attempts or 1 for limited attempts">
                                                                    </asp:TextBox>

                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                        TargetControlID="txtAttempts"
                                                                        FilterType="Custom"
                                                                        FilterMode="ValidChars"
                                                                        ValidChars="01">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                    <label>(Enter 0 for unlimited attempts)</label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                        ControlToValidate="txtTestDuration" Display="None"
                                                                        ErrorMessage="Enter Test Duration" SetFocusOnError="True" ValidationGroup="submit">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                                        ControlToValidate="txtAttempts" ValidationExpression="\d+" 
                                                                        EnableClientScript="true" SetFocusOnError="True" Display="none"
                                                                        ErrorMessage="Please enter numbers only" runat="server" ValidationGroup="submit" />

                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="trShowRandom" visible="false" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Show Random</label>
                                                                    </div>
                                                                    <asp:CheckBox ID="cbRandom" runat="server" Checked="true" />
                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="div_ShowResult">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Show Result After Test &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="cbShowResult" runat="server" AutoPostBack="true" OnCheckedChanged="cbShowResult_CheckedChanged" /></label>
                                                                        <label class="mt-3">Apply Full Randomization &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkFullRandomeze" runat="server" /></label>
                                                                        <label class="mt-3" runat="server"  id="lblshowanssheet" visible="false" >Show Answer Sheet After Test &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkshowanswerkey" runat="server"/></label>
                                                                  
                                                                    </div>

                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Add Questions into Test </label>
                                                                    </div>
                                                                    <asp:RadioButtonList ID="rbtnRandomQuestion" runat="server" RepeatDirection="Horizontal"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="rbtnRandomQuestion_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Show Random" Value="R" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Select Manually" Value="NR"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="divswitch" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Apply Alerts For Switching Exam Window</label>
                                                                    </div>
                                                                    <asp:RadioButtonList ID="rdoAllowMalPactice" runat="server" RepeatDirection="Horizontal"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="rdoAllowMalPactice_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="divMalfunction" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Enter Window Switching Count :</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtMalFunction" runat="server" CssClass="form-control"
                                                                        MaxLength="1"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12" id="divapplypass" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Apply Password For Attempts the Exam</label>
                                                                    </div>
                                                                    <asp:RadioButtonList ID="rdoAllowPassword" runat="server" RepeatDirection="Horizontal"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="rdoAllowPassword_SelectedIndexChanged" >
                                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                  <div class="form-group col-lg-6 col-md-6 col-12" id="divpassword" runat="server" >
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label>Enter Password:</label>
                                                                    </div>
                                                                    <asp:TextBox ID="Txtpassword" runat="server" CssClass="form-control"    AutoPostBack="true"
                                                                        MaxLength="20"></asp:TextBox>
                                                                   
                                                                </div>
                                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                                    <asp:ImageButton ID="imgAddQuestions" ImageUrl="~/IMAGES/action_down.png" Width="25px"
                                                                        Height="35px" AlternateText="Add Marks" runat="server" OnClick="imgAddQuestions_Click" />
                                                                    <b>Transfer Questions</b> <span style="color: Red">(Click here when you select/deselect
                                                                                                question)</span>
                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Total No of Questions For Test</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTotQue" runat="server" CssClass="form-control" MaxLength="2"
                                                                        ReadOnly="true"
                                                                        AutoPostBack="true" OnTextChanged="txtTotQue_TextChanged"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                        ControlToValidate="txtTotQue" SetFocusOnError="True"
                                                                        ErrorMessage="Please Transfer Questions by clicking on Arrow button"
                                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                                        ControlToValidate="txtTotQue" SetFocusOnError="True" Display="none"
                                                                        ValidationExpression="\d+"  EnableClientScript="true"
                                                                        ErrorMessage="Please enter numbers only" runat="server" ValidationGroup="submit" />
                                                                </div>
                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Total Marks For Test </label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTotMarks" ReadOnly="true" runat="server" CssClass="form-control"
                                                                        MaxLength="2"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                        ControlToValidate="txtTotMarks" Display="none"
                                                                        ErrorMessage="Enter Total Test Marks" SetFocusOnError="True"
                                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                                    </div>
                                                                    <asp:HiddenField ID="hdnTo" runat="server" />
                                                                </div>

                                                                <div class="col-12 btn-footer">
                                                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                                                        ValidationGroup="submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" />

                                                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-12 col-lg-6">
                                                            <div id="pnlTopic" runat="server" visible="true" style="display: block;">
                                                                <div class="sub-heading">
                                                                    <h5>Random Questions For Test</h5>
                                                                </div>

                                                                <asp:Panel ID="pnlTopicList" runat="server">
                                                                    <asp:ListView ID="lsvbindquestion" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="Table2">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>SrNo
                                                                                            </th>
                                                                                            <th>Topic
                                                                                            </th>
                                                                                            <th>Ratio                                                                
                                                                                            </th>
                                                                                            <th>Questions                                                                    
                                                                                            </th>
                                                                                            <th>Marks                                                                 
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
                                                                            <tr>
                                                                                <td>
                                                                                    <%# Container.DataItemIndex + 1%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("TOPIC")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtSelectQ" runat="server" MaxLength="2"
                                                                                        CssClass="form-control"
                                                                                        onkeyup="validateNumeric(this);"
                                                                                        Text='<%# Eval("QuestionRatio") %>' ToolTip='<%# Eval("TOPIC") %>'
                                                                                        onBlur="checkVal();">
                                                                                    </asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"
                                                                                        Enabled="false"
                                                                                        Text='<%# Eval("Questions")%>'></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtMarks" runat="server" CssClass="form-control"
                                                                                        Enabled="false"
                                                                                        Text='<%#Eval("MARKS_FOR_QUESTION")%>'></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <EmptyDataTemplate>
                                                                            <p class="text-center text-bold">
                                                                                <asp:Label ID="lblError" runat="server" SkinID="Errorlbl"
                                                                                    Text="No Record Found"></asp:Label>
                                                                            </p>
                                                                        </EmptyDataTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>

                                                            <div class="form-group col-lg-6 col-md-6 col-12 mb-4">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Select Section</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                                    AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Section" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <asp:Panel ID="pnlSelectedQuestion" runat="server">
                                                                <asp:ListView ID="lvSelectedQuestions" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Selected Questions For List</h5>
                                                                        </div>
                                                                        <div class="table table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>Sr.No
                                                                                        </th>
                                                                                        <th>Question
                                                                                        </th>
                                                                                        <th>Topic
                                                                                        </th>
                                                                                        <th>Marks
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
                                                                        <tr>
                                                                            <td>
                                                                                <%# Container.DataItemIndex + 1%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("QUESTIONTEXT")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("TOPIC")%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblQMarks" Text='<%# Eval("MARKS_FOR_QUESTION")%>'
                                                                                    runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>

                                                            <div class="col-12" id="pnlQuestions" runat="server" visible="false">
                                                                <div class="sub-heading">
                                                                    <h5>Manual Questions For Test</h5>
                                                                </div>

                                                                <asp:Panel ID="pnlQuestions1" runat="server">
                                                                    <div class="table table-responsive">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="Table1">
                                                                            <asp:Repeater ID="lvQuestions" runat="server">
                                                                                <HeaderTemplate>
                                                                                    <thead class="bg-light-blue">
                                                                                        <th></th>
                                                                                        <th>Questions
                                                                                        </th>
                                                                                        <th>Topic                                                                  
                                                                                        </th>
                                                                                        <th>Total Marks                                                                    
                                                                                        </th>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <%-- <tr id="itemPlaceholder" runat="server" />--%>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="cbQuestionRow" runat="server"
                                                                                                ToolTip='<%# Eval("QUESTIONNO")%>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("QUESTIONTEXT")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("TOPIC")%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("MARKS_FOR_QUESTION")%>                                                        
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    </tbody>
                                                                                </FooterTemplate>
                                                                            </asp:Repeater>
                                                                        </table>
                                                                    </div>
                                                                </asp:Panel>
                                                            </div>

                                                            <div class="" id="PnlList" runat="server">
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>

                                                                <asp:Panel ID="PnlListview" runat="server">
                                                                    <asp:ListView ID="lvStudent" runat="server" >
                                                                        <LayoutTemplate>

                                                                            <table class="table table-striped table-bordered nowrap"  style="width: 100%" id="Table3">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:CheckBox ID="cbHead" runat="server" Checked="true"  OnCheckedChanged="cbHead_CheckedChanged1" 
                                                                                              onchange="CheckAll(this)"  /> <%--onclick="totAllIDs(this)"--%>
                                                                                        </th>
                                                                                        <th>RRN                                                                
                                                                                        </th>
                                                                                        <th>Student Name
                                                                                        </th>
                                                                                        <th>Mobile Number                                                                 
                                                                                        </th>
                                                                                        <th>Section                                                                    
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
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkStud" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("REGNO")%>                                                          
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("STUDNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("STUDENTMOBILE")%>                                                           
                                                                                </td>
                                                                                <%-- <td style="width: 7%; text-align: left">
                                                                                                        <%# Eval("EMAILID")%> 
                                                                                                    </td>--%>
                                                                                <td>
                                                                                    <%# Eval("SECTIONNAME")%>                                                          
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>

                                                            </div>

                                                        </div>

                                                        <div class="col-md-3">
                                                            <asp:HiddenField ID="hdfTot" runat="server" />
                                                            <asp:Label ID="LblTotalMarks" runat="server" onkeyup="return Calculate(this);"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnllvView" runat="server">
                                    <div class="col-12">
                                        <div class=" sub-heading">
                                            <h5>Create Test</h5>
                                        </div>
                                        <asp:Panel ID="pnlCourse" runat="server">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Course Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblCourseName" Font-Bold="true" runat="server"></asp:Label>
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </asp:Panel>


                                        <div class="row mt-3">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Create New </label>
                                                </div>
                                                <asp:Button ID="btnObjectiveTest" runat="server" OnClick="btnObjectiveTest_Click" CssClass="btn btn-primary"
                                                    Text="Objective Type Test" ToolTip="Click here to Add New Objective Type Test" />
                                                <asp:Button ID="btnDescriptiveTest" runat="server" OnClick="btnDescriptiveTest_Click" Text="Descriptive Type Test"
                                                    ToolTip="Click here to Add new Descriptive Type Test" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnViewTestReport" runat="server" OnClick="btnViewTestReport_Click"
                                                    Text="Test Report" CssClass="btn btn-primary" ToolTip="Click here for Test Report" />

                                            </div>
                                        </div>


                                        <asp:Panel ID="pnlTestList1" runat="server">
                                            <div class="sub-heading">
                                                <h5>Questions List</h5>
                                            </div>
                                            <asp:Panel ID="pnlTestList" runat="server">
                                                <asp:ListView ID="lvTest" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Delete
                                                                    </th>
                                                                    <th>Edit</th>
                                                                    <th>Test Name 
                                                                    </th>
                                                                    <th>Start Date                                                                   
                                                                    </th>
                                                                    <th>End Date                                                                     
                                                                    </th>
                                                                    <th>Test Duration                                                                
                                                                    </th>
                                                                    <th>Test Time                                                                     
                                                                    </th>
                                                                    <th>Test Type                                                                  
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
                                                            <td>
                                                                 <asp:ImageButton ID="btndelete" runat="server" AlternateText="Delete Record"
                                                                    CausesValidation="false" OnClick="btndelete_Click"
                                                                    CommandArgument='<%# Eval("TESTNO") %>' ImageUrl="~/Images/delete.png" OnClientClick="showConfirmDel(this); return false;"
                                                                    ToolTip="Delete Record" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record"
                                                                    CausesValidation="false" OnClick="btnEdit_Click"
                                                                    CommandArgument='<%# Eval("TESTNO") %>' ImageUrl="~/images/edit1.png"
                                                                    ToolTip="Edit Record" />
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="btnlnkSelect" runat="server" CommandArgument='<%# Eval("TESTNO") %>'
                                                                    CommandName='<%# Eval("TESTNO") %>' OnClick="btnlnkSelect_Click"
                                                                    Text='<%# Eval("TESTNAME")%>'
                                                                    ToolTip='<%# Eval("TEST_TYPE")%>'></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <%# Eval("STARTDATE","{0:dd-MMM-yyyy}")%>                                                         
                                                            </td>
                                                            <td>
                                                                <%# Eval("ENDDATE","{0:dd-MMM-yyyy}")%>                                                        
                                                            </td>
                                                            <td>
                                                                <%# Eval("TESTDURATION")%>                                                            
                                                            </td>
                                                            <td>
                                                                <%# Eval("STARTDATE", "{0:hh:mm:ss tt}")%>
                                                                                            -
                                                                                            <%# Eval("ENDDATE", "{0:hh:mm:ss tt}")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("TEST_TYPE")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>

                                        </asp:Panel>

                                    </div>
                                </asp:Panel>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="txtEndDt" />
            <asp:AsyncPostBackTrigger ControlID="txtStartDt" />--%>
            <%-- <asp:PostBackTrigger ControlID="btnSubmit" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

    <script type="text/javascript" language="javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = 0;
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }

        }
        function validateNumericBlanckCheck(txt, QuestionS) {
            if (txt.value == '') {
                txt.value = 0;
                return;
            }
            else if (Number(txt.value) > Number(QuestionS.value)) {
                txt.value = 0;
                alert('Ratio must be less than available questions');
                txt.focus();
                return;
            }
        }



        function totAllQUESTIONS(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }

        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function IsNumeric(txt) {
            var ValidChars = "0123456789.-";
            var num = true;
            var mChar;

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);
                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Error! Only Numeric Values Are Allowed")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }

    </script>

    <script type="text/javascript" language="javascript">


        function checkVal() {
            debugger;

            var table = document.getElementById('tblQue');

            var tot = 0.00;
            for (var r = 0; r < 1000; r++) {

                var txtSlectedQ = document.getElementById('ctl00_ContentPlaceHolder1_lsvbindquestion_ctrl' + r.toString() + '_txtSelectQ');
                var txtlimitQ = document.getElementById('ctl00_ContentPlaceHolder1_lsvbindquestion_ctrl' + r.toString() + '_TextBox2');

                if (parseInt(txtSlectedQ.value) > parseInt(txtlimitQ.value)) {


                    alert("Please Select Quetion lesser than limit");
                    document.getElementById('ctl00_ContentPlaceHolder1_lsvbindquestion_ctrl' + r.toString() + '_txtSelectQ').value = '';
                    document.getElementById('ctl00_ContentPlaceHolder1_lsvbindquestion_ctrl' + r.toString() + '_txtSelectQ').focus();
                    document.getElementById('ctl00_ContentPlaceHolder1_lsvbindquestion_ctrl' + r.toString() + '_txtSelectQ').style.color = 'red';
                }

            }


        }



        function Calculate(me) {
            var count = 0.00;
            try {

                for (i = 0; i <= 10000; i++) {

                    count = Number(count) + Number(document.getElementById("ctl00_ContentPlaceHolder1_lvQuestions_ctrl" + i + "_txtSelectQ").value);

                }
                document.getElementById("ctl00_ContentPlaceHolder1_lvQuestions_txttot").value = count;
                document.getElementById("ctl00_ContentPlaceHolder1_hdnTo").value = count;
                document.getElementById("ctl00_ContentPlaceHolder1_lblTotal").value = count;
            }
            catch (e) {
                document.getElementById("ctl00_ContentPlaceHolder1_lvQuestions_txttot").value = count;
                document.getElementById("ctl00_ContentPlaceHolder1_hdnTo").value = count;
                document.getElementById("ctl00_ContentPlaceHolder1_lblTotal").value = count;
            }
        }
    </script>
   




     <%--<script>
         function totAllSubjects(headchk) {
             var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
         var label = document.getElementById("<%=lblChkCount.ClientID %>");
         var frm = document.forms[0]
         for (i = 0; i < document.forms[0].elements.length; i++) {
             var e = frm.elements[i];
             if (e.type == 'checkbox') {
                 if (e.name.endsWith('chkFeesTransfer')) {
                     if (headchk.checked == true) {
                         e.checked = true;
                         hdfTot.value = Number(hdfTot.value) + 1;
                     }
                     else {
                         e.checked = false;
                         label.innerHTML = "0";
                         document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
                        }

                    }
                }
            }
        }</script>--%>



    <%--<script type="text/javascript">
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                //alert(sender._selectedDate.getTime().toString())
                //alert(new Date().toString())
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>--%>
</asp:Content>
