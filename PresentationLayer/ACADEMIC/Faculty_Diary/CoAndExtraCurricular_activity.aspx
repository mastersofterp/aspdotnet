<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CoAndExtraCurricular_activity.aspx.cs" Inherits="ACADEMIC_CoAndExtraCurricular_activity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div>
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
    </div>--%>
  <%--  <asp:UpdatePanel ID="updSection" runat="server"   >
        <ContentTemplate>--%>
    <div id="divMsg" runat="server"></div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Co-Curricular and Extra Curricular Activity</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblNameOfProgram" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                             
                                        <asp:TextBox ID="txtProgramName" runat="server" MaxLength="256" ViewStateMode="Enabled" AppendDataBoundItems="True"
                                             TextMode="MultiLine" CssClass="form-control" ToolTip="Enter Name of the program"  TabIndex="1" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProgramName"
                                            Display="None" ErrorMessage="Please Enter Name of the program" SetFocusOnError="True" 
                                            ValidationGroup="validate" />
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divfromdate" runat="server">
                                        <div class="label-dynamic">
                                            <sup id="frmdtspan" runat="server">*</sup>
                                            <label>Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-green"></i>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" ValidationGroup="Show" onpaste="return false;"
                                                TabIndex="2" ToolTip="Please Enter Date" CssClass="form-control" Style="z-index: 0;" />
                                            <%-- <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                AlternateText="Select Date" Style="cursor: pointer" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDate" PopupButtonID="dvcal1" />
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtDate"
                                                Display="None" ErrorMessage="Please Enter Date" SetFocusOnError="True"  
                                                ValidationGroup="validate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                           <%-- <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeFromDate"
                                                ControlToValidate="txtDate" EmptyValueMessage="Please Enter From Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="validate" SetFocusOnError="True" />--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblGroupTeacher" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtGroupTeacher" runat="server" CssClass="form-control" MaxLength="150" ToolTip="Enter Group Teacher Details"  TabIndex="3" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGroupTeacher"
                                            Display="None" ErrorMessage="Please Enter Group Teacher Details" SetFocusOnError="True" 
                                            ValidationGroup="validate"/>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblPrincipal" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtPrincipal" runat="server" CssClass="form-control" ToolTip="Enter Principal Details" MaxLength="150" TabIndex="4"  />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPrincipal"
                                            Display="None" ErrorMessage="Please Enter Principal Details" SetFocusOnError="True" 
                                            ValidationGroup="validate" />
                                    </div>
                                </div>
                            </div>
                              <div class="col-12 btn-footer">
                                   <asp:Button ID="btnSubmit" runat="server" TabIndex="5" Text="Submit"  OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="validate"  />
                                   <asp:Button ID="btnCoandExtReport" runat="server" TabIndex="6" Text="Report" OnClick="btnCoandExtReport_Click" CssClass="btn btn-primary" CausesValidation="false" />
                                   <asp:Button ID="btnCancel" runat="server" TabIndex="7" Text="Cancel" OnClick="btnCancel_Click1"  CssClass="btn btn-warning"  CausesValidation="false" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="validate" />
                               
                            </div>
                        </div>




                    </div>
                </div>
            </div>
      <%--  </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>--%>

</asp:Content>

