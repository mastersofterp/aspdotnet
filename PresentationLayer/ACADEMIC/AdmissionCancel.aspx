<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AdmissionCancel.aspx.cs" Inherits="Academic_AdmissionCancel" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <script type="text/javascript" charset="utf-8">
        //$(document).ready(function () {

        //    $(".display").dataTable({
        //        "bJQueryUI": true,
        //        "sPaginationType": "full_numbers"
        //    });

        //});

        function RunThisAfterEachAsyncPostback() {
            $(function () {
                $("#<%=txtFromDate.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                    yearRange: '1975:' + getCurrentYear()
                });
            });

            function getCurrentYear() {
                var cDate = new Date();
                return cDate.getFullYear();
            }
        }

    </script>

    <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <%--<ul class="nav nav-tabs">
        <li class="active"><a href="#tabLC" data-toggle="tab" aria-expanded="true">ADMISSION CANCEL</a></li>
        <li class=""><a href="#tabBC" data-toggle="tab" aria-expanded="false">RE-ADMISSION</a></li>
    </ul>--%>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ADMISSION CANCEL</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search By</label>
                                        </div>
                                        <asp:RadioButton ID="rdoRegno" runat="server" OnCheckedChanged="rdoRegno_CheckedChanged" AutoPostBack="true" Text="Reg No." GroupName="search"
                                                Checked="true" TabIndex="1" />
                                        <asp:RadioButton ID="rdoStudName" runat="server" OnCheckedChanged="rdoStudName_CheckedChanged" AutoPostBack="true" Text="Name" GroupName="search"
                                                TabIndex="2" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reg No.</label>
                                        </div>
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control"
                                                            ToolTip="Enter text to search" TabIndex="3" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                            ValidationGroup="search" TabIndex="4" CssClass="btn btn-primary" />
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtSearchText"
                                            Display="None" ErrorMessage="Please Enter Reg No." SetFocusOnError="true"
                                            ValidationGroup="search" Width="10%" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="search" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Total Amount</label>
                                        </div>
                                        <asp:Label ID="lblAmtTotal" runat="server"></asp:Label>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Amount Paid</label>
                                        </div>
                                        <asp:Label ID="lblAmtPaids" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Cancellation Report</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trDegree" runat="server" visible="True">
                                        <div class="label-dynamic">
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="5"
                                            ToolTip="Please Select Degree">
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select degree" SetFocusOnError="True" ValidationGroup="Cancel"
                                            InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trBranch" runat="server">
                                        <div class="label-dynamic">
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" TabIndex="6" ToolTip="Please Select Branch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trFrmDate" runat="server" visible="True">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue" id="imgFromDate"></i>
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

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trToDate" runat="server" visible="True">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue" id="imgToDate"></i>
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
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                        Text="Report" ValidationGroup="Cancel" TabIndex="11" CssClass="btn btn-info" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        OnClick="btnCancel_Click" TabIndex="12" CssClass="btn btn-warning" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFromDate"
                                        Display="None" ErrorMessage="Please enter from date" SetFocusOnError="true"
                                        ValidationGroup="Cancel" Width="10%" ID="rfvFromDate" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server"
                                        ControlExtender="meeFromDate" ControlToValidate="txtFromDate" Display="None"
                                        EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                        InvalidValueBlurredMessage="Invalid Date"
                                        InvalidValueMessage="From Date is Invalid (Enter DD-MM-YYYY Format)"
                                        SetFocusOnError="True" TooltipMessage="Please Enter From Date"
                                        ValidationGroup="Cancel" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtToDate"
                                        Display="None" ErrorMessage="Please enter to date" SetFocusOnError="true"
                                        ValidationGroup="Cancel" Width="10%" ID="rfvToDate" />
                                    <asp:ValidationSummary ID="ValCancelSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Cancel" />
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvSearchResults" runat="server" >
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Select</th>
                                                    <th>Reg No.</th>
                                                    <th>Name</th>
                                                    <th>DOB</th>
                                                    <th>Degree</th>
                                                    <th>Branch</th>
                                                    <th>Sem.</th>
                                                    <th>Admission Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate></EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="rdoSelectRecord" runat="server" ToolTip='<%# Eval("IDNO") %>' value='<%# Eval("IDNO") %>' OnCheckedChanged="rdoSelectRecord_CheckedChanged1" />
                                                <%--<asp:RadioButton ID="rdoSelectRecord" runat="server" value='<%# Eval("IDNO") %>'/>--%>
                                                <%--<input id="rdoSelectRecord" name="Student" type="radio" value='<%# Eval("IDNO") %>' />--%></td>
                                            <td><%# Eval("REGNO")%>
                                                <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("NAME") %>' Visible="false"></asp:Label></td>
                                            </td>

                                            <td><%# Eval("NAME") %></td>
                                            <td><%# (Eval("DOB").ToString() != string.Empty) ? ((DateTime)Eval("DOB")).ToShortDateString() : "-" %></td>
                                            <td><%# Eval("DEGREENO")%>
                                                <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENO")%>' ToolTip='<%# Eval("SHORTNAME")%>' Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hdnBranchno" runat="server" Value='<%# Eval("BRANCHNO")%>' />
                                                <%-- <asp:label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGE_NAME")%>' ToolTip='<%# Eval("COLLEGE_ID")%>' Visible="false"></asp:label>
                                            --%>  </td>

                                            <td><%# Eval("SHORTNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# (Eval("ADMDATE").ToString() != string.Empty) ? ((DateTime)Eval("ADMDATE")).ToShortDateString() : "-" %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divRemark" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reason of Cancelling Student Admission : <span style="color: red">(Maximum limit 300 characters)</span></label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" CssClass="form-control" MaxLength="300"
                                            runat="server" ValidationGroup="cancel1" />
                                        <asp:RequiredFieldValidator ID="rfvCancel" runat="server" ControlToValidate="txtRemark" ErrorMessage="Please Enter Remark" InitialValue="0"
                                            Display="None" SetFocusOnError="true"  ValidationGroup="cancel1"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divFileUpd" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Attach File:</label>
                                        </div>
                                        <asp:FileUpload ID="fuFile" runat="server" ToolTip="Select file to upload" TabIndex="2" />
                                        <span style="color: red">Note : Upload the Files only with following formats: .PDF and Size should be upto 100kb.</span>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnCancelAdmission" runat="server" OnClick="btnCancelAdmission_Click"  OnClientClick="return validation();"  Text="Cancel Admission" Visible="false" ValidationGroup="cancel1" CssClass="btn btn-primary" />
                                <%--<asp:Button ID="btnCancelAdmission" runat="server" OnClientClick="CancelAdmission(); return false" Text="Cancel Admission" Visible="false" CssClass="btn btn-primary" />--%>
                                <asp:Button ID="btnCancelAdmissionSlip" runat="server" OnClick="btnCancelAdmissionSlip_Click" Visible="false" Enabled="false" Text="Cancel Admission Slip" CssClass="btn btn-warning" />
                                <asp:Button ID="btnCan" Visible="false" runat="server" Text="Cancel"
                                                TabIndex="12" CssClass="btn btn-warning" OnClick="btnCan_Click" />
                                <asp:HiddenField ID="hdcount" runat="server" />
                                <asp:HiddenField ID="hdrecount" runat="server" />
                                    <asp:ValidationSummary ID="valCancelAdmission" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="cancel1" />
                            </div>

                            <div id="divMsg" runat="server">
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnCancelAdmission" />
            <asp:PostBackTrigger ControlID="btnCancelAdmissionSlip" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>



                    <div class="tab-content">
                        <div class="tab-pane active" id="tabLC"></div>
                        <div class="tab-pane " id="tabBC">
                            <div>
                                <div>
                                    <asp:UpdateProgress ID="updProg1" runat="server" AssociatedUpdatePanelID="updReAdmit"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <%--<div style="width: 120px; padding-left: 5px">
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
                                <asp:UpdatePanel ID="updReAdmit" runat="server" Visible="false">
                                    <ContentTemplate>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="box box-primary">
                                                    <div class="box-header with-border">
                                                      <%--  <h3 class="box-title" ><b>RE-ADMISSION </b></h3>--%>

                                                        <div class="pull-right">
                                                            <div style="color: Red; font-weight: bold">
                                                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                                            </div>
                                                        </div>
                                                    
                                                    </div>
                                                    <span style="color: red">&nbsp;&nbsp;&nbsp;Note : Upload the Files only with following formats: .PDF, .PNG, .JPEG, .JPG and Size should be upto 100kb.</span>

                                                    <div class="box-body">
                                                        <div class="col-md-12">
                                                            <div class="col-md-6">
                                                                <%--    <h3>Search Student</h3>--%>
                                                                <div class="col-md-9">
                                                                    <legend>Search By</legend>
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="rdoRegno1" runat="server" OnCheckedChanged="rdoRegno1_CheckedChanged" AutoPostBack="true" Text="Reg No." GroupName="search"
                                                                                Checked="true" />

                                                                        </label>

                                                                        <label>
                                                                            <asp:RadioButton ID="rdoStudName1" runat="server" OnCheckedChanged="rdoStudName1_CheckedChanged" AutoPostBack="true" Text="Name" GroupName="search" />
                                                                        </label>

                                                                    </div>
                                                                </div>
                                                                <div class="col-md-7" style="margin-top: 10px">

                                                                    <asp:TextBox ID="txtSearchText1" runat="server" CssClass="form-control"
                                                                        ToolTip="Enter text to search." />
                                                                </div>
                                                                <div class="box-footer col-md-3">
                                                                    <p class="text-center">

                                                                        <asp:Button ID="btnSearch1" runat="server" Text="Search" OnClick="btnSearch1_Click"
                                                                            ValidationGroup="search1" TabIndex="4" CssClass="btn btn-primary" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchText1"
                                                                            Display="None" ErrorMessage="Please Enter Registration No." SetFocusOnError="true"
                                                                            ValidationGroup="search1" Width="10%" />
                                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                            ShowSummary="false" ValidationGroup="search1" />
                                                                </div>

                                                                <div class="col-md-12">
                                                                    <label>Total Amount :</label>
                                                                    <asp:Label ID="lblTAmt" runat="server"></asp:Label>
                                                               
                                                                </div>
                                                                 <br />
                                                                <div class="col-md-12" style="margin-top:15px">
                                                                    <label> Amount Paid :</label>
                                                                    <asp:Label ID="lblAmtPaid" runat="server"></asp:Label>
                                                                
                                                                </div>
                                                                <br />
                                                               <%-- <div class="col-md-12" style="margin-top:15px">
                                                                     <div class="col-md-6">
                                                                    <asp:FileUpload ID="fuFile" runat="server" />
                                                                    <asp:HiddenField ID="hdnFile" runat="server" /></div>
                                                                     <div class="col-md-6" style="margin-top:-9px">
                                                                     <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click" Width="80px" Height="35px"
                                                                      CssClass="btn btn-success"/>
                                                                         </div>
                                                                </div>--%>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <fieldset>
                                                                    <legend>Re-Admission Report</legend>
                                                                    <div class="form-group col-md-12" id="DivDegree1" runat="server" visible="True">
                                                                        <label>Degree</label>
                                                                        <asp:DropDownList ID="ddlDegree1" runat="server" CssClass="form-control"
                                                                            AppendDataBoundItems="True" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlDegree1_SelectedIndexChanged"
                                                                            ToolTip="Please Select Degree">
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                          Display="None" ErrorMessage="Please Select degree" SetFocusOnError="True" ValidationGroup="Cancel"
                                                                          InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="form-group col-md-12" id="DivBranch1" runat="server">
                                                                        <label>Branch</label>
                                                                        <asp:DropDownList ID="ddlBranch1" runat="server" CssClass="form-control"
                                                                            AppendDataBoundItems="True" ToolTip="Please Select Branch">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-md-6" id="DivFromDate1" runat="server" visible="True">
                                                                        <label><span style="color: red;">*</span> From Date</label>
                                                                        <div class="input-group">
                                                                            <div class="input-group-addon">
                                                                                <i class="fa fa-calendar text-blue" id="imgFromDate1"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtFromDate1" runat="server" CssClass="form-control"
                                                                                ValidationGroup="Readmit" ToolTip="Please Select Date"></asp:TextBox>

                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                PopupButtonID="imgFromDate1" TargetControlID="txtFromDate1" />
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                                                TargetControlID="txtFromDate1" Mask="99/99/9999" MessageValidatorTip="true"
                                                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                                                ErrorTooltipEnabled="True" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-md-6" id="DivToDate1" runat="server" visible="True">
                                                                        <label><span style="color: red;">*</span> To Date</label>
                                                                        <div class="input-group">
                                                                            <div class="input-group-addon">
                                                                                <i class="fa fa-calendar text-blue" id="imgToDate1"></i>
                                                                            </div>
                                                                            <asp:TextBox ID="txtToDate1" runat="server" CssClass="form-control" ValidationGroup="Readmit"
                                                                                ToolTip="Please Select Date"></asp:TextBox>

                                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                                PopupButtonID="imgToDate1" TargetControlID="txtToDate1">
                                                                            </ajaxToolKit:CalendarExtender>
                                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                                                TargetControlID="txtToDate1" Mask="99/99/9999" MessageValidatorTip="true"
                                                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                                                ControlExtender="meeToDate" ControlToValidate="txtToDate1" Display="None"
                                                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                                InvalidValueMessage="To Date is Invalid (Enter dd-mm-yyyy Format)"
                                                                                TooltipMessage="Please Enter To Date" ValidationGroup="Readmit" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="box-footer col-md-12">
                                                                        <p class="text-center">
                                                                            <asp:Button ID="btbnReport1" runat="server" OnClick="btbnReport1_Click"
                                                                                Text="Report" ValidationGroup="Readmit" CssClass="btn btn-info" />
                                                                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel"
                                                                                OnClick="btnCancel1_Click" CssClass="btn btn-danger" />
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFromDate1"
                                                                                Display="None" ErrorMessage="Please Enter From date" SetFocusOnError="true"
                                                                                ValidationGroup="Readmit" Width="10%" ID="RequiredFieldValidator2" />
                                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                                                ControlExtender="meeFromDate" ControlToValidate="txtFromDate1" Display="None"
                                                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                                                InvalidValueBlurredMessage="Invalid Date"
                                                                                InvalidValueMessage="From Date is Invalid (Enter mm-dd-yyyy Format)"
                                                                                SetFocusOnError="True" TooltipMessage="Please Enter From Date"
                                                                                ValidationGroup="Readmit" />
                                                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtToDate1"
                                                                                Display="None" ErrorMessage="Please enter to date" SetFocusOnError="true"
                                                                                 ValidationGroup="Readmit" Width="10%" ID="RequiredFieldValidator3" />
                                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                                ShowSummary="false" ValidationGroup="Readmit" />
                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12 table table-responsive">
                                                           <asp:UpdatePanel ID="updlv" runat="server">   
                                                               <ContentTemplate>
                                                         <asp:ListView ID="lvReAdmit" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="listViewGrid">
                                                                        <h4>Search Results:</h4>
                                                                        <table id="tblSearchResultsforReadmit" class="table table-hover table-bordered">
                                                                            <thead>
                                                                                <tr class="bg-light-blue">
                                                                                    <th>Select</th>
                                                                                    <th>Reg No.</th>
                                                                                    <th>Name</th>
                                                                                    <th>DOB</th>
                                                                                    <th>Degree</th>
                                                                                    <th>Branch</th>
                                                                                    <th>Sem.</th>
                                                                                    <th>Admission Date</th>
                                                                                    <th>Upload Readmission doc.</th>
                                                                                    <th>Upload</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <EmptyDataTemplate></EmptyDataTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                       
                                                                            <%-- <input id="rdoSelectRecord" name="Student" type="radio" value='<%# Eval("IDNO") %>' /></td>--%>
                                                                            <asp:RadioButton ID="rdoSelectRecord" runat="server" AutoPostBack="false" value='<%# Eval("IDNO") %>' ToolTip='<%# Eval("IDNO") %>' OnCheckedChanged="rdoSelectRecord_CheckedChanged" />
                                                                         
                                                                        <td><%# Eval("REGNO")%></td>
                                                                        <td><%# Eval("NAME") %></td>
                                                                        <td><%# (Eval("DOB").ToString() != string.Empty) ? ((DateTime)Eval("DOB")).ToShortDateString() : "-" %></td>
                                                                        <td><%# Eval("DEGREENO")%></td>
                                                                        <td><%# Eval("SHORTNAME")%></td>
                                                                        <td><%# Eval("SEMESTERNAME")%></td>
                                                                        <td><%# (Eval("ADMDATE").ToString() != string.Empty) ? ((DateTime)Eval("ADMDATE")).ToShortDateString() : "-" %></td>
                                                                         <td style="width: 8%">
                                                                         <asp:FileUpload ID="fuFile" runat="server"  />
                                                                         <asp:HiddenField ID="hdnFile" runat="server" />
                                                                         <br />
                                                                     </td>
                                                                     <td>                                                                
                                                                           <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click" Width="80px" Height="35px"
                                                                             CssClass="btn btn-success" />
                                                                     </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                                   </ContentTemplate>
                                                               <Triggers>
                                                                   <%--<asp:AsyncPostBackTrigger ControlID="rdoSelectRecord" EventName="rdoSelectRecord_CheckedChanged" />--%>
                                                               </Triggers>
                                                               </asp:UpdatePanel>

                                                        </div>
                                                    </div>

                                                    <div class="box-footer col-md-12" id="divReAdmit" runat="server" visible="false">
                                                        <fieldset>
                                                            <legend>Re-Admission Details</legend>
                                                            <div class="form-group col-md-3">
                                                                <label><span style="color: red">*</span>Admission Batch</label>
                                                                <asp:DropDownList ID="ddlAdmBatch2" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch2"
                                                                    Display="None" ErrorMessage="Please Select Admission Batch" ValidationGroup="Admission"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-3">
                                                                <label><span style="color: red">*</span>College</label>
                                                                <asp:DropDownList ID="ddlCollege2" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege2_SelectedIndexChanged" CssClass="form-control">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCollege2" runat="server" ControlToValidate="ddlCollege2"
                                                                    Display="None" ErrorMessage="Please Select College" ValidationGroup="Admission"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-3">
                                                                <label><span style="color: red">*</span>Degree</label>
                                                                <asp:DropDownList ID="ddlDegree2" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                    ToolTip="Please Select Degree" OnSelectedIndexChanged="ddlDegree2_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree2"
                                                                    Display="None" ErrorMessage="Please Select Degree" ValidationGroup="Admission"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-3">
                                                                <label><span style="color: red">*</span>Branch</label>
                                                                <asp:DropDownList ID="ddlBranch2" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch2" runat="server" ControlToValidate="ddlBranch2" Display="None" 
                                                                  ErrorMessage="Please Select Branch" ValidationGroup="Admission" InitialValue="0" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-3">
                                                                <label><span style="color: red">*</span>Semester</label>
                                                                <asp:DropDownList ID="ddlSemester2" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                    ToolTip="Please Select Semester">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSemester2"
                                                                    Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Admission"
                                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-md-3">
                                                                <b>Reason for Re-Admitting Student :</b>
                                                                <br />
                                                                <asp:TextBox ID="txtRemark1" Rows="2" TextMode="MultiLine" MaxLength="300" runat="server" />
                                                                <asp:RequiredFieldValidator ID="rfvRemark1" runat="server" ControlToValidate="txtRemark1"
                                                                    Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="Admission"></asp:RequiredFieldValidator>
                                                            </div>

                                                        </fieldset>
                                                    </div>
                                                    <div class="box-footer col-md-12">
                                                        <p class="text-center">
                                                            <asp:Button ID="btnReAdmit" runat="server" OnClick="btnReAdmit_Click" Text="Re-Admit" Visible="false" OnClientClick="return ConfirmReAdmit();" ValidationGroup="Admission" CssClass="btn btn-primary" />

                                                            <asp:Button ID="btnReAdmissionSlip" runat="server" OnClick="btnReAdmissionSlip_Click" Text="Re-Admission Slip" Visible="false" Enabled="false" CssClass="btn btn-warning" />

                                                            <asp:Button ID="btnCancel_ReAdm" runat="server" OnClick="btnCancel_ReAdm_Click" Text="Clear" Visible="false" CssClass="btn btn-danger" />
                                                            <asp:ValidationSummary ID="rfv2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="Admission" />
                                                        </p>

                                                    </div>

                                                </div>
                                                <div id="divMsg1" runat="server">
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="btnReAdmit" />--%>
                                        <%--<asp:PostBackTrigger ControlID="btnReAdmissionSlip" />--%>
                                         <%--<asp:PostBackTrigger ControlID="btnSearch1" />--%>
                                          <asp:PostBackTrigger ControlID="lvReAdmit" />
                               
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
          
        <script type="text/javascript" lang="javascript">

            function ConfirmReAdmit() {
                var ret = confirm('Are you sure you want to Re-admit this Student?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
            function CancelAdm() {
                var ret = confirm('Are you sure you want to cancel this Student Admission?');
                if (ret == true)
                    return true;
                else
                    return false;
            }

        </script>

        <script type="text/javascript" language="javascript">
            function ShowClearance() {
                try {
                    var recValue = GetSelectedRecord();

                    if (recValue != "")
                        __doPostBack("ShowClearance", recValue);
                    else
                        alert("Please select a student record.");
                }
                catch (e) {
                    alert("Error: " + e.description);
                }
            }

            function GetSelectedRecord() {
                var recordValue = "";
                try {
                    var tbl = document.getElementById('tblSearchResults');
                    if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                        for (i = 1; i < tbl.rows.length; i++) {
                            var dataRow = tbl.rows[i];
                            var dataCell = dataRow.firstChild;
                            var rdo = dataCell.firstChild;
                            if (rdo.checked) {
                                recordValue = rdo.value;
                            }
                        }
                    }
                }
                catch (e) {
                    alert("Error: " + e.description);
                }
                return recordValue;
            }


            function CancelAdmission() {
                try {

                    var recValue = GetSelectedRecord();
                    if (recValue != "") {
                        if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("Are you sure you want to cancel this student's admission."))
                            __doPostBack("CancelAdmission", "");
                    }
                    else {
                        alert("Please enter reason for canceling the student's admission.");
                        document.getElementById('<%= txtRemark.ClientID %>').focus();
                    }
                }
                else if (recValue == "") {
                    alert("Please select a student");
                }

            }
            catch (e) {
                alert("Error: " + e.description);
            }
            // return false;
        }


        //added by srikanth 13-4-2020
        function ReAdmission() {

            var recordValue = "";
            try {
                debugger;
                var tbl = document.getElementById('tblSearchResultsforReadmit');
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;
                        var rdo = dataCell.firstChild;
                        if (rdo.checked) {
                            recordValue = rdo.value;
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return recordValue;

        }
        //added by srikanth 13-4-2020
        function ReAdmission() {
            try {

                var recValue = GetSelectedRecordForReAdmit();
                if (recValue != "") {
                    if ((document.getElementById('<%=txtRemark1.ClientID %>').value.trim() != "") && (document.getElementById('<%=ddlAdmBatch2.SelectedIndex %>').value != 0) && (document.getElementById('<%=ddlDegree2.SelectedIndex %>').value != 0) && (document.getElementById('<%=ddlSemester2.SelectedIndex %>').value != 0)) {
                        if (confirm("Are you sure you want to Re-Admit this Student ?"))
                            __doPostBack("ReAdmission", "");
                    }
                    else {
                        if (document.getElementById('<%=txtRemark1.ClientID %>').value.trim() = "") {
                            alert("Please Enter Remark for ReAdmitting the Student");
                            document.getElementById('<%= txtRemark1.ClientID %>').focus();
                        }
                        if (document.getElementById('<%=ddlAdmBatch2.SelectedIndex %>').value = 0) {
                            alert("Please Select Admission Batch");
                            document.getElementById('<%= ddlAdmBatch2.SelectedIndex %>').focus();
                        }
                        if (document.getElementById('<%=ddlDegree2.SelectedIndex %>').value = 0) {
                            alert("Please Select Degree");
                            document.getElementById('<%= ddlDegree2.SelectedIndex %>').focus();
                        }
                        if (document.getElementById('<%=ddlSemester2.SelectedIndex %>').value != 0) {
                            alert("Please Select Semester");
                            document.getElementById('<%= ddlSemester2.SelectedIndex %>').focus();
                        }
                    }
                }
                else if (recValue == "") {
                    alert("Please select a student");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            } 
        }




            function validation() {
                var hdncount = document.getElementById('<%=hdcount.ClientID%>');
                 var count = 0;
                 for (var i = 0; i < hdncount.value; i++) {
                     var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvSearchResults_ctrl' + i + '_rdoSelectRecord');
                     if (chk.type == 'checkbox') {
                         if (chk.checked) {
                             count++;
                         }
                     }
                 }
                 if (count > 0) {
                     var remark = document.getElementById('<%= txtRemark.ClientID%>').value;
                     if (remark == '') {
                         alert('Please Enter Remark.');
                         return false;
                     }
                     else {
                         var ret = confirm('Are You Sure To Cancel Selected Student Admission');
                         if (ret == true)
                             return true;
                         else
                             return false;
                     }
                 }
                 else {
                     alert('Please Check Student for Cancel Admission');
                     return false;
                 }
             }



           



        </script>
</asp:Content>
