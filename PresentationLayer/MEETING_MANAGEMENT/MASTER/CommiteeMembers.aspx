<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CommiteeMembers.aspx.cs" Inherits="MEETING_MANAGEMENT_Master_CommiteeMembers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 320px; left: 520px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <script type="text/javascript">                                               //Saahil Trivedi 14-01-2021
        function CheckDateEalier(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("Back Dates Are Not Allowed");
                sender._selectedDate = new Date();
                sender._textbox.set_Value("");
            }
        }
    </script>


    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>

    <style type="text/css">
        .Calendar .ajax__calendar_body {
            width: 200px;
            height: 170px; /* modified */
            position: relative;
            border: solid 0px;
        }

        .Calendar .ajax__calendar_container {
            background-color: #ffffff;
            border: 1px solid #646464;
            color: #000000;
            width: 195px;
            height: 210px;
        }

        .Calendar .ajax__calendar_footer {
            border: solid top 1px;
            cursor: pointer;
            padding-top: 3px;
            height: 6px;
            background-color: #ebf4f8;
        }

        .Calendar .ajax__calendar_day {
            cursor: pointer;
            height: 17px;
            padding: 0 2px;
            text-align: right;
            width: 18px;
        }

        .Calendar .ajax__calendar_year {
            border: solid 1px #E0E0E0;
            /*font-family: Tahoma, Calibri, sans-serif;*/
            font-family: Verdana;
            font-size: 10px;
            text-align: center;
            font-weight: bold;
            text-shadow: 0px 0px 2px #D3D3D3;
            text-align: center !important;
            vertical-align: middle;
            margin: 1px;
            height: 40px; /* added */
        }

        .Calendar .ajax__calendar_month {
            border: solid 1px #E0E0E0;
            /*font-family: Tahoma, Calibri, sans-serif;*/
            font-family: Verdana;
            font-size: 10px;
            text-align: center;
            font-weight: bold;
            text-shadow: 0px 0px 2px #D3D3D3;
            text-align: center !important;
            vertical-align: middle;
            /* margin: 1px;
        height: 40px; /* added */
        }
    </style>
    
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updActivity"
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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MEMBER ALLOTMENT</h3>
                        </div>
                        <div class="box-body">
                           <%-- calender issue resolved   whole page designed 03-03-2022 modified ashwini--%>
                            <asp:Panel ID="pnlDesig" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCollegeName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Institute Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Committee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Select Committee " OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" TabIndex="3">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                              </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:Panel ID="pnlCllaimInfo" runat="server">
                                    <div class="col-12">
                                        <asp:ListView ID="lvmember" runat="server" Visible="false" OnItemDataBound="lvmember_ItemDataBound">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>MEMBER LIST</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" Visible="true" />
                                                                    Select
                                                                </th>
                                                                <th>Member Name
                                                                </th>
                                                                <th>Start Date
                                                                </th>
                                                                <th>End Date
                                                                </th>
                                                                <th>Designation
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
                                                        <asp:CheckBox ID="chkRow" runat="server" OnCheckedChanged="chkRow_CheckedChanged" ToolTip='<%# Eval("PK_CMEMBER")%>' AutoPostBack="true" TabIndex="4" />
                                                        <asp:HiddenField ID="hdnmember" runat="server" Value='<%# Eval("PK_CMEMBER")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="i1" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtstartdate" runat="server" Style="text-align: right" TabIndex="5" CssClass="form-control" ToolTip="Select Date"></asp:TextBox>

                                                            <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Enabled="true" CssClass="Calendar"
                                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="i1" TargetControlID="txtstartdate"/> <%--OnClientDateSelectionChanged="CheckDateEalier"--%>

                                                            <ajaxToolKit:MaskedEditExtender ID="meeFrmDt" runat="server" Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus"
                                                                MessageValidatorTip="true" MaskType="Date" AcceptNegative="None" ErrorTooltipEnabled="true" TargetControlID="txtstartdate"
                                                                OnInvalidCssClass="errordate" ClearMaskOnLostFocus="true" />

                                                            <ajaxToolKit:MaskedEditValidator ID="mevFrmDt" runat="server" ControlExtender="meeFrmDt"
                                                                ControlToValidate="txtstartdate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="true"
                                                                ErrorMessage="Please Enter Valid Date In format" EmptyValueBlurredText="*"
                                                                InvalidValueMessage="Please Enter Valid Date In format" Display="None" ValidationGroup="submit" SetFocusOnError="true" />
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="i2" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtenddate" runat="server" Style="text-align: right" TabIndex="6" CssClass="form-control" ToolTip="Select Date"></asp:TextBox>

                                                            <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtenddate"
                                                                CultureInvariantValues="true" Display="None" ErrorMessage="End Date Must Be Equal To Or Greater Than Start Date."
                                                                Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="Submit"
                                                                ControlToCompare="txtstartdate" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" CssClass="Calendar"
                                                                TargetControlID="txtenddate" PopupButtonID="i2" Enabled="true" EnableViewState="true"/>  <%--OnClientDateSelectionChanged="CheckDateEalier"--%>

                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtenddate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFrmDt"
                                                                ControlToValidate="txtenddate" EmptyValueMessage="Please Enter Date" InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                Display="None" TooltipMessage="Please Enter Meeting Date" EmptyValueBlurredText="Empty"
                                                                InvalidValueBlurredMessage="Invalid Date" SetFocusOnError="true" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true"
                                                            AutoPostBack="true" CssClass="form-control" ToolTip="Select Designation" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" TabIndex="7">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDesignation"
                                                            Display="None" ErrorMessage="Please Select Designation" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("USERID")%>' />
                                                    </td>
                                                    <%--<td>
                                                                        <asp:HiddenField ID="hdnUserid" runat="server" Value='<%# Eval("USERID")%>' />
                                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </asp:Panel>

                            </asp:Panel>



                            <div class=" col-12 btn-footer mt-4">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" TabIndex="8" />
                                <asp:Button ID="btnPrint" runat="server" Text="Report" Visible="true" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnPrint_Click" TabIndex="10" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="9" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" HeaderText="Following Field(s) are mandatory" />

                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">

        function calendarShown(sender, args) {
            //alert('inside');
            sender._popupBehavior._element.style.zIndex = 999999;  //5000; //1000; //99999999//10005; // 999999;
        }

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
        function SelectAll(mainChk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (mainChk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }


    </script>

</asp:Content>
