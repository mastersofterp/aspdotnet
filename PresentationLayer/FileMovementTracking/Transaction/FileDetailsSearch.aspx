<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FileDetailsSearch.aspx.cs" Inherits="FileMovementTracking_Transaction_FileDetailsSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>


    <%--<asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FILE DETAIL SEARCH</h3>
                </div>
                <div class="box-body">

                    <asp:Panel ID="pnlFile" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>File Detail Search</h5>
                            </div>

                            <div class="row">
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select </label>
                                    </div>
                                    <asp:RadioButtonList ID="rdbSearch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbSearch_SelectedIndexChanged" RepeatDirection="Horizontal">
                                        <%--   <asp:ListItem Value="0">File Name</asp:ListItem>
                                        <asp:ListItem Value="1">File No.</asp:ListItem>
                                        <asp:ListItem Value="2">File Creation Date</asp:ListItem>
                                        <asp:ListItem Value="3">Keywords</asp:ListItem>--%>
                                        <asp:ListItem Text="File Name&nbsp;&nbsp;&nbsp;&nbsp;" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="File No.&nbsp;&nbsp;&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Keywords&nbsp;&nbsp;&nbsp;&nbsp;" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="File Creation Date" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div id="divFileName" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>File Name </label>
                                    </div>
                                    <asp:DropDownList ID="ddlFileName" CssClass="form-control" runat="server" TabIndex="1"
                                        AppendDataBoundItems="true" ToolTip="Select File Name" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="mt-2">
                                        <asp:TextBox ID="txtFileName" runat="server" CssClass="form-control" TabIndex="9"
                                            placeholder="Enter Character to Search" ToolTip="Enter File Name"></asp:TextBox>
                                        <asp:HiddenField ID="hfFileId" runat="server" />

                                        <ajaxToolKit:AutoCompleteExtender ID="autAgainstAc" runat="server" TargetControlID="txtFileName"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                            ServiceMethod="GetSectionName" OnClientShowing="clientShowing" OnClientItemSelected="GetSUName">
                                        </ajaxToolKit:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div id="divFileNo" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>File No </label>
                                    </div>
                                    <asp:DropDownList ID="ddlFileNo" CssClass="form-control" runat="server" TabIndex="1" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select File No">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                    <div class="">
                                        <asp:TextBox ID="txtFileCode" runat="server" CssClass="form-control" TabIndex="9"
                                            placeholder="Enter Character to Search" ToolTip="Enter File No."></asp:TextBox>
                                        <asp:HiddenField ID="hdnFileIdForFC" runat="server" />
                                        <ajaxToolKit:AutoCompleteExtender ID="AutoFileNo" runat="server" TargetControlID="txtFileCode"
                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                            ServiceMethod="GetFileNameByCode" OnClientShowing="clientShowing" OnClientItemSelected="GetFileCode">
                                        </ajaxToolKit:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div id="divFileDate" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgFrmDt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>

                                        <asp:TextBox ID="txtstartdate" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Select From Date"></asp:TextBox>


                                        <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtstartdate"
                                            PopupButtonID="imgFrmDt" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>

                                        <ajaxToolKit:MaskedEditExtender ID="meeFrmDt" runat="server" TargetControlID="txtstartdate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />

                                        <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="meeFrmDt" ControlToValidate="txtstartdate"
                                            IsValidEmpty="true" ErrorMessage="From Date Is Invalid [ Enter dd/MM/yyyy Format]"
                                            InvalidValueMessage="From Date Is Invalid [ Enter dd/MM/yyyy Format]" Display="None" Text="*" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                            ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>

                                        <asp:RequiredFieldValidator ID="rfvFrmDate" runat="server" ControlToValidate="txtstartdate"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select From Date" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div id="divToDate" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="i1" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>

                                        <asp:TextBox ID="txtenddate" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Select To Date"></asp:TextBox>


                                        <%-- <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtenddate"
                                            PopupButtonID="imgTodt" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>--%>

                                        <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtenddate" PopupButtonID="imgTodt">
                                        </ajaxToolKit:CalendarExtender>

                                        <ajaxToolKit:MaskedEditExtender ID="meeToDt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                            TargetControlID="txtenddate" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>

                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeToDt"
                                            IsValidEmpty="true" ErrorMessage="To Date Is Invalid [ Enter dd/MM/yyyy Format] " ControlToValidate="txtenddate"
                                            InvalidValueMessage="To Date Is Invalid [ Enter dd/MM/yyyy Format]" Display="None" Text="*" ValidationGroup="Submit">
                                        </ajaxToolKit:MaskedEditValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtenddate"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select To Date" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtenddate"
                                            CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                            ValidationGroup="Submit" ControlToCompare="txtstartdate" />
                                    </div>
                                </div>



                                <%-- <div id="divFileToDate" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                               <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div >
                                        <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control" ToolTip="Select To Date" TabIndex="3"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgTodt" TargetControlID="txtenddate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeToDt" runat="server" TargetControlID="txtenddate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />

                                        <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtenddate"
                                            CultureInvariantValues="true" Display="None" ErrorMessage="End Date Must Be Equal To Or Greater Than Start Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                            ValidationGroup="Schedule" ControlToCompare="txtstartdate" />
                                    </div>
                                </div>--%>

                                <div id="divKeywords" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Keywords </label>
                                    </div>
                                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="form-control" TabIndex="10"
                                        ToolTip="Enter File No."></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>


                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Show" TabIndex="1" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="1" OnClick="btnReport_Click"
                            CssClass="btn btn-info" CausesValidation="false" ToolTip="Click here to Show Report" />
                        <asp:Button ID="btnStatusReport" runat="server" Text="Status Report" TabIndex="1" OnClick="btnStatusReport_Click"
                            CssClass="btn btn-info" CausesValidation="false" ToolTip="Click here to Show Report" />
                        <asp:Button ID="btnCancel" runat="server" AutoPostBack="true" Text="Cancel" TabIndex="1" OnClick="btnCancel_Click"
                            CssClass="btn btn-warning" CausesValidation="false" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                        <asp:ValidationSummary ID="VS2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server" Visible="false">
                            <asp:ListView ID="lvFile" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>File Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>SELECT
                                                    </th>
                                                    <th>FILE NO.
                                                    </th>
                                                    <th>FILE NAME
                                                    </th>
                                                    <th>FILE PATH
                                                    </th>
                                                    <th>CREATION DATE
                                                    </th>
                                                    <th>DOCUMENT TYPE
                                                    </th>
                                                    <th>MOVEMENT STATUS
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
                                            <asp:RadioButton ID="rdoFile" runat="server" AutoPostBack="true" ToolTip='<%# Eval("FILE_ID") %>'
                                                OnClick="javascript:SelectSingleRadiobutton(this.id)" OnCheckedChanged="rdoFile_CheckedChanged" />

                                        </td>
                                        <td>
                                            <%# Eval("FILE_CODE")%>
                                        </td>
                                        <td>
                                            <%# Eval("FILE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("FILEPATH") %>
                                        </td>
                                        <td>
                                            <%# Eval("CREATION_DATE", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("DOCUMENT_TYPE")%>
                                        </td>
                                        <td>
                                            <%# Eval("MOVEMENT_STATUS")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlMovemnt" runat="server">
                            <asp:ListView ID="lvFileMovement" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Movement Of The File</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>FILE NO.
                                                    </th>
                                                    <th>FILE NAME
                                                    </th>
                                                    <th>MOVEMENT DATE
                                                    </th>
                                                    <th>RECEIVER DETAILS
                                                    </th>
                                                    <th>MOVEMENT STATUS
                                                    </th>
                                                    <th>RECEIVE NOTE
                                                    </th>
                                                    <th>FORWARD/ RETURN NOTE
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
                                            <%# Eval("FILE_CODE")%>
                                        </td>
                                        <td>
                                            <%# Eval("FILE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("MOVEMENT_DATE", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("FNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("FILE_STATUS") %>
                                        </td>
                                        <td>
                                            <%# Eval("REMARK_RECEIVE") %>
                                        </td>
                                        <td>
                                            <%# Eval("REMARK_FOR_RET") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlNewFiles" runat="server" Visible="false">
                            <asp:ListView ID="lvNewFiles" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>List of documents </h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">

                                                    <th>File Name
                                                    </th>
                                                    <th>Download
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
                                            <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.png"
                                                AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' OnClick="imgdownloadNew_Click" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlFinalList" runat="server" Visible="false">
                            <asp:ListView ID="lvFinalList" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of documents</h5>
                                    </div>
                                    <div id="lgv1">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">

                                                    <th>File Name
                                                    </th>
                                                    <th>Download
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
                                            <asp:HiddenField ID="hdnFinalIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                            <asp:HiddenField ID="hdnFinalUPLNO" runat="server" Value='<%# Eval("UPLNO")%>' />
                                            <asp:Label ID="lblFinalFileName" runat="server" Text='<%# Eval("FILENAME")%> '></asp:Label>

                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.gif" CommandArgument='<%# Eval("FILEPATH")%>'
                                                AlternateText='<%# Eval("FILENAME")%>' OnClick="imgdownload_Click" CommandName='<%# Eval("UA_NO")%>' />
                                            <asp:HiddenField ID="hdnSourcePath" runat="server" Value='<%# Eval("SOURCEPATH")%>' />
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
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript">
        function GetSUName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtFileName').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hfFileId').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_ddlFileName').value = Name[0];
        }

    </script>

    <script type="text/javascript">
        function GetFileCode(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            var Name = idno[0].split("-");
            document.getElementById('ctl00_ContentPlaceHolder1_txtFileCode').value = idno[1];
            document.getElementById('ctl00_ContentPlaceHolder1_hdnFileIdForFC').value = Name[0];
            document.getElementById('ctl00_ContentPlaceHolder1_ddlFileNo').value = Name[0];
        }
    </script>

    <script type="text/javascript">
        function SelectSingleRadiobutton(rdbtnid) {
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }
    </script>

</asp:Content>

