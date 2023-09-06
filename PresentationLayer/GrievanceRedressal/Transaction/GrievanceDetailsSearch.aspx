<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GrievanceDetailsSearch.aspx.cs" Inherits="GrievanceRedressal_Transaction_GrievanceDetailsSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGrievanceApplication"
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
    <asp:UpdatePanel ID="updGrievanceApplication" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRIEVANCE SEARCH</h3>
                        </div>
                        <div class="box-body">
                            <div id="divGR" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <div class="row">
                                             <div class="col-12">
                                           <%-- <div class="sub-heading">
                                                <h5>Grievance Details Search</h5>
                                            </div>--%>
                                                 </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>From Date </label>
                                                </div>
                                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalTodt1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="1"
                                                        MaxLength="10" ToolTip="Application Date" Style="z-index: 0;"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgCalTodt1" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfvFDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="From Date is Required" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" TargetControlID="txtFromDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFromDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />

                                                    <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt"
                                                        ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true">
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                             <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>To Date</label>
                                                </div>
                                                         <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="1"
                                                        MaxLength="10" ToolTip="Application Date" Style="z-index: 0;"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                        PopupButtonID="imgCalTodt" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                             <asp:RequiredFieldValidator ID="rfvTDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="To Date is Required" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtFromDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtToDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />

                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeTodt"
                                                        ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true">
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                             <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Grievance Type</label>
                                                </div>
                                                   <asp:DropDownList ID="ddlGrievanceType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="True"
                                                    ValidationGroup="show" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCommittee" runat="server"
                                                    ErrorMessage="Please Select Grievance Type" ControlToValidate="ddlGrievanceType" Display="None"
                                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                               
                            </asp:Panel>
                                 </div>
                            
                         <div class=" col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Search" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Search" CausesValidation="true" TabIndex="1" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="1" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                   
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlRedressalCell" runat="server" >
                            <asp:ListView ID="lvshowRedressalCell" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading"><h5>GRIEVANCE DETAILS</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>VIEW
                                                    </th>
                                                    <th>STUDENT NAME
                                                    </th>
                                                    <th>ENROLL NO.
                                                    </th>
                                                    <th>DEGREE
                                                    </th>
                                                    <th>BRANCH
                                                    </th>
                                                    <th>SEMESTER
                                                    </th>
                                                    <th>GRIEVANCE CODE.
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
                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                CommandArgument='<%# Eval("GAID") %>' ImageUrl="~/Images/view2.png" TabIndex="1"
                                                ToolTip="View Record" OnClick="btnEdit_Click" />
                                            <asp:HiddenField ID="hdnsrno" runat="server" Value='<%# Eval("GAID") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("STUDNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("ENROLLNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("LONGNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SEMFULLNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("GRIV_CODE")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12" id="lblGrivHide">
                            <asp:Label ID="lblGrievance" runat="server" Text=""></asp:Label>
                        <%--<label id=""></label>--%>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server" >
                            <asp:ListView ID="lstGrvReply" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                          <div class="sub-heading"><h5>GRIEVANCE REPLY</h5>
                                        </div>
                                  
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>

                                                    <th>COMMITTEE REPLY
                                                    </th>
                                                    <th>STUDENT REPLY
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
                                            <%# Eval("REPLY")%>
                                        </td>
                                        <td>
                                            <%# Eval("STUD_REMARK")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                </div>

                            </div>
                       </div>
            </div>
                </div>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

