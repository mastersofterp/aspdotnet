<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Phd_Selection_List.aspx.cs" Inherits="ACADEMIC_PHD_Phd_Selection_List" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .dynamic-nav-tabs li.active a {
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }

        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updmarkEntry"
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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                    </h3>
                </div>
                <div class="nav-tabs-custom mt-2 col-12" id="myTabContent">
                    <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                        <li class="nav-item active" id="divlkselection" runat="server">
                            <asp:LinkButton ID="lkselection" runat="server" OnClick="lkselection_Click" CssClass="nav-link" TabIndex="1">Phd Selection List</asp:LinkButton></li>
                        <li class="nav-item" id="divlkoffer" runat="server">
                            <asp:LinkButton ID="lkoffer" runat="server" OnClick="lkoffer_Click" CssClass="nav-link" TabIndex="2">Phd Offer letter</asp:LinkButton></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade show active" id="divselection" role="tabpanel" runat="server" aria-labelledby="Selection-tab">
                            <asp:UpdatePanel ID="updmarkEntry" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Admission Batch</label>
                                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                            ControlToValidate="ddlAdmBatch" Display="None" SetFocusOnError="true" ValidationGroup="show" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                            ControlToValidate="ddlAdmBatch" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>School Applied for</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSchool" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" ToolTip="Please select school applied for">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Please select school applied for."
                                                        ControlToValidate="ddlSchool" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="show" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%-- <sup>* </sup>--%>
                                                        <label>PhD/Programme Applied For</label>
                                                        <asp:DropDownList ID="ddlprogram" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlprogram_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server" ErrorMessage="Please Select PhD/Programme Applied For"
                                                            ControlToValidate="ddlprogram" Display="None" SetFocusOnError="true" ValidationGroup="show" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server" ErrorMessage="Please Select PhD/Programme Applied For"
                                                            ControlToValidate="ddlprogram" Display="None" SetFocusOnError="true" ValidationGroup="submit" />--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>
                                                        <%--<sup>*</sup>--%>
                                            Mode of pursuing PhD</label>
                                                    <asp:DropDownList ID="ddlPhDMode" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" ToolTip="Please Select Mode of pursuing PhD" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPhDMode_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Full Time</asp:ListItem>
                                                        <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                        <%--<asp:ListItem Value="3">Full Time with Fellowship</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator44" InitialValue="0" runat="server" ErrorMessage="Please Select Mode of pursuing PhD"
                                            ControlToValidate="ddlPhDMode" Display="None" SetFocusOnError="true" ValidationGroup="show" />--%>
                                                    <asp:HiddenField ID="hdftestmark" Value="100" runat="server" />
                                                    <asp:HiddenField ID="hdfinterview" Value="50" runat="server" />
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnshow" runat="server" TabIndex="4" Text="Show" OnClick="btnshow_Click" CssClass="btn btn-primary" ValidationGroup="show" />
                                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="submit" TabIndex="5" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" Visible="false" />
                                            <asp:Button ID="btncancel" runat="server" TabIndex="6" Text="Cancel" CssClass="btn btn-warning" OnClick="btncancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="show"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup id="divmandatory" runat="server">* </sup>
                                                            <label>Supervisor</label>
                                                            <asp:DropDownList ID="ddlsuper" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server" ErrorMessage="Please Select Supervisor"
                                                                ControlToValidate="ddlsuper" Display="None" SetFocusOnError="true" ValidationGroup="submit" />--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:ListView ID="LvPhdMark" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Search List</h5>

                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="lstTable">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th style="text-align: center">Sr.No.
                                                                </th>
                                                                <th>
                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAll(this)" />
                                                                </th>
                                                                <th>Application ID</th>
                                                                <th>Student Name</th>
                                                                <th>Test Mark</th>
                                                                <th>Interview Mark</th>
                                                                <th>Total Mark</th>
                                                                <th>Supervisor</th>
                                                                <th>Status</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkallotment" runat="server" ToolTip='<%# Eval("USERNO")%>' Enabled='<%# Eval("SELECTION_STATUS").ToString() == "1" ? false : true%>' />

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblusername" runat="server" Text='<% #Eval("USERNAME")%>' ToolTip='<%# Eval("USERNO")%>'></asp:Label></td>

                                                        <td>
                                                            <asp:Label ID="lblname" runat="server" Text='<% #Eval("STUDENT_NAME")%>'></asp:Label></td>

                                                        <td>
                                                            <asp:Label ID="lbltestmark" runat="server" Text='<% #Eval("TEST_MARK")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblinterviewmark" runat="server" Text='<% #Eval("INTERVIEW_MARK")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbltotalmark" runat="server" Text='<% #Eval("TOTAL_MARK")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblsuper" runat="server" Text='<% #Eval("UA_FULLNAME")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<% #Eval("STATUS")%>' Font-Bold="true" ForeColor='<%# (Convert.ToInt32(Eval("SELECTION_STATUS") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divoffer" runat="server" visible="false" role="tabpanel" aria-labelledby="OfferLetter-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updoffer"
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
                            <asp:UpdatePanel ID="updoffer" runat="server">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Admission Batch</label>
                                                        <asp:DropDownList ID="ddladmoffer" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddladmoffer_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server" ErrorMessage="Please Select Admission Batch"
                                                            ControlToValidate="ddladmoffer" Display="None" SetFocusOnError="true" ValidationGroup="show" />

                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>School Applied for</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please select school applied for">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select school applied for."
                                                        ControlToValidate="ddlCollege" InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="show" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%-- <sup>* </sup>--%>
                                                        <label>PhD/Programme Applied For</label>
                                                        <asp:DropDownList ID="ddlpgm" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlpgm_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" InitialValue="0" runat="server" ErrorMessage="Please Select PhD/Programme Applied For"
                                                            ControlToValidate="ddlpgm" Display="None" SetFocusOnError="true" ValidationGroup="show" />--%>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>
                                                        Mode of pursuing PhD</label>
                                                    <asp:DropDownList ID="ddlmode" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" ToolTip="Please Select Mode of pursuing PhD" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Full Time</asp:ListItem>
                                                        <asp:ListItem Value="2">Part Time</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="HiddenField1" Value="100" runat="server" />
                                                    <asp:HiddenField ID="HiddenField2" Value="50" runat="server" />
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShowOffer" runat="server" TabIndex="4" Text="Show" OnClick="btnShowOffer_Click"
                                                CssClass="btn btn-primary" ValidationGroup="show" />
                                            <asp:Button ID="btnSend" runat="server" ValidationGroup="submit" TabIndex="5" Text="Send Offer Letter" OnClick="btnSend_Click" CssClass="btn btn-primary" Visible="false" />
                                            <asp:Button ID="btncanceloffer" runat="server" TabIndex="6" Text="Cancel" CssClass="btn btn-warning" OnClick="btncanceloffer_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDate" runat="server" visible="false">
                                                    <label><span style="color: red">* </span>Offer Letter Valid Date</label>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <div class="fa fa-calendar text-blue" id="icon"></div>
                                                        </div>
                                                        <asp:TextBox ID="txtStartDate" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Enter Date" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtStartDate" PopupButtonID="icon" Enabled="true">
                                                    </ajaxToolKit:CalendarExtender>

                                                    <ajaxToolKit:MaskedEditExtender ID="meStartdate" runat="server" TargetControlID="txtStartDate"
                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <%--       <asp:RequiredFieldValidator ID="rfvStartdate" runat="server" ControlToValidate="txtStartDate"
                                                        Display="None" ValidationGroup="regsubmit" ErrorMessage="Please Enter Date."></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divTime" runat="server" visible="false">
                                                    <label><span style="color: red">* </span>Time</label>

                                                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" ToolTip="Please Enter Time" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                                    <ajaxToolKit:MaskedEditExtender ID="meStarttime" runat="server" TargetControlID="txtStartTime"
                                                        Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                        MaskType="Time" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid time."
                                                        ControlToValidate="txtStartTime"  SetFocusOnError="true" ValidationGroup="submit" style="color:red"
                                                        ValidationExpression="((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime"
                                                        Display="None" ValidationGroup="regsubmit" ErrorMessage="Please Enter Time."></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="Panel1" runat="server" Visible="false">

                                            <asp:ListView ID="LvOffer" runat="server" OnItemDataBound="LvOffer_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Search List</h5>

                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="lstTable">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th style="text-align: center">Sr.No.
                                                                </th>
                                                                <th>
                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAlloffer(this)" />
                                                                </th>
                                                                <th>Application ID</th>
                                                                <th>Student Name</th>
                                                                <th>Email Id</th>
                                                                <th>Test Mark</th>
                                                                <th>Interview Mark</th>
                                                                <th>Total Mark</th>
                                                                <th>Status</th>
                                                                <th>Date</th>
                                                                <th>Time</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkallotment" runat="server" ToolTip='<%# Eval("USERNO")%>' Enabled='<%# Eval("EMAILSTATUS").ToString() == "1" ? false : true%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblusername" runat="server" Text='<% #Eval("USERNAME")%>' ToolTip='<%# Eval("USERNO")%>'></asp:Label></td>

                                                        <td>
                                                            <asp:Label ID="lblname" runat="server" Text='<% #Eval("STUDENT_NAME")%>'></asp:Label>
                                                            <asp:Label ID="lblbranch" runat="server" Text='<% #Eval("LONGNAME")%>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblemail" runat="server" Text='<% #Eval("EMAILID")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbltestmark" runat="server" Text='<% #Eval("TEST_MARK")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblinterviewmark" runat="server" Text='<% #Eval("INTERVIEW_MARK")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lbltotalmark" runat="server" Text='<% #Eval("TOTAL_MARK")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<% #Eval("EMAILSEND")%>' Font-Bold="true" ForeColor='<%# (Convert.ToInt32(Eval("EMAILSTATUS") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<% #Eval("INFORMED_DATE")%>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<% #Eval("INFORMED_TIME")%>'></asp:Label></td>
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

    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvPhdMark$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvPhdMark$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript" language="javascript">

        function SelectAlloffer(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvOffer$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvOffer$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
</asp:Content>
