<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AbsentStudentListReport.aspx.cs" Inherits="ACADEMIC_AbsentStudentListReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
    <asp:UpdatePanel ID="updSection" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Absent Student List Report</h3>
                           <%-- <h3 class="box-title">--%>
                                <%--<asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>--%>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <%--<div class="form-group col-md-12" style="color: red; font-weight: bold">
                                    <label style="color: #FF0000">Note:</label>
                                    <span style="color: #FF0000">* Marked is Mandatory !</span>
                                </div>--%>

                            <div class="col-12">
                                <div class="row">
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="true"  CssClass="form-control" data-select2-enable="true" 
                                            TabIndex="1" OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="AbsentList">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" TabIndex="2" AutoPostBack="true" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                             data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" >                      
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="AbsentList">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                   
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" TabIndex="3" AppendDataBoundItems="true" AutoPostBack="true"  CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select ddlDepartment" InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>--%>
                                    

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True"  AutoPostBack="True" data-select2-enable="true"
                                            CssClass="form-control" TabIndex="3" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="AbsentList">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true" data-select2-enable="true"
                                             CssClass="form-control" TabIndex="4" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="AbsentList">
                                        </asp:RequiredFieldValidator>
                                    </div>
                               <div class="form-group col-lg-3 col-md-6 col-12">
                                   <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYddlSubject" runat="server" Font-Bold="true">Subject</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="5"
                                             data-select2-enable="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsubject" runat="server" ControlToValidate="ddlSubject"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Subject" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                            
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                         <div class="label-dynamic">
                                              <sup>*</sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="FromDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="6" CssClass="form-control pull-right" 
                                                placeholder="From Date" ToolTip="Please Select From Date"  />

                                            <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="FromDate" Enabled="True" OnClientDateSelectionChanged="CheckDate">
                                            </ajaxToolKit:CalendarExtender>

                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="AbsentList"></asp:RequiredFieldValidator>

                                      

                                        </div>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                         <div class="label-dynamic">
                                             <sup>*</sup>
                                           <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="ToDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodate" runat="server" TabIndex="7"  placeholder="To Date" 
                                                ToolTip="Please Select To Date" CssClass="form-control pull-right"  />

                                            <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtTodate" PopupButtonID="ToDate" Enabled="True" OnClientDateSelectionChanged="CheckDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="AbsentList"></asp:RequiredFieldValidator>
                                              <%--<asp:CompareValidator ID="CompareValidator1" ValidationGroup="AbsentList" ForeColor="Red" runat="server"
                                        ControlToValidate="txtFromDate" ControlToCompare="txtTodate" Operator="LessThan" Type="Date" Display="None"
                                        ErrorMessage="From Date must be less than To Date."></asp:CompareValidator>--%>

                                        </div>
                                    </div>

                                  
                                    <div class="form-group col-md-6">
                                        <label for="city">Student</label>
                                        <asp:DropDownList ID="ddlStudents" runat="server" TabIndex="8" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                               <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>

                                

                                    <div class="form-group col-md-3">
                                        <label for="city">Report In</label>
                                        <asp:RadioButtonList ID="rdoReportType" runat="server"
                                            RepeatDirection="Horizontal" TabIndex="9">
                                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                                            <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                       
                        <div class="col-12 btn-footer">
                             <asp:LinkButton ID="btnAbsentStudentList" runat="server" TabIndex="10" 
                                      ValidationGroup="AbsentList" CssClass="btn btn-info" OnClick="btnAbsentStudentList_Click">
                                      Absent Student List</asp:LinkButton>

                                      <asp:LinkButton ID="btnAbsentStudentListReport" runat="server" TabIndex="11"  
                                      ValidationGroup="AbsentList" CssClass="btn btn-info" OnClick="btnAbsentStudentListReport_Click">
                                       Subject Student Wise</asp:LinkButton>

                                    
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        class="btn btn-warning" TabIndex="12" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="AbsentList" Style="text-align: center" />
                         

                        </div>
                     
                      
                         </div>

                       
                        <div id="divMsg" runat="Server">
                        </div>
                    </div>
                </div>
            </div>
            <script language="javascript" type="text/javascript">

                function getCurrentYear() {
                    var cDate = new Date();
                    return cDate.getFullYear();
                }

                function CheckDate(sender, args) {
                    //var txtfrm = document.getElementById('txtFromDate')
                    //var txtto = document.getElementById('txtToDate')
                    if (sender._selectedDate > new Date()) {
                        sender._selectedDate = new Date();
                        alert("Do not select Future Date!");
                        sender._textbox.set_Value("");
                        document.getElementById("txtFromDate").value = '';
                        document.getElementById("txtTodate").value = "";
                    }
                }
                </script>
            <script>
                function comparedate() {
                    var fromdate = document.getElementById("txtFromdate");
                    var todate = document.getElementById("txtTodate");
                    if (fromdate < todate) {
                        alert("From date should be less than To date");
                        return false;
                    }
                }
    </script>
        </ContentTemplate>

         <Triggers>
           <asp:PostBackTrigger ControlID="btnAbsentStudentList" />
          <asp:PostBackTrigger ControlID="btnAbsentStudentListReport" />
        </Triggers>
    </asp:UpdatePanel>
    </asp:Content>