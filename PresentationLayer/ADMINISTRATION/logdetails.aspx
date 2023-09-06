<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="logDetails.aspx.cs" Inherits="Administration_logDetais" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        var ddlText, ddlValue, ddl, lblMesg;
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=ddlName.ClientID %>");
            for (var i = 0; i < ddl.options.length; i++) {
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }
        }
        window.onload = CacheItems;
        function FilterItems(value) {
            ddl.options.length = 0;
            alert(ddl.options.length);
            for (var i = 0; i < ddlText.length; i++) {
                if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                    AddItem(ddlText[i], ddlValue[i]);
                }
            }
            lblMesg.innerHTML = ddl.options.length + " items found.";
            if (ddl.options.length == 0) {
                AddItem("No items found.", "");
            }
        }
        function AddItem(text, value) {
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }

        function GetDropDownText(txtusername) {
            //onkeyup = "FilterItems(this.value)"
            __doPostBack("GetDropDownText", "");
        }

        function RunThisAfterEachAsyncPostback() {

            $(function () {
                $("#<%=txtTodt.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                    yearRange: '1975:' + getCurrentYear()
                });
            });

            $(function () {
                $("#<%=txtFromdt.ClientID%>").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'dd/mm/yy',
                    yearRange: '1975:' + getCurrentYear()
                });
            });

            function getCurrentYear() {
                var cDate = new Date();
                return cDate.getFullYear();
            }
        }
    </script>

    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtUsername]');
            var $ddl = $('select[id$=ddlName]');
            var $items = $('select[id$=ddlName] option');

            $txt.keyup(function () {
                // alert('hi');
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                    );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Users Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " Users found");
                }

            }
        });
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Log Details</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-6 col-sm-12 col-12" id="Username" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:TextBox ID="txtUsername" runat="server" onkeyup="FilterItems(this.value)" BackColor="AliceBlue" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Username</label>
                                        </div>
                                        <asp:DropDownList ID="ddlName" AppendDataBoundItems="true" runat="server"
                                            OnSelectedIndexChanged="ddlName_SelectedIndexChanged" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlName"
                                            Display="None" ErrorMessage="Please select Username" ValidationGroup="logDetails"
                                            InitialValue="-1"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="ftv" runat="server" TargetControlID="txtUsername"
                                            WatermarkText="Search User Here">
                                        </ajaxToolKit:TextBoxWatermarkExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-sm-12 col-12" id="dateuser" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalFromdt">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromdt" runat="server" CssClass="form-control" TabIndex="1" onchange="return CheckFutureDate1(this);"
                                                MaxLength="12"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFromdt" runat="server" ControlToValidate="txtFromdt"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="logDetails"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <%-- <asp:Image ID="imgCalFromdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer"
                                        meta:resourcekey="imgCalFromdtResource1" />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromdt"
                                                PopupButtonID="imgCalFromdt" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server" TargetControlID="txtFromdt"
                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server" ControlExtender="meeFromdt"
                                                ControlToValidate="txtFromdt" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="logDetails" SetFocusOnError="True"
                                                ErrorMessage="mevFromdt" meta:resourcekey="mevFromdtResource1">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalTodt">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodt" runat="server" CssClass="form-control" MaxLength="12"  TabIndex="1" onchange="return CheckFutureDate1(this);" />
                                            <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="logDetails"
                                                SetFocusOnError="True" meta:resourcekey="rfvTodtResource1">
                                            </asp:RequiredFieldValidator>
                                            <%--<asp:Image ID="imgCalTodt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer"
                                        meta:resourcekey="imgCalTodtResource1" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodt"
                                                PopupButtonID="imgCalTodt" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" TargetControlID="txtTodt"
                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt"
                                                ControlToValidate="txtTodt" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="logDetails" SetFocusOnError="True"
                                                ErrorMessage="mevTodt">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="logDetails"
                            CssClass="btn btn-primary" OnClientClick="return ValidateDate();" TabIndex="1"/>
                        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click"
                            ValidationGroup="logDetails" CssClass="btn btn-primary" OnClientClick="return ValidateDate();"  TabIndex="1"/>

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="logDetails"
                            CssClass="btn btn-warning" CausesValidation="False" OnClick="btnCancel_Click"  TabIndex="1"/>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="logDetails"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlTimeTable" runat="server">
                            <asp:ListView ID="lvList" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Login Details</h5>
                                    </div>
                                    <table id="examplez" class="table table-responsive table-hover table-bordered table-striped display" style="width:100%;">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>User Name
                                                </th>
                                                <th>IP Address
                                                </th>
                                                <th>Mac Address
                                                </th>
                                                <th>Login Time
                                                </th>
                                                <th>Logout Time
                                                </th>
                                                <th>Activity Details
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>

                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("Ua_Name") %>
                                        </td>
                                        <td>
                                            <%# Eval("IPADDRESS")%>
                                        </td>
                                        <td>
                                            <%# Eval("MACADDRESS")%>
                                        </td>
                                        <td>
                                            <%#GetLogTime(Eval("LoginTime"))%>
                                        </td>
                                        <td>
                                            <%#GetLogTime(Eval("LogoutTime"))%>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDetails" runat="server" Text="View Details"
                                                CommandArgument='<%#Eval("ID") %>' OnClick="btnDetails_Click" CssClass="btn btn-info" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>


                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                            <%--    <asp:ListView ID="lvList" runat="server">
                        <LayoutTemplate>
                            <div>                                
                                   <h4> Login Details </h4>
                                <table id="tbllogdetails" class="table table-hover table-bordered table-responsive table-striped">
                                    <thead>
                                    <tr class="bg-light-blue">
                                        <th>
                                            User Name
                                        </th>
                                        <th>
                                            IP Address
                                        </th>
                                        <th>
                                            Mac Address
                                        </th>
                                        <th >
                                            Login Time
                                        </th>
                                        <th>
                                            Logout Time
                                        </th>
                                        <th >
                                            Activity Details
                                        </th>
                                    </tr></thead>
                                    <tbody>  <tr id="itemPlaceholder" runat="server" /></tbody>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr >
                                <td>
                                    <%# Eval("Ua_Name") %>
                                </td>
                                <td>
                                    <%# Eval("IPADDRESS")%>
                                </td>
                                <td>
                                    <%# Eval("MACADDRESS")%>
                                </td>
                                <td>
                                    <%#GetLogTime(Eval("LoginTime"))%>
                                </td>
                                <td>
                                    <%#GetLogTime(Eval("LogoutTime"))%>
                                </td>
                                <td>
                                    <asp:Button ID="btnDetails" runat="server" Text="View Details" 
                                        CommandArgument='<%#Eval("ID") %>' OnClick="btnDetails_Click" CssClass="btn btn-info" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        </asp:ListView>--%>
                        </asp:Panel>
                    </div>
                    <div class="col-12" runat="server" id="divDetails" visible="false">
                        <div class="sub-heading">
                            <h5>Activity Details</h5>
                        </div>
                        <asp:UpdatePanel ID="pnlDetails" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="lvDetails" runat="server">
                                    <LayoutTemplate>
                                        <div class="vista-grid">
                                            <table id="examplez1" class="table table-hover table-bordered table-striped display" style="width:100%;">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>User Name
                                                        </th>
                                                        <th>Page
                                                        </th>
                                                        <th>Activity Time
                                                        </th>
                                                        <th hidden>Status
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
                                                <%# Eval("UA_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("PAGE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("LOG_TIME")%>
                                            </td>
                                            <td hidden>
                                                <%--<%# Eval("STATUS")%>--%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>

                                <%--<div class="vista-grid_datapager">
                                    <asp:DataPager ID="dpPager2" runat="server" PagedControlID="lvDetails" PageSize="5"
                                        OnPreRender="dpPager_PreRenderDetails">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>--%>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" Visible="false" CssClass="btn btn-primary" OnClick="btnBack_Click"></asp:Button>
                                    <asp:Button ID="btnExcelDetails" runat="server" Text="Export to Excel" OnClick="btnExcelDetails_Click"
                                        Visible="false" CssClass="btn btn-primary" />
                                </div>  
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExcelDetails" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript" language="JavaScript">
        function ValidateDate() {
            //Created by Mr. Manish Walde
            var validate = false;
            if (Page_ClientValidate()) {
                var fromDate = document.getElementById('<%= txtFromdt.ClientID %>').value;
                var fromdateArr = fromDate.split('/');
                var fromDate = fromdateArr[2] + '/' + fromdateArr[1] + '/' + fromdateArr[0];

                var toDate = document.getElementById('<%= txtTodt.ClientID %>').value;
                var todateArr = toDate.split('/');
                var toDate = todateArr[2] + '/' + todateArr[1] + '/' + todateArr[0];

                if (fromDate.valueOf() > toDate.valueOf()) {
                    alert('From Date Cannot be Greater Than To Date. Please Enter Valid Dates.');
                    document.getElementById('<%= txtFromdt.ClientID %>').focus();
                }
                else {
                    validate = true;
                }
            }
            return validate;
        }

        function CheckFutureDate(id) {
            //Created by Mr. Manish Walde
            // Return today's date and time
            var date = new Date();
            //var today = $.datepicker.formatDate("dd-mm-yy", date);

            // returns the month (from 0 to 11)
            var month = date.getMonth() + 1;
            //month = month - 1;
            //typeof month;

            if (month.toString().length == 1)
                month = "0" + month;

            // returns the day of the month (from 1 to 31)
            var day = date.getDate();
            if (String(day).length == 1)
                day = "0" + day;

            // returns the year (four digits)
            var year = date.getFullYear();

            var today = year + "/" + month + "/" + day;

            var enterDate = id.value;
            var dateArr = enterDate.split('/');
            var enterDate = dateArr[2] + '/' + dateArr[1] + '/' + dateArr[0];

            if (enterDate.valueOf() > today.valueOf()) {
                alert('Future date cannot be entered as Admission Date.');
                id.value = '';
                id.value = day + "/" + month + "/" + year;
                id.focus();
            }
        }

        function CheckFutureDate1(date1) {
            //alert(date1.value);
            var date = date1.value.split("/");
            var Selected_Date = new Date(date[2], date[1] - 1, date[0]);
            var current_Date = Date.now();
            if (Selected_Date > current_Date) {
                alert('Future date not allowed.');
                date1.value = '';
                date1.id.focus();
                return false;
            }
            else {

                if (date1.id == 'ctl00_ContentPlaceHolder1_txtToDate') {
                    var Fromdate = document.getElementById('<%= txtFromdt.ClientID%>').value.split('/');
                    var from_date = new Date(Fromdate[2], Fromdate[1] - 1, Fromdate[0]);
                    if (Selected_Date < from_date) {
                        alert('To Date should be greater than or equal to From Date.');
                        date1.value = '';
                        date1.id.focus();
                        return false;
                    }
                    else {

                        return true;
                    }
                }
            }
        }
    </script>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#examplez').DataTable({
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]

            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            bindDataTable1();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable1() {
            var myDT = $('#examplez1').DataTable({

                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]
            });
        }
    </script>--%>
</asp:Content>
