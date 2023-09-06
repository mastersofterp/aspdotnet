<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SeatingPlanNew_Crescent.aspx.cs" Inherits="ACADEMIC_SEATINGARRANGEMENT_SeatingPlanNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <link href="<%=Page.ResolveClientUrl("~/plugins/dragable-swap/DragSwap.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/dragable-swap/MyCss.css") %>" rel="stylesheet" />

    <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.min.js"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/dragable-swap/jspdf.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/dragable-swap/html2canvas.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/dragable-swap/zepto.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/dragable-swap/zepto.dragswap.js")%>"></script>

    <%--<link href="../../JAVASCRIPTS/DraggableDiv/DragSwap.css" rel="stylesheet" />
    <link href="../../JAVASCRIPTS/DraggableDiv/MyCss.css" rel="stylesheet" />
   
    <script src="../../JAVASCRIPTS/DraggableDiv/jspdf.min.js"></script>
    <script src="../../JAVASCRIPTS/DraggableDiv/zepto.dragswap.js"></script>
    <script src="../../JAVASCRIPTS/DraggableDiv/zepto.min.js"></script>
    <script src="../../JAVASCRIPTS/DraggableDiv/html2canvas.js"></script>--%>

    <!-- MultiSelect Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>


    <%--  <link href="../../jquery/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../jquery/bootstrap-multiselect.js"></script>    
    <link href="../../jquery/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../jquery/bootstrap-multiselect.js"></script>--%>
    <%-- <script src="../../JAVASCRIPTS/DraggableDiv/jquery-3.2.1.min.js"></script>--%>

    <%-- <script src="../../JAVASCRIPTS/DraggableDiv/jquery-3.2.1.min.js"></script>--%>







    <asp:HiddenField ID="hfdTabsVal" runat="server" Value="1" />
    <asp:HiddenField ID="hfdDataTable" runat="server" Value="" />
    <%--2023--%>

    <asp:HiddenField ID="hfd_SESSIONNO" runat="server" Value="1" />
    <asp:HiddenField ID="hfd_REGNO" runat="server" Value="1" />
    <asp:HiddenField ID="hfd_ROOMNO" runat="server" Value="1" />
    <asp:HiddenField ID="hfd_SEATNO" runat="server" Value="1" />
    <asp:HiddenField ID="hfd_SEATNO_N" runat="server" Value="1" />
    <asp:HiddenField ID="hfd_LOCK" runat="server" Value="1" />
    <asp:HiddenField ID="hfd_EXAMDATE" runat="server" Value="1" />
    <asp:HiddenField ID="hfd_SLOT" runat="server" Value="1" />

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SEATING ARRANGEMENT</h3>
                    <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">CREATE SEATING PLAN</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">CANCEL SEATING PLAN</a>
                            </li>
                            <li class="nav-item d-none">
                                <a class="nav-link " data-toggle="tab" href="#tab_3 " tabindex="1">COPY SEATING PLAN</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_4" tabindex="1">SEATING PLAN REPORT</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <asp:UpdatePanel ID="updpnl_CreatePlan" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <%--Starts Here--%>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButton ID="rbInternal" runat="server" Text="&nbsp;Internal Exam" GroupName="IntExt" TabIndex="1" Checked="true" />&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbExternal" runat="server" Checked="false" Text="&nbsp;External Exam" GroupName="IntExt" TabIndex="2" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>

                                                </div>

                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtExamDate" runat="server" TabIndex="2" ValidationGroup="submit" OnTextChanged="txtExamDate_TextChanged" AutoPostBack="true" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                                            ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                                            InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                                            InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                    </div>
                                                </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <asp:Label ID="lblDYtxtExamDate" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamdate" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamdate_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvExamdat" runat="server" ControlToValidate="ddlExamdate"
                                                        Display="None" ErrorMessage="Please Select Exam Date" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam  Slot </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlslot" runat="server" TabIndex="3" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlslot_SelectedIndexChanged">
                                                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlslot"
                                                        Display="None" ErrorMessage="Please Select Slot" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Block</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBlock" runat="server" TabIndex="4" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Floor</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlFloor" runat="server" TabIndex="5" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Room</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRoom" runat="server" TabIndex="6" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lbltotcountHead" runat="server" Text=""></asp:Label></label>
                                                <asp:Label ID="lbltotcount" runat="server" Text=""></asp:Label><br />
                                            <label>
                                                <asp:Label ID="lblroomcapacityHead" runat="server" Text=""></asp:Label></label>
                                            <asp:Label ID="lblroomcapacity" runat="server" Text=""></asp:Label>
                                            <p class="text-center">
                                                <asp:Button ID="btnShow" runat="server" Text="Show Allotment" TabIndex="7" CssClass="btn btn-primary" OnClick="btnShow_Click" />

                                                <asp:Button ID="btnShow1" runat="server" Text="Show Alloted Seats" TabIndex="8" CssClass="btn btn-primary" OnClick="btnShow1_Click" Visible="false" />
                                                <%--<button type="button" class="btn btn-info" id="PrintXX">Print Report</button>--%>
                                                <asp:Button ID="btnSave" runat="server" Text="Save & Lock Allotment" TabIndex="8" CssClass="btn btn-primary" OnClick="btnSave_Click" Enabled="true" />
                                                <asp:Button ID="btnStastical" runat="server" Text="Statistical Report" Width="15%" TabIndex="15" Visible="false" />
                                                <asp:Button ID="btnClear" runat="server" Width="100px" CssClass="btn btn-warning" Text="Cancel" OnClick="btnClear_Click" TabIndex="9" />
                                                <div class="col-md-12" runat="server" id="divSeatingPlan" visible="false">
                                                    <div class="row">
                                                        <h3><span class="label label-default">Room View</span></h4>
                                                                <div class="panel panel-default">
                                                                    <div class="panel-heading"></div>
                                                                    <div class="panel-body">
                                                                        <div runat="server" id="DynamicSeating">
                                                                            <%--Dynamic HTML here--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                    </div>
                                                </div>

                                        </div>
                                        <%--Ends Here--%>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnShow" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClear" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_2">
                                <asp:UpdatePanel ID="updpnl_CancelPlan" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButton ID="rbCancelInternal" runat="server" Text="&nbsp;Internal Exam" GroupName="CancelIntExt" TabIndex="1" Checked="true" />&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbCancelExternal" runat="server" Checked="false" Text="&nbsp;External Exam" GroupName="CancelIntExt" TabIndex="2" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession1" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSession1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession1"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="configure"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtExamDate1" runat="server" TabIndex="2" ValidationGroup="submit" OnTextChanged="txtExamDate1_TextChanged" AutoPostBack="true" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="ceExamDate1" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtExamDate1" PopupButtonID="imgExamDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtExamDate1"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" />
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam Date</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamdate1" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamdate1_SelectedIndexChanged" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExamdate1"
                                                        Display="None" ErrorMessage="Please Select Exam Date" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam  Slot</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlslot1" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlslot1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Room</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRoom1" runat="server" TabIndex="4" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlRoom1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">

                                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel Allotment" TabIndex="5" CssClass="btn btn-warning" OnClick="btnCancel1_Click" />
                                            <asp:Button ID="btnClear1" runat="server" Text="Clear" TabIndex="6" CssClass="btn btn-warning" OnClick="btnClear1_Click" />



                                        </div>
                                        <div class="col-12" runat="server" id="divSeatingPlan2" visible="false">
                                            <div class="row">
                                                <h3 style="margin-top: 0px;"><span class="label label-default">Room View</span></h4>
                                                        <div class="panel panel-default">
                                                            <div class="panel-heading"></div>
                                                            <div class="panel-body">
                                                                <div runat="server" id="CancelDynamicSeating" class="row">
                                                                    <%--Dynamic HTML here--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                            </div>
                                        </div>
                                        <%--Ends Here--%>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCancel1" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClear1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_3 d-none">
                                <asp:UpdatePanel ID="updpnl_CopyPlan" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:RadioButton ID="rbCopyInternal" runat="server" Text="&nbsp;Internal Exam" GroupName="CopyIntExt" TabIndex="1" Checked="true" />&nbsp;
                                                    <asp:RadioButton ID="rbCopyExternal" runat="server" Checked="false" Text="&nbsp;External Exam" GroupName="CopyIntExt" TabIndex="2" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession3" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession3_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Exam Date</label>
                                                    </div>
                                                    <%--  <div class="input-group date">
                                                        <div class="input-group-addon" id="Image2">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>--%>
                                                    <asp:DropDownList ID="ddlExamDate3" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlExamDate3_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--</div>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Exam  Slot</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSlot3" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSlot3_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- <div class="col-12 mb-3">
                                            <span style="color: #2e82f6"><b>Copy To </b><i class="fas-solid fa fa-copy" aria-hidden="true" title="Copy To"></i></span>
                                        </div>--%>
                                        <div class="col-12">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession3_1" runat="server" TabIndex="4" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession3_1_SelectedIndexChanged" Enabled="false">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam Date</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamDate3_1" runat="server" TabIndex="5" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlExamDate3_1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam  Slot</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSlot3_1" runat="server" TabIndex="6" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSlot3_1_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Copy Seating Plan is Applicable only for 1st Year Students. </span></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnCopyPlan3" runat="server" Text="Copy Plan" TabIndex="5" CssClass="btn btn-primary" OnClick="btnCopyPlan3_Click" />
                                            <asp:Button ID="btnClear3" runat="server" Text="Clear" TabIndex="6" CssClass="btn btn-warning" OnClick="btnClear3_Click" />

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                            <div class="tab-pane fade" id="tab_4">
                                <asp:UpdatePanel ID="updpnl_PlanReports" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamType" runat="server" TabIndex="1" AppendDataBoundItems="True" data-select2-enable="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Internal Exam</asp:ListItem>
                                                        <asp:ListItem Value="2">External Exam</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Session</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession2" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession2_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Degree</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree2" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree2_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch2" runat="server" TabIndex="4" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="reportExamDate" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Exam Date</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtExamDate2" runat="server" TabIndex="3" ValidationGroup="submit" OnTextChanged="txtExamDate2_TextChanged" AutoPostBack="true" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="ceExamDate2" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtExamDate2" PopupButtonID="imgExamDate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtExamDate2"
                                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                            MaskType="Date" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Exam  Slot</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSlot2" runat="server" TabIndex="5" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSlot2_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Rooms</label>
                                                    </div>
                                                    <asp:ListBox ID="ddlRooms" runat="server" ValidationGroup="report" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo" SelectionMode="multiple" OnSelectedIndexChanged="ddlRooms_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>

                                                </div>

                                            </div>


                                            <div class="form-group col-md-3" id="reportExamName" runat="server" visible="false">
                                                <label><span style="color: red;">* </span>Exam Name :</label>
                                                <asp:ListBox ID="ddlExamName" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control multi-select-demo" SelectionMode="multiple">
                                                    <asp:ListItem Value="CAT 1^1$2">CAT 1 with I year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 1^3$4">CAT 1 with II year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 1^5$6">CAT 1 with III year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 1^7$8">CAT 1 with IV year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 2^1$2">CAT 2 with I year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 2^3$4">CAT 2 with II year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 2^5$6">CAT 2 with III year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 2^7$8">CAT 2 with IV year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 3^1$2">CAT 3 with I year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 3^3$4">CAT 3 with II year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 3^5$6">CAT 3 with III year</asp:ListItem>
                                                    <asp:ListItem Value="CAT 3^7$8">CAT 3 with IV year</asp:ListItem>
                                                </asp:ListBox>
                                            </div>
                                        </div>

                                        <div class="col-md-12 canvas_div_pdf">
                                            <div runat="server" id="divSeatingPlanReport">
                                                <%--Seating Plan Generated Here.--%>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <%--<center>--%>
                                            <asp:Button ID="btnSeatingPlanReport" runat="server" Text="Seating Allotment Report" CssClass="btn btn-primary" OnClick="btnSeatingPlanReport_Click" TabIndex="3" Style="margin-top: 5px" Visible="false" />
                                            <asp:Button ID="btnClassWiseHallAllotment" runat="server" Text="Department wise Hall Allotment" CssClass="btn btn-primary" OnClick="btnClassWiseHallAllotment_Click" TabIndex="4" Style="margin-top: 5px" Visible="false" />
                                            <asp:Button ID="btnSubjectWiseHallAllotment" runat="server" Text="Subject Wise Hall Allotment Report" CssClass="btn btn-primary" OnClick="btnSubjectWiseHallAllotment_Click" TabIndex="5" Style="margin-top: 5px" Visible="false" />
                                            <asp:Button ID="btnDateSessionWiseHallAllotment" runat="server" Text="Date And Session Wise Hall Allotment Report" CssClass="btn btn-primary" OnClick="btnDateSessionWiseHallAllotment_Click" TabIndex="6" Style="margin-top: 5px" Visible="false" />
                                            <asp:Button ID="btnEndSemReport" runat="server" Text="Answer Script Collection Report" CssClass="btn btn-primary" OnClick="btnEndSemReport_Click" TabIndex="7" Style="margin-top: 5px" Visible="false" />
                                            <asp:Button ID="btnInternalBranchWise" runat="server" Text="Branch Wise Report (Internal)" CssClass="btn btn-primary" OnClick="btnInternalBranchWise_Click" TabIndex="8" Style="margin-top: 5px" Visible="false" />
                                            <asp:Button ID="btnInternalBranchWise_Exl" runat="server" Text="Branch Wise Report (Internal) (Excel)" CssClass="btn btn-primary" OnClick="btnInternalBranchWise_Exl_Click" TabIndex="9" Style="margin-top: 5px" Visible="false" />
                                            <asp:Button ID="btnInternalRoomWise" runat="server" Text="Room Wise Report  (Internal)" CssClass="btn btn-primary" OnClick="btnInternalRoomWise_Click" TabIndex="10" Style="margin-top: 5px" Visible="false" />
                                            <asp:Button ID="btnInternalRoomAndSectionWise" runat="server" Text="Room and Section Wise Report (Internal)" CssClass="btn btn-primary" OnClick="btnInternalRoomAndSectionWise_Click" TabIndex="11" Style="margin-top: 5px" Visible="false" />
                                            </center>
                                                            <hr />
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSeatingPlanReport" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClassWiseHallAllotment" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSubjectWiseHallAllotment" />
                                        <asp:AsyncPostBackTrigger ControlID="btnDateSessionWiseHallAllotment" />
                                        <asp:AsyncPostBackTrigger ControlID="btnEndSemReport" />
                                        <asp:AsyncPostBackTrigger ControlID="btnInternalBranchWise" />
                                        <asp:AsyncPostBackTrigger ControlID="btnInternalRoomWise" />
                                        <asp:AsyncPostBackTrigger ControlID="btnInternalRoomAndSectionWise" />
                                        <asp:PostBackTrigger ControlID="btnInternalBranchWise_Exl" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <%-- <div class="tabbable-panel" style="border-bottom-width: 0px">
                                <div class="tabbable-line">
                                    <ul class="nav nav-tabs ">
                                        <li class="active">
                                            <a id="tab1X" href="#tabX_1" data-toggle="tab"><span class="MyTabStyle"><i class="fa fa-th-large" aria-hidden="true"></i>&nbsp;&nbsp;Create Seating Plan</span></a>
                                        </li>
                                        <li>
                                            <a id="tab2X" href="#tabX_2" data-toggle="tab"><span class="MyTabStyle"><i class="fa fa-times" aria-hidden="true"></i>&nbsp;&nbsp;Cancel Seating Plan</span></a>
                                        </li>

                                        <li>
                                            <a id="tab3X" href="#tabX_3" data-toggle="tab"><span class="MyTabStyle"><i class="fa fa-files-o" aria-hidden="true"></i>&nbsp;&nbsp;Copy Seating Plan</span></a>
                                        </li>
                                        <li>
                                            <a id="tab4X" href="#tabX_4" data-toggle="tab"><span class="MyTabStyle"><i class="fa fa-print" aria-hidden="true"></i>&nbsp;&nbsp;Seating Plan Reports</span></a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tabX_1">
                                        </div>

                                        <div class="tab-pane" id="tabX_2">
                                        </div>

                                        <div class="tab-pane" id="tabX_3">
                                        </div>

                                        <div class="tab-pane" id="tabX_4">
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--  <script src="<%=Page.ResolveClientUrl("~/jquery/bootstrap-multiselect.js")%>"></script>--%>
    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShow" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
        </Triggers>
    </asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>
    <%--Modal Call Starts Here--%>
    <div class="modal fade" id="BranchPref" role="dialog">
        <asp:UpdatePanel ID="upd_Branch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title" style="color: #3c8dbc;"><i class="fa fa-gg" aria-hidden="true"></i>Branch Preference</h4>
                            <button type="button" class="close" data-dismiss="modal" style="color: red; font-weight: bolder">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="hfdPrefBranches" ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hfdPrefCourses" ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hfdPrefCount" ClientIDMode="Static" runat="server" />
                                <asp:HiddenField ID="hfdPrevStatus" ClientIDMode="Static" runat="server" />
                                <div class="col-md-10 col-md-offset-1">
                                    <b style="color: red">Note : Drag and Drop Branches according to your preference.</b>
                                    <%--<label ID="lblcurrentCount1" class="pull - right" runat="server"></label>--%>
                                    <label id="lblcurrentCount" runat="server" />
                                </div>
                                <div class="col-12 mt-2">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr. No.
                                                </th>
                                                <th>Branch</th>
                                                <th> Sem</th>
                                                <th>Code</th>
                                                <th>Type</th>
                                                <th>Total Student Count</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptBranchPref" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="indexX" >
                                                           <%# Container.ItemIndex + 1 %>
                                                        </td>
                                                        <td>
                                                            <label class="containerX" style="font-weight: 100">
                                                                <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("CODE") %>' ToolTip='<%#Eval("BRANCHNO")+","+Eval("COURSENO")+","+Eval("PREV_STATUS")%>'></asp:Label>
                                                                <input type="checkbox" class="checkX">
                                                                <span class="checkmark"></span>
                                                            </label>
                                                        </td>
                                                        <td>
                                                          <%#Eval("SEMESTERNO")%>
                                                        </td>
                                                        <td>
                                                          <%#Eval("CCODE")%>
                                                        </td>
                                                        <td>
                                                         <%#Eval("COURSE_TYPE")%>
                                                        </td>
                                                        <td>
                                                            <%-- <asp:TextBox ID="txtActualStudCnt" runat="server" Text="0" Class="CntValidation" Style="height: 30px; width: 60px"></asp:TextBox> --%>
                                                            <input type="text" id="txtActualStudCnt" class="CntValidation" class="form-control" />
                                                            <asp:HiddenField ID="hfdStudCnt" runat="server" Value='<%#Eval("COUNTX")%>' />
                                                        </td>
                                                        <td>/ <%#Eval("COUNTX")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnBranchPref" CssClass="btn btn-primary" runat="server" Text="Proceed" OnClick="btnBranchPref_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBranchPref" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Modal Call Ends Here--%>
    <script>
        var $ = jQuery.noConflict();
    </script>

    <script type="text/javascript">

        function ShowSeatingPlanReport() {

            var HTML_Width = $(".canvas_div_pdf").width();
            var HTML_Height = $(".canvas_div_pdf").height();
            var top_left_margin = 15;
            var PDF_Width = HTML_Width + (top_left_margin * 2);
            var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
            var canvas_image_width = HTML_Width;
            var canvas_image_height = HTML_Height;

            var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;


            html2canvas($(".canvas_div_pdf")[0], { allowTaint: true }).then(function (canvas) {
                canvas.getContext('2d');

                console.log(canvas.height + "  " + canvas.width);


                var imgData = canvas.toDataURL("image/jpeg", 1.0);
                var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                pdf.addImage(imgData, 'JPG', 10, top_left_margin, canvas_image_width, canvas_image_height);


                for (var i = 1; i <= totalPDFPages; i++) {
                    pdf.addPage(PDF_Width, PDF_Height);
                    pdf.addImage(imgData, 'JPG', 10, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                }

                //pdf.save("HTML-Document.pdf");
                window.open(pdf.output('bloburl'), '_blank');
            });
            $(".canvas_div_pdf").css("display", "none");
        };

        $(document).ready(function () {

            $("#tab1X").click(function () {
                $('#<%=hfdTabsVal.ClientID%>').val(1);
             });

             $("#tab2X").click(function () {
                 $('#<%=hfdTabsVal.ClientID%>').val(2);
            });

             $("#tab3X").click(function () {
                 $('#<%=hfdTabsVal.ClientID%>').val(3);
            });

             $("#tab4X").click(function () {
                 $('#<%=hfdTabsVal.ClientID%>').val(4);
                //__doPostBack("<updpnl_PlanReports.UniqueID %>", "");
            });

         });




        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {
                //Used for Draggable Div
                Zepto('.sortableX').dragswap({
                    element: 'div',
                    dropAnimation: true
                });
                //END

                var fixHelperModified = function (e, tr) {
                    var $originals = tr.children();
                    var $helper = tr.clone();
                    $helper.children().each(function (index) {
                        $(this).width($originals.eq(index).width())
                    });
                    return $helper;
                },
                  // the updateIndex function works just fine without any parameters
                  updateIndex = function () {
                      $('td.indexX').each(function (i) {
                          $(this).html(i + 1);
                          $(this).css("text-align", "center");
                      });
                  };
                $("tbody").sortable({
                    helper: fixHelperModified,
                    stop: updateIndex
                }).disableSelection();
                $("body").on('click', function () {
                    updateIndex();
                });

                $('.checkX').change(function () {

                    if (this.checked) {
                        //var returnVal = confirm("Are you sure?");
                        //$(this).prop("checked", returnVal);
                        $(this).parent().find("span").eq(0).addClass("lblBranchX");
                        $(this).parent().parent().parent().find(".CntValidation").addClass("CntValX");
                    }
                    else {
                        $(this).parent().find("span").eq(0).removeClass("lblBranchX");
                        $(this).parent().parent().parent().find(".CntValidation").removeClass("CntValX");
                        $(this).parent().parent().parent().find(".CntValidation").val("0");
                    }

                    var sumTotal = 0;
                    var len = $('.lblBranchX').length;
                    for (var i = 0; i < len; i++) {

                        var Num = $("input[id*=txtActualStudCnt]")
                        var txtvalue = Num[i];

                        if (txtvalue.value != "" || txtvalue.value != null || txtvalue.value != 0) {
                            sumTotal += Number(txtvalue.value);
                        }
                    }
                    $("[id*=lblcurrentCount]").text('Current Count : ' + sumTotal);

                });


                $(<%=btnBranchPref.ClientID%>).click(function () {
                    debugger;
                    var i;
                    var l = $('.lblBranchX').length;
                    if (l == 0) {
                        alert('Please Select Atleast One Branch !!');
                        return false;
                    }
                    var w = '';
                    var x = '';
                    var y = '';
                    var z = '';
                    for (i = 0; i < l; i++) {
                        w += $('.lblBranchX')[i].title.split(',')[0] + ',';
                        x += $('.lblBranchX')[i].title.split(',')[1] + ',';
                        y += $('.lblBranchX')[i].title.split(',')[2] + ',';
                        z += $('.CntValX')[i].value + ',';
                    }
                    //  z = $('#ctl00_ContentPlaceHolder1_lblcurrentCount').val();
                    // z = $('#<%=lblcurrentCount.ClientID%>').length.val();


                    $('#<%=hfdPrefBranches.ClientID%>').val(w);
                    $('#<%=hfdPrefCourses.ClientID%>').val(x);
                    $('#<%=hfdPrevStatus.ClientID%>').val(y);
                    $('#<%=hfdPrefCount.ClientID%>').val(z);


                    return true;
                });

                $(".CntValidation").keyup(function () {
                    debugger;
                    var OriginalCnt = parseInt($(this).parent().find("input[type=hidden]").val());
                    var ActualCnt = parseInt($(this).val());

                    if (ActualCnt > OriginalCnt) {
                        $(this).val('1');
                        alert('Entered Value should not greater than Actual Count');
                        $(this).fadeOut("slow").css("border", "1px solid red");
                        $(this).fadeIn("slow");
                    }
                    else {
                        $(this).css("border", "1px solid #d2d6de");
                    }

                    // ADDED BY NARESH BEERLA FOR SUMATION OF USER INPUT COUNT ON DT 23072022
                    var i;
                    var len = $('.lblBranchX').length;
                    //ctl00$ContentPlaceHolder1$rptBranchPref$ctl01$txtActualStudCnt
                    // var txtvalue = (document.getElementById('ctl00_ContentPlaceHolder1_rptBranchPref_ctrl0_txtActualStudCnt').value);
                    var txtvalue = parseInt($(this).val());
                    //$('#<=lblcurrentCount.ClientID%>').val();
                    // alert(txtvalue);
                    if (len == 0) {
                        alert('Please Select Atleast One Branch !!');
                        document.getElementById('ctl00_ContentPlaceHolder1_lblcurrentCount').value = '';
                        return false;
                    }
                    var sumTotal = 0;
                    var CurrentCount = document.getElementById('ctl00_ContentPlaceHolder1_lblcurrentCount').value;
                    if (CurrentCount == "" || CurrentCount == null) {
                        CurrentCount = "Current Count :0";
                    }
                    for (i = 0; i < len; i++) {
                        //   txtvalue = $('#txtActualStudCnt')[i].val();//$('#txtActualStudCnt')[i].value; //$("#<=txtActualStudCnt.ClientID%>").val(Val); //(document.getElementById('ctl00_ContentPlaceHolder1_rptBranchPref_ctrl' + i + '_txtActualStudCnt').value);

                        var Num = $("input[id*=txtActualStudCnt]")
                        var txtvalue = Num[i];

                        if (txtvalue.value != "" || txtvalue.value != null || txtvalue.value != 0) {
                            sumTotal += Number(txtvalue.value);  //Number(CurrentCount.split(':')[1] + txtvalue.value);// $('.lblBranchX')[i].title.split(',')[0] + ',';
                        }
                        // else {
                        // $('#<%=lblcurrentCount.ClientID%>').val(sumTotal);//.val();
                        //  $("input[id*=lblcurrentCount]").text(sumTotal);

                        //   }
                    }
                    //document.getElementById("ctl00_ContentPlaceHolder1_lblcurrentCount").innerText = sumTotal;
                    $("[id*=lblcurrentCount]").text('Current Count : ' + sumTotal);

                });

                $(".CntValidation").keydown(function (e) {
                    // debugger;
                    var keyCode = e.keyCode || e.which;
                    if (keyCode == 9 && ($(this).val() == '' || $(this).val() == '0')) {
                        e.preventDefault();
                        alert('This Field should not be blank or Zero !!');
                    }
                });

                $(".CntValidation").focusout(function () {
                    debugger;
                    if ($(this).val() == '') {
                        $(this).val('1');
                        alert('Please enter Student Count');
                        $(this).fadeOut("slow").css("border", "1px solid red");
                        $(this).fadeIn("slow");
                        $(this).focus();
                    }
                });

                $("#tab1X").click(function () {
                    $('#<%=hfdTabsVal.ClientID%>').val(1);
                });

                $("#tab2X").click(function () {
                    $('#<%=hfdTabsVal.ClientID%>').val(2);
                });

                $("#tab3X").click(function () {
                    $('#<%=hfdTabsVal.ClientID%>').val(3);
                });

                $("#tab4X").click(function () {
                    $('#<%=hfdTabsVal.ClientID%>').val(4);
                });

                $('#<%=btnSave.ClientID%>').click(function () {
                    var r = confirm("Are you sure to Create Seating Allotment for " + $("#<%=ddlRoom.ClientID%> option:selected").text() + " !!");
                    if (r == true) {
                        debugger;
                        var Val = "";
                        for (var i = 0; i < $(".cellX").length; i++) {
                            Val += $(".cellX").eq(i).children().eq(4).html() + "," + $(".roominfoX").eq(i).html() + "_";
                            $("#<%=hfdDataTable.ClientID%>").val(Val);
                        }

                        return true;
                    } else {
                        return false;
                    }
                })

                $('#<%=btnCancel1.ClientID%>').click(function () {
                    if ($('#<%=ddlSession1.ClientID%> option:selected').val() == 0) {
                        alert('Please Select Session.');
                        return false;
                    }
                    else if ($('#<%=ddlExamdate1.ClientID%>').val() == '') {
                        alert('Please Select Exam Date.');
                        return false;
                    }
                    else if ($('#<%=ddlslot1.ClientID%> option:selected').val() == 0) {
                        alert('Please Select Slot.');
                        return false;
                    }
                    else if ($('#<%=ddlRoom1.ClientID%> option:selected').val() == 0) {
                        alert('Please Select Room.');
                        return false;
                    }

                    var r = confirm("Are you sure to Cancel Seating Allotment for " + $("#<%=ddlRoom1.ClientID%> option:selected").text() + " !!");
                    if (r == true) {
                        return true;
                    } else {
                        return false;
                    }
                });


                $('#<%=btnCopyPlan3.ClientID%>').click(function () {
                    if ($('#<%=ddlSession3.ClientID%> option:selected').val() == 0) {
                        alert('Please Select Session.');
                        return false;
                    }
                    else if ($('#<%=ddlExamDate3.ClientID%> option:selected').val() == 0) {
                        alert('Please Select From Exam Date.');
                        return false;
                    }
                    else if ($('#<%=ddlSlot3.ClientID%> option:selected').val() == 0) {
                        alert('Please Select Slot 1.');
                        return false;
                    }
                    else if ($('#<%=ddlExamDate3_1.ClientID%> option:selected').val() == 0) {
                        alert('Please Select To Exam Date.');
                        return false;
                    }
                    else if ($('#<%=ddlSlot3_1.ClientID%> option:selected').val() == 0) {
                        alert('Please Select Slot 2.');
                        return false;
                    }

                    var r = confirm("Are you sure to Copy Seating Plan From " + $("#<%=ddlExamDate3.ClientID%> option:selected").text() + "  To " + $("#<%=ddlExamDate3_1.ClientID%> option:selected").text() + " !!");
                    if (r == true) {
                        return true;
                    } else {
                        return false;
                    }
                });


            });
        });

    </script>



    <%--  <script type="text/javascript">
         jq1833 = jQuery.noConflict();

         jq1833(document).ready(function () {
             jq1833("#<%=ddlExamDate3.ClientID%>").searchable({
                maxListSize: 200, // if list size are less than maxListSize, show them all
                maxMultiMatch: 300, // how many matching entries should be displayed
                exactMatch: false, // Exact matching on search
                wildcards: true, // Support for wildcard characters (*, ?)
                ignoreCase: true, // Ignore case sensitivity
                latency: 200, // how many millis to wait until starting search
                warnMultiMatch: 'top {0} matches ...',
                warnNoMatch: 'no matches ...',
                zIndex: 'auto'
            });

            jq1833("#<%=ddlExamDate3_1.ClientID%>").searchable({
                maxListSize: 200, // if list size are less than maxListSize, show them all
                maxMultiMatch: 300, // how many matching entries should be displayed
                exactMatch: false, // Exact matching on search
                wildcards: true, // Support for wildcard characters (*, ?)
                ignoreCase: true, // Ignore case sensitivity
                latency: 200, // how many millis to wait until starting search
                warnMultiMatch: 'top {0} matches ...',
                warnNoMatch: 'no matches ...',
                zIndex: 'auto'
            });

            $("#<%=ddlExamDate3.ClientID%>").css('width', '');
            $("#<%=ddlExamDate3.ClientID%>").parent().css('width', '');
            $("#<%=ddlExamDate3.ClientID%>").css('height', '');

            $("#<%=ddlExamDate3_1.ClientID%>").css('width', '');
            $("#<%=ddlExamDate3_1.ClientID%>").parent().css('width', '');
            $("#<%=ddlExamDate3_1.ClientID%>").css('height', '');

        });

        var prm = Sys.WebForms.PageRequestManager();
        prm.add_endRequest(function () {
            jq1833 = jQuery.noConflict();

            jq1833(document).ready(function () {
                jq1833("#<%=ddlExamDate3.ClientID%>").searchable({
                    maxListSize: 200, // if list size are less than maxListSize, show them all
                    maxMultiMatch: 300, // how many matching entries should be displayed
                    exactMatch: false, // Exact matching on search
                    wildcards: true, // Support for wildcard characters (*, ?)
                    ignoreCase: true, // Ignore case sensitivity
                    latency: 200, // how many millis to wait until starting search
                    warnMultiMatch: 'top {0} matches ...',
                    warnNoMatch: 'no matches ...',
                    zIndex: 'auto'
                });

                jq1833("#<%=ddlExamDate3_1.ClientID%>").searchable({
                    maxListSize: 200, // if list size are less than maxListSize, show them all
                    maxMultiMatch: 300, // how many matching entries should be displayed
                    exactMatch: false, // Exact matching on search
                    wildcards: true, // Support for wildcard characters (*, ?)
                    ignoreCase: true, // Ignore case sensitivity
                    latency: 200, // how many millis to wait until starting search
                    warnMultiMatch: 'top {0} matches ...',
                    warnNoMatch: 'no matches ...',
                    zIndex: 'auto'
                });

                $("#<%=ddlExamDate3.ClientID%>").css('width', '');
                $("#<%=ddlExamDate3.ClientID%>").parent().css('width', '');
                $("#<%=ddlExamDate3.ClientID%>").css('height', '');

                $("#<%=ddlExamDate3_1.ClientID%>").css('width', '');
                $("#<%=ddlExamDate3_1.ClientID%>").parent().css('width', '');
                $("#<%=ddlExamDate3_1.ClientID%>").css('height', '');

            });

        });
    </script>--%>
</asp:Content>
