<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehicleRouteAllotment.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_VehicleRouteAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .numeric_button {
            background-color: #384B69;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
            border: none;
        }

        .current_page {
            background-color: #09151F;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
        }

        .next_button {
            background-color: #1F3548;
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            padding: 2px;
        }

        .style1 {
            height: 28px;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VEHICLE ROUTE ALLOTMENT</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:RadioButtonList ID="rdblistVehicleType" runat="server" RepeatDirection="Horizontal" ToolTip="Select Vehicle Type"
                                        OnSelectedIndexChanged="rdblistVehicleType_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                        <asp:ListItem Selected="True" Value="1">College Vehicles</asp:ListItem>
                                        <asp:ListItem Value="2">Contract Vehicles</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle</label>
                                    </div>
                                    <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        ToolTip="Select Vehicle" AutoPostBack="false" TabIndex="2" OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblVType" runat="server" Text=""></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvVehicle" runat="server" ErrorMessage="Please Select Vehicle."
                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlVehicle" Display="Dynamic" Text="*">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Driver</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDriver" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        ToolTip="Select Driver Name" TabIndex="3">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="trRouteDrop" runat="server" visible="true">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Route</label>
                                    </div>
                                    <asp:DropDownList ID="ddlRoute" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        AutoPostBack="false" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged" TabIndex="4" ToolTip="Select Route">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRoute" runat="server" ErrorMessage="Please Select Route." Text="*"
                                        ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlRoute" Display="None">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="tdFDt" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="img3">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" ToolTip="Enter From Date" ValidationGroup="Submit"
                                            TabIndex="6" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtFrmDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                            Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                            TargetControlID="txtFrmDt" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                            ControlToValidate="txtFrmDt" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            Display="None" Text="*" ValidationGroup="Submit"> </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="tdTDt" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image2">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>

                                        <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control" ToolTip="Enter To Date" ValidationGroup="Submit"
                                            TabIndex="7" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtToDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            OnInvalidCssClass="errordate" TargetControlID="txtToDt" ClearMaskOnLostFocus="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                            ControlToValidate="txtToDt" Display="None" Text="*" ValidationGroup="Submit"
                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" />
                                        <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFrmDt" ControlToValidate="txtToDt"
                                            ErrorMessage="To Date Should Greater Than From Date" runat="server" Operator="GreaterThan"
                                            SetFocusOnError="True" Type="Date" ValidationGroup="Submit" Display="None" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trDeparture" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Departure Time </label>
                                    </div>
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtDepTime" runat="server" TabIndex="8" CssClass="form-control"
                                            ToolTip="Enter Departure Time"></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="meDepTime" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true"
                                            OnInvalidCssClass="errordate" TargetControlID="txtDepTime" AcceptAMPM="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevDepTime" runat="server" ControlExtender="meDepTime"
                                            ControlToValidate="txtDepTime" Display="None" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Time" InvalidValueMessage="Departure Time is Invalid (Enter 12 Hour Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter Departure Time" ValidationGroup="Add" IsValidEmpty="false">                                                       
                                        </ajaxToolKit:MaskedEditValidator>
                                        <div class="input-group-addon">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add Time" CssClass="btn btn-primary"
                                                OnClick="btnAdd_Click" TabIndex="9" ValidationGroup="Add" ToolTip="Click here to Add Departure Time" Visible="false" />
                                        </div>
                                    </div>
                                </div>




                            </div>
                        </div>
                    </asp:Panel>

                    <div class="col-12">
                        <asp:Panel ID="lvPanel" runat="server" ScrollBars="Auto" Visible="false">
                            <%--  <asp:ListView ID="lvDeparture" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <h4 class="box-title">List Of Departure Time
                                                    </h4>
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Delete </th>
                                                                <th>DEPARTURE TIME</th>
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
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/images/delete.gif" OnClick="btnDeleteRec_Click" ToolTip="Delete Record" />
                                                    </td>
                                                    <td><%# Eval("DEPARTURE_TIME") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>--%>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                            OnClick="btnSubmit_Click" TabIndex="10" ToolTip="Click here to Submit" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click"
                            TabIndex="12" ToolTip="Click here to Show Report" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                            TabIndex="11" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="VS1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" HeaderText="Following Fields are mandatory" />
                        <asp:ValidationSummary ID="VS2" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="List" ValidationGroup="Add" HeaderText="Following Fields are mandatory" />
                    </div>
                    <div class="col-12 mt-3">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvVRAllotment" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Vehicle Route Allotment List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>ACTION                        
                                                    </th>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>DRIVER
                                                    </th>
                                                    <th>ROUTE NAME
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
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png"
                                                CommandArgument='<%# Eval("RAID") %>' ToolTip="Delete Record"
                                                OnClientClick="showConfirmDel(this); return false;" OnClick="btnDelete_Click" />
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("RAID") %>'
                                                ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("VEHNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("DNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("ROUTENAME")%>
                                        </td>
                                        <%-- <td style="width:10%">
                                            <%# Eval("FDATE", "{0:dd-MMM-yyyy}")%>
                                        </td>--%>

                                        <%--  <td style="width:10%">
                                            <%# Eval("TDAT", "{0:dd-MMM-yyyy}")%>
                                        </td>--%>
                                        <%-- <td style="width:10%">
                                            <%# Eval("DTIME","{0:hh:mm tt}")%>
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="vista-grid_datapager d-none">
                                <div class="text-center">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvVRAllotment" PageSize="10" OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField
                                                FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true"
                                                ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link"
                                                ButtonCount="7" CurrentPageLabelCssClass="current" />
                                            <asp:NextPreviousPagerField
                                                LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true"
                                                ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">

        function CheckAlphabetSpecial(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 44) {  // k == 44 use for comma  

                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {

                obj.style.backgroundColor = "White";
                return true;
            }

            else {

                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>

    <asp:UpdatePanel ID="updActivity" runat="server">
    </asp:UpdatePanel>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <div class="col-md-12">
        <div class="text-center">
            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                <div class="text-center">
                    <div class="modal-content">
                        <div class="modal-body">
                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                            <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                            <div class="text-center">
                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>

    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>
</asp:Content>
