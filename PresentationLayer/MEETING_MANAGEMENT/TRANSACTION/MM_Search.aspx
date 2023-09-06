<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MM_Search.aspx.cs" Inherits="MEETING_MANAGEMENT_Transaction_MM_Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">

        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MEETING SEARCH</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCollegeName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Institute Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Meeting Period (From Date)</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgFrmDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtstartdate" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Select From Date" AutoPostBack="true" OnTextChanged="txtstartdate_TextChanged"></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Meeting Period(From Date)"
                                                        ValidationGroup="Schedule" ControlToValidate="txtstartdate" Display="None"
                                                        Text="*"></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtstartdate"
                                                    PopupButtonID="imgFrmDt" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeFrmDt" runat="server" TargetControlID="txtstartdate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />


                                                <ajaxToolKit:MaskedEditValidator ID="mevFrmDt" runat="server" ControlExtender="meeFrmDt" ControlToValidate="txtstartdate"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Schedule"></ajaxToolKit:MaskedEditValidator><%--EmptyValueMessage="Please Enter From Date" IsValidEmpty="false" EmptyValueBlurredText="Empty"--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>To Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control" ToolTip="Select To Date" TabIndex="3" AutoPostBack="true" OnTextChanged="txtenddate_TextChanged"></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select To Date"
                                                        ValidationGroup="Schedule" ControlToValidate="txtenddate" Display="None"
                                                        Text="*"></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgTodt" TargetControlID="txtenddate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeToDt" runat="server" TargetControlID="txtenddate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />

                                                <ajaxToolKit:MaskedEditValidator ID="mevToDt" runat="server" ControlExtender="meeToDt" ControlToValidate="txtenddate"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    ErrorMessage="Please Enter To Date  In [dd/MM/yyyy] format"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Schedule"
                                                    Display="None" Text="*"></ajaxToolKit:MaskedEditValidator>
                                                <%--EmptyValueMessage="Please Enter To Date" EmptyValueBlurredText="Empty" IsValidEmpty="false" --%>


                                                <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtenddate"
                                                    CultureInvariantValues="true" Display="None" ErrorMessage="End Date Must Be Equal To Or Greater Than Start Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                    ValidationGroup="Schedule" ControlToCompare="txtstartdate" />
                                            </div>



                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Committee </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true"
                                                 CssClass="form-control" data-select2-enable="true" AutoPostBack="true" ToolTip="Select Committee" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged"> <%--AutoPostBack="true"--%>
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Meeting Code</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMeeting" runat="server" AppendDataBoundItems="true"
                                                AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Meeting" OnSelectedIndexChanged="ddlMeeting_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Meeting code"
                                                        ValidationGroup="Schedule" InitialValue="0" ControlToValidate="ddlMeeting" Display="None"
                                                        Text="*">
                                                    </asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                             <div class="col-12 btn-footer">
                                <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="Schedule" CssClass="btn btn-primary" ToolTip="Click here to Show" OnClick="btnshow_Click" CausesValidation="true" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" ValidationGroup="Submit" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btncancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Schedule" />
                            </div>
                            <asp:Panel ID="pnlMeetingInfo" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Meeting List</h5>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlAgendaGrid" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvAgenda" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>SELECT
                                                                </th>
                                                                <%--<th>AGENDA NUMBER
                                                                                </th>--%>
                                                                <th>MEETING NUMBER
                                                                </th>
                                                                <th>MEETING TITLE
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record"
                                                            CausesValidation="false" CommandArgument='<%# Eval("PK_MEETINGDETAILS") %>' CommandName='<%# Eval("PK_AGENDA") %>'
                                                            ImageUrl="~/images/edit.png" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                        <asp:HiddenField ID="hdnagenda" runat="server" Value='<%# Eval("PK_AGENDA") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("MEETING_CODE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AGENDATITAL")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </asp:Panel>
                            <asp:Panel ID="pnlinfo" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Meeting Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Meeting Title</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txttitle" Enabled="false" CssClass="form-control" ToolTip="Title"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Meeting Date </label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtMeetingDate" Enabled="false" CssClass="form-control" ToolTip="Meeting Date"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Venue</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtvenue" Enabled="false" CssClass="form-control" ToolTip="Meeting Venue" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Meeting Code</label>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtcode" Enabled="false" CssClass="form-control" ToolTip="Meeting Code"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Meeting Description </label><%-- shaikh juned  27-06-22--%>
                                            </div>
                                            <asp:TextBox runat="server" ID="txtdetail" Enabled="false" TextMode="MultiLine" CssClass="form-control" ToolTip="Meeting Description"></asp:TextBox>

                                        </div>



                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlFile" runat="server" Visible="false">
                                <div class="col-12">
                                    <%-- <div class="sub-heading">
                                        <h5>Download Files</h5>
                                    </div>--%>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto" Visible="false">
                                        <asp:ListView ID="lvfile" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Download files</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action</th>
                                                                <th>File Name</th>
                                                                <th>Download</th>
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
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png"
                                                            CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>' AlternateText="Delete Record" ToolTip="Delete Record"
                                                            OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                                    </td>
                                                    <td>
                                                        <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.png" AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' OnClick="imgdownload_Click" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </asp:Panel>
                           
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <%-- Shaikh Juned (05/04/2022)----start----%>
            <asp:PostBackTrigger ControlID="lvfile" />
        </Triggers>
        <%-- Shaikh Juned (05/04/2022)--end----%>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">
        function totAllIDs(headchk) {
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
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../../IMAGES/expand_blue.jpg";
            } //../images/action_up.gif
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../../IMAGES/collapse_blue.jpg";
            }
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>
