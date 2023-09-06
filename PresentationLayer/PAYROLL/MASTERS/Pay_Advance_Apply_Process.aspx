<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Advance_Apply_Process.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_Advance_Apply_Process" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../CSS/master.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        function caldiff() {

            if ((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != null)) {

                var d = GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value));
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = (parseInt(d) + 1);
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN") {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                    }
                }

            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0) {
                alert("No. of Days can not be 0 or less than 0 ");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value)) {

                alert("No. of Days not more than Balance Days");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            return false;
        }

     
    </script>
   
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ADVANCE APPLY PROCESS</h3>
                </div>
                <div>
                    <form role="form">
                        <div class="box-body">

                            <div class="col-md-12">
                                <div class="form-group col-md-2"></div>
                                <div class="form-group col-md-4">
                                    <label>Advance Date :</label>
                                </div>
                                <div class="form-group col-md-4">
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <asp:Image ID="imgCalAdvdt" runat="server" ImageUrl="~/images/calendar.png"
                                                Style="cursor: pointer" />
                                        </div>
                                        <asp:TextBox ID="txtAdvdt" runat="server"  MaxLength="10" Style="z-index: 0;" TabIndex="12"
                                            CssClass="form-control" ToolTip="Enter Advance Date" />
                                        <asp:RequiredFieldValidator ID="rfvTodt" runat="server"
                                            ControlToValidate="txtAdvdt" Display="None" ErrorMessage="Enter Advance Date"
                                            SetFocusOnError="true" ValidationGroup="AdvAppPro"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtAdvdt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            TargetControlID="txtAdvdt" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt" ControlToValidate="txtAdvdt"
                                            Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                            TooltipMessage="Enter Advance Date" ValidationGroup="AdvAppPro">                                                    
                                                    &#160;&#160;
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>

                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="form-group col-md-2"></div>

                                <div class="form-group col-md-4">
                                    <label>Advance Amount :</label>
                                </div>
                                <div class="form-group col-md-4">
                                    <asp:TextBox ID="txtAdvanceAmt" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" CssClass="form-control" ToolTip="Advace Amount"
                                        TabIndex="9">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAdvAmt" runat="server" ControlToValidate="txtAdvanceAmt" ErrorMessage="Enter Advance Amount"  ValidationGroup="AdvAppPro">

                                    </asp:RequiredFieldValidator>
                                </div>

                            </div>

                            <div class="col-md-12">
                                <div class="form-group col-md-12">
                                    <div class="form-group col-md-2"></div>
                                    <div class="form-group col-md-4">
                                        <label>Reason :</label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" MaxLength="100" TextMode="MultiLine"
                                            ToolTip="Enter Reason" TabIndex="18" />
                                        <asp:RequiredFieldValidator ID="rfvReason" runat="server"
                                            ControlToValidate="txtReason" Display="None" ErrorMessage="Please Enter Reason"
                                            ValidationGroup="AdvAppPro"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12">
                               <div class="form-group col-md-12">
                                    <div class="form-group col-md-2"></div>
                                    <div class="form-group col-md-4">
                                        <label>Path :</label>
                                    </div>
                                     <div class="form-group col-md-4">
                                        <asp:TextBox ID="txtPath" runat="server" CssClass="form-control" ReadOnly="true"
                                              />
                                         </div>
                                </div>

                            </div>
                            <div class="form-group col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="AdvAppPro" TabIndex="23"
                                        CssClass="btn btn-success" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="24"
                                        CssClass="btn btn-danger" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                </p>
                            </div>

                            <div class="col-md-12">
                                <asp:Panel ID="pnllist" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading"> Apply Process List</div>
                                        <div class="panel panel-body">
                                            <asp:Panel ID="pnlLeaveCard" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvAdvAppProcess" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <asp:Label ID="lblErr" runat="server" Text="">
                                                        </asp:Label>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="lgv1">
                                                            <%--<h4 class="box-title">
                                                            Employee  Card
                                                        </h4>--%>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th style="width:120px">Advance Date 
                                                                        </th>
                                                                        <th style="width:150px">Advance Amount
                                                                        </th>
                                                                        <th>Reason 
                                                                        </th>  
                                                                         <th style="width:100px">Status 
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
                                                                <%# Eval("ADVCANEDATE") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ADVANCEAMOUNT") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("REASON") %>
                                                            </td>
                                                         <td>
                                                                <%# Eval("ADVCANSTATUS") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>



                                <asp:Panel ID="pnlLeaveStatus" runat="server">
                                    <asp:ListView ID="lvStatus" runat="server">
                                        <EmptyDataTemplate>
                                            <p class="text-center text-bold">
                                                <asp:Label ID="ibler" runat="server" Text="No more  aaplication"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="box-title"> Application Status
                                                </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th></th>
                                                            <th> Name
                                                            </th>
                                                            <th>Form Date
                                                            </th>
                                                            <th>Todate
                                                            </th>
                                                            <th>No of days
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <th>Application Report
                                                            </th>
                                                            <%-- <th>
                                                        Joining Report
                                                    </th>--%>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("LETRNO") %>'
                                                        AlternateText="Edit Record" ToolTip='<%# Eval("LNO")%>' />&nbsp;
                                                    <%--OnClick="btnEdit_Click" Enabled="true"--%>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LETRNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" />
                                                    OnClick="btnDelete_Click"
                                                        <%--OnClientClick="showConfirmDel(this); return false;" Enabled="true"--%>
                                                </td>
                                                <td>
                                                    <%# Eval("LEAVENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("From_date")%>
                                                </td>
                                                <td>
                                                    <%# Eval("To_date") %>
                                                </td>
                                                <td>
                                                    <%# Eval("No_of_days")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    <%--<%# Eval("Status") %>--%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnRPT" runat="server" Text="Application Report" CommandArgument='<%# Eval("LNO")%>'
                                                        ToolTip='<%# Eval("LETRNO")%>' CssClass="btn btn-info" TabIndex="3" />
                                                    <%--OnClick="btnRPT_Click"--%>
                                                </td>
                                                <%-- <td>
                                                <asp:Button ID="btnLeaveRPT" runat="server" Text="Joining Report" CommandArgument='<%# Eval("LNO")%>'
                                                ToolTip='<%# Eval("LETRNO")%>' OnClick="btnLeaveRPT_Click"  />
                                                
                                            </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="vista-grid_datapager">
                                        <div class="text-center">
                                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvStatus" PageSize="10">
                                                <%--OnPreRender="dpPager_PreRender"--%>
                                                <Fields>
                                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                        ShowLastPageButton="true" ShowNextPageButton="true" />

                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </div>

                                </asp:Panel>


                            </div>

                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
       <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />

                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>
            </td>
        </tr>
    <div id="divMsg" runat="server"></div>
</asp:Content>

