<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PTDeductionReport.aspx.cs" Inherits="PAYROLL_REPORTS_PTDeductionReport" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
  
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="updPanel" runat="server">
         <ContentTemplate>
     <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">PT Deduction Reports</h3>
                </div>
                <div class="box-body">
                    
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>PT Deduction Reports</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>Select College</label>
								            </div>
                                             <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select College" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" data-select2-enable="true"
                                                AutoPostBack="true" CssClass="form-control"
                                                AppendDataBoundItems="true"
                                                 OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" />
							            </div>                                       
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <%--<label>Staff</label>--%>
                                                <label>Scheme/Staff</label>
								            </div>
                                            <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlStaff"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Scheme/Staff" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlStaff" runat="server" TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" />
							            </div>                                      
                                          <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
									            <sup>* </sup>
									            <label>PayHead</label>
								            </div>
                                            <asp:RequiredFieldValidator ID="rvfPayHead" runat="server" ControlToValidate="ddlPayHead"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select PayHead" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlPayHead" runat="server" TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPayHead_SelectedIndexChanged" />
							            </div>      
                                           </div>
                                </div> 
                   
                                           <div class="col-12">
                                    <div class="row">                               
                                        <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
                                                 <sup>* </sup>
									            <label>From Date</label>
								            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDt" runat="server" AutoPostBack="true" TabIndex="4" CssClass="form-control" />
                                               
                                              

                                    <ajaxToolkit:CalendarExtender ID="calDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgDate" TargetControlID="txtFromDt">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditExtender ID="meDate" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                        TargetControlID="txtFromDt">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="mvDate" runat="server" ControlExtender="meDate"
                                        ControlToValidate="txtFromDt" Display="None" EmptyValueMessage="Please Enter From Date."
                                        ErrorMessage="Please Select Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                        IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="submit"></ajaxToolkit:MaskedEditValidator>
                                            </div>
							            </div>     
                                  
                                      
                                         <div class="form-group col-lg-3 col-md-6 col-12">
								            <div class="label-dynamic">
                                                 <sup>* </sup>
									            <label>To Date</label>
								            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDt" runat="server" AutoPostBack="true" TabIndex="4" CssClass="form-control" />
                                               
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgDate" TargetControlID="txtToDt">
                                    </ajaxToolkit:CalendarExtender>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                        TargetControlID="txtToDt">
                                    </ajaxToolkit:MaskedEditExtender>
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meDate"
                                        ControlToValidate="txtToDt" Display="None" EmptyValueMessage="Please Enter To Date."
                                        ErrorMessage="Please Select Valid Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                        IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="submit"></ajaxToolkit:MaskedEditValidator>
                                            </div>
							            </div>                                     
                                        </div>
                                    </div>
                                
                   
                            <div class="col-md-12 text-center">
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                             
                                <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="25" CssClass="btn btn-info"
                                    ValidationGroup="submit" OnClick="btnReport_Click"  />                                
                                
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="27"
                                    CausesValidation="false" />                               
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

