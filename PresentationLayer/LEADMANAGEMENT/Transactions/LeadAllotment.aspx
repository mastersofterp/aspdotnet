<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeadAllotment.aspx.cs" Inherits="LEADMANAGEMENT_Transactions_LeadAllotment" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlListView.dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAllot"
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
    <asp:UpdatePanel ID="updAllot" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tab1">Lead Allotment</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="3" href="#tab3">Query</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tab2">Reports</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="4" href="#tab4">Remark</a>
                                    </li>

                                </ul>
                                <div class="box-tools pull-right">
                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                </div>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdLeadMain" runat="server" AssociatedUpdatePanelID="UpdLead"
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
                                        <asp:UpdatePanel ID="UpdLead" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:HiddenField ID="hdnLead" runat="server" ClientIDMode="Static" />--%>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label id=""></label>--%>
                                                                <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="1" ToolTip="Please Select Admission Batch." AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label id=""></label>--%>
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Programme Type"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlProgrammeType" runat="server" TabIndex="2" ToolTip="Please Select Programme Type." AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlProgrammeType_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                       <%-- <div id="Div3" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>--%>
                                                                <%--<label id=""></label>--%>
                                                               <%-- <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Programme Level"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlLevel" runat="server" TabIndex="1" ToolTip="Please Select Programme Level." AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">0%</asp:ListItem>
                                                                <asp:ListItem Value="2">25%</asp:ListItem>
                                                                <asp:ListItem Value="3">50%</asp:ListItem>
                                                                <asp:ListItem Value="4">75%</asp:ListItem>
                                                                <asp:ListItem Value="5">100%</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hftot" runat="server" />
                                                        </div>--%>
                                                        <div id="divUser" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Main User"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlMainUser" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" ToolTip="Please Select Main User." TabIndex="3"  OnSelectedIndexChanged="ddlMainUser_SelectedIndexChanged1" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>




                                                          <div class="form-group col-lg-3 col-md-6 col-12" id="divSubCounsellor" runat="server" visible="false">
                                                 <div class="label-dynamic">
                                                      <sup>* </sup>
                                                     <asp:Label ID="lblDYsubuser" runat="server" Font-Bold="true" Text="SubCounsellors"></asp:Label>
                                                       </div>
                                             <asp:DropDownList ID="ddlsubuser" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" ToolTip="Please Select Main User." TabIndex="4"  >
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                   </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="rfvSubUser" runat="server" ControlToValidate="ddlsubuser" Display="None" ErrorMessage="Please Select Sub User." ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                      </div>
                                                        <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <asp:RadioButtonList ID="rdoAllot" runat="server" ToolTip="Please Select Allotment Selection." TabIndex="1" RepeatColumns="3" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnShow" runat="server" TabIndex="1" ToolTip="Click To Show." Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" OnClientClick="return Validate();" />
                                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="1" ToolTip="Click To Submit." Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return selectAndSubmitValidate();" Enabled="false" />
                                                    <asp:Button ID="btnSend" runat="server" TabIndex="1" ToolTip="Click To Send Email." Text="Send Email" CssClass="btn btn-primary" OnClick="btnSend_Click" OnClientClick="return selectAtleastOne();" Visible="false"/>
                                                    <asp:Button ID="btnCancel" runat="server" TabIndex="1" ToolTip="Click To Cancel." Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlListView" runat="server">
                                                        <asp:ListView ID="lvLeadAllot" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Lead Allotment List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divAllotment">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="return SelectAll(this)" ToolTip="Select/Select all" />
                                                                            </th>
                                                                            <th>Applicant Name
                                                                            </th>
                                                                            <th>Mobile No
                                                                            </th>
                                                                            <th>Email Id
                                                                            </th>
                                                                            <th>Degree Name
                                                                            </th>
                                                                            <th>Registration Date
                                                                            </th>
                                                                          <%--  <th>Progress Level
                                                                            </th>--%>
                                                                            <th>Main Counsellor
                                                                            </th>
                                                                             <th>SubCounsellor
                                                                            </th>
                                                                            
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("USERNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkName" runat="server" Text='<%#Eval("FULLNAME") %>' OnClick="lnkName_Click"></asp:LinkButton>
                                                                        <asp:HiddenField ID="hdnUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("MOBILENO") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("EMAILID") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("DEGREE") %>
                                                                        <asp:HiddenField ID="hdnDegree" runat="server" Value='<%#Eval ("DEGREENO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("REGDATE") %>
                                                                    </td>

                                                                    <td><%# Eval("MAIN_COUNSELLOR") %></td>
                                                                    <td><%# Eval("SUB_COUNSELLOR") %> </td>    
                                                                     <%--<td><%# Eval("UA_FULLNAME") %></td>
                                                                      <td><%# Eval("SUBUSER_UA_NO") %> </td>   --%>
                                                                   <%-- <td>
                                                                        <%#Eval("PROGRESS_LEVEL") %> 
                                                                    </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="modal fade beauty" id="myApplicationModal">
                                            <div class="modal-dialog modal-xl">
                                                <div class="modal-content">
                                                    <div>
                                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updPopUp"
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
                                                    <asp:UpdatePanel ID="updPopUp" runat="server">
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12 col-12">
                                                                    <div class="modal-header">
                                                                        <h3 class="modal-title">Application Details</h3>
                                                                        <button type="button" class="close" title="Close" onclick="closePopUp();">&times;</button>
                                                                        <%--<asp:ImageButton ID="btnClosePop" runat="server" />--%>
                                                                    </div>
                                                                    <div id="divMsg" runat="server"></div>
                                                                    <div class="box-body mt-2">
                                                                        <div class="col-12">
                                                                            <div class="row">
                                                                                <div class="col-lg-6 col-md-6 col-12">
                                                                                    <ul class="list-group list-group-unbordered">
                                                                                        <li class="list-group-item"><b>
                                                                                            <asp:Label ID="lblStudName" runat="server" Font-Bold="true" Text="Student Name"></asp:Label>
                                                                                            :</b>
                                                                                            <a class="sub-label">
                                                                                                <asp:Label ID="lblName" runat="server"></asp:Label>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li class="list-group-item"><b>
                                                                                            <asp:Label ID="lblEmailNew" runat="server" Font-Bold="true" Text="Email Id"></asp:Label>
                                                                                            :</b>
                                                                                            <a class="sub-label">
                                                                                                <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li class="list-group-item"><b>
                                                                                            <asp:Label ID="lblPhoneNo" runat="server" Font-Bold="true" Text="Mobile No."></asp:Label>
                                                                                            :</b>
                                                                                            <a class="sub-label">
                                                                                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li class="list-group-item" id="liRemark" runat="server" style="display: none"><b>
                                                                                            <asp:Label ID="lblRemark" runat="server" Text="Remark" Font-Bold="true"></asp:Label>
                                                                                            :</b>
                                                                                            <asp:TextBox ID="txt_Remark" runat="server" TabIndex="9" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine"></asp:TextBox>
                                                                                            <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                                                                TargetControlID="txt_Remark" WatermarkText="Enter Remark" />
                                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="textFilter" runat="server" TargetControlID="txt_Remark"
                                                                                                InvalidChars="~`!@#$%^&*()_-+={[}]|\:;<,.>?/'" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                                <div class="col-lg-6 col-md-6 col-12">
                                                                                    <ul class="list-group list-group-unbordered">
                                                                                        <li class="list-group-item"><b>
                                                                                            <asp:Label ID="lblLeadStatus" runat="server" Font-Bold="true" Text="Lead Status"></asp:Label>
                                                                                            :</b>
                                                                                            <a class="sub-label">
                                                                                                <asp:Label ID="lblLeadStatusName" runat="server"></asp:Label>
                                                                                            </a>
                                                                                            <span>
                                                                                                <asp:ImageButton ID="imgEditLead" runat="server" ImageUrl="~/Images/edit1.png"
                                                                                                    AlternateText="Edit Record" ToolTip="Edit Record" AutoPostBack="true" OnClick="imgEditLead_Click" /></span>
                                                                                            </a>
                                                                                        </li>
                                                                                        <li class="list-group-item" id="liLeadStatus" runat="server" style="display: none"><b>
                                                                                            <asp:Label ID="lblLeadDDL" runat="server" Text="Lead Status" Font-Bold="true"></asp:Label>
                                                                                            :</b>
                                                                                            <asp:DropDownList ID="ddlLeadStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:HiddenField ID="hdnEnqueryno" runat="server" />
                                                                                        </li>

                                                                                        <li class="list-group-item" id="liFollowDate" runat="server" style="display: none"><b>
                                                                                            <asp:Label ID="lblFollowDate" runat="server" Text="Next Followup date" Font-Bold="true"></asp:Label>
                                                                                            :</b>
                                                                                            <asp:TextBox ID="txtEndDate" runat="server" TabIndex="4"
                                                                                                ToolTip="Please Enter Next Followup Date" CssClass="form-control" />
                                                                                            <i class="fa fa-calendar input-prefix" id="faCalendar" runat="server" aria-hidden="true" style="float: right; margin-top: -23px; margin-right: 10px;"></i>
                                                                                            <ajaxToolKit:CalendarExtender ID="ceAdmDt" runat="server" Format="dd/MM/yyyy"
                                                                                                TargetControlID="txtEndDate" PopupButtonID="faCalendar" Enabled="True">
                                                                                            </ajaxToolKit:CalendarExtender>
                                                                                            <ajaxToolKit:MaskedEditExtender ID="meeAdmDt" runat="server" TargetControlID="txtEndDate"
                                                                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                                                CultureTimePlaceholder="" Enabled="True" />
                                                                                            <%--   <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                                                                ErrorMessage="Please Enter Next Followup Date" ControlToValidate="txtEndDate" Display="None"
                                                                                                ValidationGroup="ubmit" />--%>
                                                                                        </li>
                                                                                    </ul>
                                                                                    <div class="col-12 btn-footer" style="text-align: center; display: none" id="divButtonModel" runat="server">
                                                                                        <asp:Button ID="btn_SubmitModal" runat="server" TabIndex="11" Text="Submit" CssClass="btn btn-outline-primary" OnClick="btn_SubmitModal_Click" />
                                                                                        <asp:Button ID="btn_Cancel" runat="server" TabIndex="12" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btn_Cancel_Click" />
                                                                                    </div>
                                                                                </div>
                                                                                <%--<div class="col-lg-6 col-md-6 col-12">
                                                                                    <div class="nav-tabs-custom mt-2 col-12">
                                                                                        <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                                                                            <li class="nav-item active" id="divlkApplication" runat="server">
                                                                                                <asp:LinkButton ID="lkAnnouncementApplication" runat="server" OnClick="lkAnnouncementApplication_Click" CssClass="nav-link" TabIndex="1">Lead Status</asp:LinkButton></li>
                                                                                            <li class="nav-item" id="divlkFeed" runat="server">
                                                                                                <asp:LinkButton ID="lkFeedback" runat="server" CssClass="nav-link" OnClick="lkFeedback_Click" TabIndex="2">Lead Details</asp:LinkButton></li>
                                                                                          
                                                                                        </ul>
                                                                                    </div>
                                                                                </div>--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab2">
                                        <div>
                                            <asp:UpdateProgress ID="UpdReportMain" runat="server" AssociatedUpdatePanelID="UpdReport"
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
                                        <%--<asp:HiddenField ID="hdnReport" runat="server" ClientIDMode="Static" />--%>
                                        <asp:UpdatePanel ID="UpdReport" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Admission Batch"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlAdmBatch_Rpt" runat="server" ToolTip="Please Select Admission Batch." TabIndex="1" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_Rpt_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>


                                                                        <%--<asp:Label ID="Label4" runat="server" Font-Bold="True" Style="color: Red" Text="TEST"></asp:Label>--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Programme Type"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlProgrammeType_Rpt" runat="server" ToolTip="Please Select Programme Type." TabIndex="1" AppendDataBoundItems="true" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-12 btn-footer">
                                                                        <asp:Button ID="btnExcel" runat="server" Text="Report(Excel)" TabIndex="1" CssClass="btn btn-primary" OnClick="btnExcel_Click" OnClientClick="return Validate_Rpt();" />
                                                                        <asp:Button ID="btnCancel_Rpt" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Rpt_Click" />
                                                                        <%--<asp:Button ID="btnCancel_Rpt" runat="server--%>
                                                                        <%--<asp:Button Text="IN" ID="btnIN" runat="server" OnClick="btnIN_Click" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnExcel" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="tab-pane" id="tab3">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdQuery"
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
                                        <asp:UpdatePanel ID="UpdQuery" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Category of Queries</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlFormCategory" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlFormCategory_SelectedIndexChanged"
                                                                            ValidationGroup="submit">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12" id="dvListView">
                                                                <asp:ListView ID="lvStudentQuery" runat="server" OnItemDataBound="lvStudentQuery_ItemDataBound" OnPagePropertiesChanged="lvStudentQuery_PagePropertiesChanged">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Student Query List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Applicant Name </th>
                                                                                    <th>Email Id</th>
                                                                                    <th>Contact Number</th>
                                                                                    <th>Query Status </th>
                                                                                    <th>Action</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemplaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr class="item">
                                                                            <td>
                                                                                <asp:Label ID="lblfirstname" runat="server" Text='<%# Eval("firstname")%>' ToolTip='<%# Eval("firstname")%>' />
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lblemailid" runat="server" Text='<%# Eval("emailid")%>' ToolTip='<%# Eval("emailid")%>' />
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("mobile")%>' ToolTip='<%# Eval("mobile")%>' />
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("feedback_status")%>' ToolTip='<%# Eval("feedback_status")%>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button runat="server" Text="Reply" CssClass="btn btn-primary" ID="btnPriview" CommandName='<%# Eval("userno")%>' ToolTip='<%# Eval("query_category")%>' OnClick="btnPriview_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="row">
                                            <div class="modal fade" id="myModal1" role="dialog">
                                                <div class="modal-dialog">
                                                    <!-- Modal content-->
                                                    <div class="modal-content chat-app">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title"><b>Reply For Query</b> <%--<i class="fa fa-question-circle"></i>--%></h4>
                                                            <span class="text-right">
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button></span>
                                                        </div>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <div class="modal-body" style="background-color: floralwhite">
                                                                    <div style="z-index: 1; position: absolute; left: 300px;">

                                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                                            DynamicLayout="true" DisplayAfter="0">
                                                                            <ProgressTemplate>
                                                                                <div style="width: 120px; padding-left: 5px">
                                                                                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                                                                                    <p class="text-success"><b>Loading..</b></p>
                                                                                </div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>

                                                                    </div>
                                                                    <div>
                                                                        <asp:ListView ID="lvFeedbackReply" runat="server">
                                                                            <LayoutTemplate>
                                                                                <div id="listViewGrid">
                                                                                    <div id="tblStudents">
                                                                                        <div id="itemPlaceholder" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUserReply" runat="server" Text='<%# Eval("FEEDBACK_DETAILS")%>' CssClass="chat-reply user-reply" />
                                                                                <asp:Label ID="lblAdminReply" runat="server" Text='<%# Eval("FEEDBACK_REPLY")%>' CssClass="chat-reply admin-reply" />
                                                                            </ItemTemplate>
                                                                        </asp:ListView>

                                                                    </div>
                                                                </div>

                                                                <div class="modal-footer">
                                                                    <div class="input-group chat-message">
                                                                        <asp:TextBox ID="txtFeedback" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Type your message here..."></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFeedback"
                                                                            ErrorMessage="Please Enter Your Answer" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <span class="input-group-btn">
                                                                            <asp:Button ID="btnSubmit_Reply" runat="server" Text="Send" CssClass="btn btn-success"
                                                                                ValidationGroup="submit" OnClick="btnSubmit_Reply_Click" />
                                                                        </span>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" data-select2-enable="true" runat="server">
                                                                            <asp:ListItem Selected="True" Value="1">Open</asp:ListItem>
                                                                            <asp:ListItem Value="2">Close</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="text-center" style="display: none;">
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    
                                                <asp:Button ID="btnCancel_Reply" runat="server" Text="Cancel" CssClass="btn btn-warning" data-dismiss="modal" />
                                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="38px" ShowMessageBox="True"
                                                                            ShowSummary="False" ValidationGroup="submit" DisplayMode="List" />

                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                  
                                                        <strong>
                                                            <asp:Label ID="lblStatus1" runat="server"></asp:Label></strong>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <script type="text/javascript">
                                            function showPopup() {
                                                $('#myModal1').modal('show');
                                            }
                                        </script>
                                    </div>
                                    <div class="tab-pane" id="tab4">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdRemark"
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
                                        <asp:UpdatePanel ID="UpdRemark" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Admission Batch"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlAdmBatch_Remark" TabIndex="1" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_Remark_SelectedIndexChanged"
                                                                            ValidationGroup="submit">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                    </div>
                                                <div class="col-12">
                                                    <asp:Panel ID="pnlListRemark" runat="server">
                                                        <asp:ListView ID="lvRemark" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Application Status List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divRemark">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                Student Name
                                                                            </th>
                                                                            <th>
                                                                                Email
                                                                            </th>
                                                                            <th>
                                                                                Mobile No.
                                                                            </th>
                                                                            <th>
                                                                                Enquiry Date
                                                                            </th>
                                                                            <th>
                                                                                Lead Status
                                                                            </th>
                                                                            <th>
                                                                                Remark
                                                                            </th>
                                                                            <th>
                                                                                Added By
                                                                            </th>
                                                                            <th>
                                                                               Next Follow Up Date
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:Label ID="lblName_Remark" runat="server" Text='<%#Eval("FIRSTNAME") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail_Remark" runat="server" Text='<%#Eval("EMAILID") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMobile_Remark" runat="server" Text='<%#Eval("MOBILENO") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEnquiryDate_Remark" runat="server" Text='<%#Eval("Enquiry Date") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblLeadStatus_Remark" runat="server" Text='<%#Eval("ENQUIRYSTATUSNAME") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblRemark_Remark" runat="server" Text='<%#Eval("REMARKS") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAddBy_Remark" runat="server" Text='<%#Eval("Added By") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblNxtFollow_Remark" runat="server" Text='<%#Eval("Next Follow Up") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        function Validate_Rpt() {
            var admBatch_Rpt = document.getElementById('<%=ddlAdmBatch_Rpt.ClientID%>').value;
            var programmeType_Rpt = document.getElementById('<%=ddlProgrammeType_Rpt.ClientID%>').value;
            var summary = "";
            if (admBatch_Rpt == "0") {
                summary += "Please Select Admission Batch.\n";
            }
            if (programmeType_Rpt == "0") {
                summary += "Please Select Programme Type.\n";
            }
            if (summary != "") {
                alert(summary);
                summary = "";
                return false;
            }
        }
       
        function selectAtleastOne() {
            var count = 0;
            var numberOfChecked = $('[id*=divAllotment] td input:checkbox:checked').length;
            if (numberOfChecked == 0) {
                alert("Please select atleast one student.");
                return false;
            }
            else
                return true;
        }
        function selectAndSubmitValidate() {
            var user = document.getElementById('<%=ddlMainUser.ClientID%>').value;
            //alert(user);
            if (user == "0") {
                alert("Please Select Main User.");
                return false;
            }
            var count = 0;
            var numberOfChecked = $('[id*=divAllotment] td input:checkbox:checked').length;
            if (numberOfChecked == 0) {
                alert("Please select atleast one student.");
                return false;
            }
            else
                return true;

        }
        function closePopUp() {
            //var liRemark = document.getElementById('<%=liRemark%>')
            $('#liRemark').css("display", "none");
            $('#liLeadStatus').css("display", "none");
            $('#liFollowDate').css("display", "none");
            $('#myApplicationModal').modal('hide');
        }
    </script>


    
</asp:Content>
