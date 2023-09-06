<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmissionCancellationApproveFinal.aspx.cs" Inherits="ACADEMIC_AdmissionCancellationApproveFinal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {

            if (!$.fn.DataTable.isDataTable('#example2')) {
                $('#example2').dataTable();
            }
            if (!$.fn.DataTable.isDataTable('#example3')) {
                $('#example3').dataT
                if (!$.fn.DataTable.isDataTable('#example1')) {
                    $('#example1').dataTable();
                }


            }
        }
    </script>--%>

    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }



        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }



        .modalPopup {
            background-color: #FFFFFF;
            width: 700px;
            border: 3px solid #858788; /*#0DA9D0;*/
            border-radius: 12px;
            padding: 0;
        }



            .modalPopup.right {
                right: 0 !important;
                top: 0 !important;
                left: inherit !important;
                border-radius: 12px;
                height: 100%;
            }



            .modalPopup .header {
                background-color: #858788; /*#2FBDF1;*/
                height: 30px;
                color: White;
                line-height: 30px;
                text-align: center;
                font-weight: bold;
                border-top-left-radius: 6px;
                border-top-right-radius: 6px;
            }



            .modalPopup .body {
                padding: 10px;
                min-height: 50px;
                text-align: center;
                font-weight: bold;
            }



            .modalPopup .footer {
                padding: 6px;
            }



            .modalPopup .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
                border-radius: 4px;
            }



            .modalPopup .yes {
                background-color: #2FBDF1;
                border: 1px solid #0DA9D0;
            }



            .modalPopup .no {
                background-color: #9F9F9F;
                border: 1px solid #5C5C5C;
            }



        element.style {
            font-family: Verdana !important;
            font-size: 10pt !important;
            color: red !important;
        }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <%--<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">--%>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <%-- <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>--%>
                <%--added new loader on 12052020 srikanth--%>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-body">
                            <div class="form-group col-md-12">
                                <div class="box-header with-border">
                                    <h3 class="box-title"><b>ADMISSION CANCELLATION</b></h3>
                                    <div class="box-tools pull-right">
                                        <div style="color: Red; font-weight: bold">
                                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                     <div class="form-group col-md-12" style="text-align: center">
                                       
                                            <asp:RadioButton ID="rdoFinalApprove" runat="server" OnCheckedChanged="rdoFinalApprove_CheckedChanged" AutoPostBack="true" Text="Final Approval" GroupName="search"
                                               TabIndex="1" />&nbsp;&nbsp;&nbsp;&nbsp;

                                        
                                            <asp:RadioButton ID="rdoReport" runat="server" OnCheckedChanged="rdoReport_CheckedChanged" AutoPostBack="true" Text="Cancellation Report" GroupName="search"
                                                TabIndex="2" />
                                       
                                    </div>
                                </div>
                                <div id="divApprove" class="row" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <%-- <div class="col-md-9">--%>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div></div>
                                                <div class="col-md-5">


                                                    <%-- <label>Reg. No:</label>--%>
                                                    <div class="form-group">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtStudent" Visible="false" runat="server" class="form-control" />
                                                            <label><span style="color: red">*</span> Student List</label>
                                                            <asp:DropDownList ID="ddlStudentList" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStudentList_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="input-group-btn">
                                                                <asp:Button ID="btnShow" Visible="false" runat="server" Text="Show" CssClass="btn btn-primary btn-flat" ValidationGroup="Show" />
                                                            </span>

                                                        </div>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="rfvRollNumber" Visible="false" runat="server"
                                                        ControlToValidate="txtStudent" Display="None"
                                                        ErrorMessage="Please Enter Reg. No" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="rfvRollNumber_submit" Visible="false" runat="server"
                                                        ControlToValidate="txtStudent" Display="None"
                                                        ErrorMessage="Please Enter Roll Number" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblPreSession" runat="server" Font-Bold="true" Visible="false" />
                                                </div>
                                                <div class="col-md-4" style="margin-top: 25px">
                                                    <span>
                                                        <asp:Button ID="btnReport" runat="server" Text="View Approval Report" OnClick="btnReport_Click" CssClass="btn btn-primary btn-flat" />
                                                    </span>
                                                </div>

                                                <div class="col-md-3">
                                                    <%--  <asp:Image ID="imgEmpPhoto" runat="server" style="float:left" ImageUrl="~/IMAGES/nophoto.jpg" Height="100px" Width="100px" />

                                                <div>
                                                    <fieldset class="fieldset">
                                                        <legend class="legend"></legend><b style="color: #FF0000">Note : </b>Student's username will also be changed after branch change.                                   
                                                    </fieldset>

                                                </div>--%>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <%-- <div class="col-md-1"></div>--%>
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblNote" Font-Bold="true" Visible="false" Text="Note: Please do not refresh page Or Do not search new student once you processed demand for current student." Style="color: red;" runat="server" SkinID="lblmsg"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" id="divdata" runat="server" visible="false">
                                            <div class="col-md-12">
                                                <div class="panel panel-default">
                                                    <div class="panel-body">
                                                        <div class="form-group col-md-6">
                                                            <label>Student Name :</label>
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                            <asp:Label ID="lblAdmbatch" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblYear" runat="server" Font-Bold="True" Visible="false"></asp:Label>

                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Branch :</label>
                                                            <asp:Label ID="lblBranch" runat="server" Style="color: green" Font-Bold="True"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Reg. No. :</label>
                                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Roll No. :</label>
                                                            <asp:Label ID="lblRollNo" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>Degree. :</label>
                                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                        </div>
                                                        <div class="form-group col-md-6">
                                                            <label>College Name :</label>
                                                            <asp:Label ID="lblColg" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body">
                                                                <div class="form-group col-md-12">
                                                                    <label>Admission Cancel Requested Remark:</label><asp:Label ID="lblRequestRemark" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                                                </div>
                                                                  <div class="form-group col-md-12">
                                                                    <label>Admission Cancel Academic Approved Remark:</label><asp:Label ID="lblAcademicRemark" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="form-group col-md-12">
                                                                    <label>Admission Cancel Approved Remark:</label><asp:Label ID="lblApprovedRemark" Font-Bold="true" ForeColor="Green" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                                <asp:Panel ID="pnlBranchChange" runat="server" Visible="false">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">

                                                            <div class="form-group col-md-6">
                                                                <span style="color: red">*</span>
                                                                <label>Remark :</label>
                                                                <span style="color: red">(Maximum limit 300 characters)</span>
                                                                <asp:TextBox ID="txtRemark" runat="server" Height="70px" TextMode="MultiLine" MaxLength="300"
                                                                    CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rvfRemark" runat="server"
                                                                    ControlToValidate="txtRemark" ErrorMessage="Please Enter Remark"
                                                                    ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-1"></div>
                                                            <div class="form-group col-md-4" style="padding-top: 30px;">
                                                                <label>Document Preview :</label>
                                                                <asp:LinkButton ID="lnkView" runat="server" Visible="false" CommandName='<%# Eval("FILE_NAME") %>' OnClick="lnkView_Click" CommandArgument='<%# Eval("PREVIEW_PATH") %>' ToolTip='<%# Eval("IDNO") %>'><image style="height:25px" src="../IMAGES/view.gif" data-toggle="modal" data-target="#myModal22"></image></asp:LinkButton>
                                                                <asp:Label ID="lblPreview" Font-Bold="false" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-md-4" style="margin-left:83px">
                                                                <label>Total Applied Amount:  </label><asp:Label ID="lblAppliedAmount" ForeColor="Green" Font-Bold="true" runat="server"></asp:Label>
                                                            </div>
                                                              <div class="form-group col-md-4">
                                                                <label>Total Paid Amount:  </label><asp:Label ID="lblPaidAmount" ForeColor="Green" Font-Bold="true" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </asp:Panel>

                                                <div class="col-md-3">
                                                    <asp:Image ID="imgEmpPhoto" runat="server" Visible="false" ImageUrl="~/IMAGES/nophoto.jpg" Height="100px" Width="100px" Style="text-align: center;" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-4"></div>
                                                <div class="col-md-8">

                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" Visible="false"
                                                        ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnCancelAdmissionSlip" runat="server" OnClick="btnCancelAdmissionSlip_Click" Text="Cancel Admission Slip" Visible="false" Enabled="false" CssClass="btn btn-warning" />
                                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-danger" Visible="false"
                                                        Text="Cancel" /><br />

                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-10">
                                                    <asp:Label ID="lblMsg" Font-Bold="true" runat="server" SkinID="lblmsg"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div id="divReport" class="row" runat="server" visible="false">

                                    <div class="form-group col-md-3">
                                        <label>Degree</label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="5"
                                            ToolTip="Please Select Degree">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select degree" SetFocusOnError="True" ValidationGroup="Cancel"
                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>

                                   <div class="form-group col-md-3">
                                        <label>Branch</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="True" TabIndex="6" ToolTip="Please Select Branch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red;">*</span> From Date</label>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"
                                                ValidationGroup="Cancel" TabIndex="7" ToolTip="Please Select Date"></asp:TextBox>

                                            <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgFromDate" TargetControlID="txtFromDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                ErrorTooltipEnabled="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red;">*</span> To Date</label>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ValidationGroup="Cancel"
                                                TabIndex="9" ToolTip="Please Select Date"></asp:TextBox>

                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="imgToDate" TargetControlID="txtToDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server"
                                                TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server"
                                                ControlExtender="meeToDate" ControlToValidate="txtToDate" Display="None"
                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                InvalidValueBlurredMessage="Invalid Date"
                                                InvalidValueMessage="To Date is Invalid (Enter mm-dd-yyyy Format)"
                                                TooltipMessage="Please Enter To Date" ValidationGroup="Cancel" />
                                        </div>
                                    </div>
                                    <div class="box-footer col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnCanReport" runat="server" OnClick="btnCanReport_Click"
                                                Text="Report" ValidationGroup="Cancel" TabIndex="11" CssClass="btn btn-info" />
                                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel"
                                                OnClick="btnCancel1_Click" TabIndex="12" CssClass="btn btn-danger" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please enter from date" SetFocusOnError="true"
                                                ValidationGroup="Cancel" Width="10%" ID="rfvFromDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server"
                                                ControlExtender="meeFromDate" ControlToValidate="txtFromDate" Display="None"
                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                InvalidValueBlurredMessage="Invalid Date"
                                                InvalidValueMessage="From Date is Invalid (Enter mm-dd-yyyy Format)"
                                                SetFocusOnError="True" TooltipMessage="Please Enter From Date"
                                                ValidationGroup="Cancel" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please enter to date" SetFocusOnError="true"
                                                ValidationGroup="Cancel" Width="10%" ID="rfvToDate" />
                                            <asp:ValidationSummary ID="ValCancelSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Cancel" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="lnkView" />
            <asp:PostBackTrigger ControlID="btnCancelAdmissionSlip" />
             <asp:PostBackTrigger ControlID="btnCanReport" />
        </Triggers>
    </asp:UpdatePanel>
    <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopup"
        TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup">
        <div class="header">
            Document
                 
        </div>
        <div class="body">
            <iframe runat="server" width="680px" height="550px" id="iframeView"></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClose" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger" Text="Close" CssClass="no" />
        </div>
    </asp:Panel>
    <script type="text/javascript" language="javascript">

        /* To collapse and expand page sections */
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../images/collapse_blue.jpg";
            }
        }
    </script>
    <div id="divMsg" runat="server" />
</asp:Content>

