<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RouteMaster.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_RouteMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <%--<asp:UpdatePanel ID="updPanel" runat="server">
      <ContentTemplate>    --%>
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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">ROUTE MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Create Route</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Route Name</label>
                                        </div>
                                        <asp:TextBox ID="txtRouteName" runat="server" MaxLength="50" CssClass="form-control"
                                            ToolTip="Enter Route Name" TabIndex="1"></asp:TextBox>
                                        <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbeRoute" runat="server" FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                            TargetControlID="txtRouteName" ValidChars="@,*,#,. ">
                                                        </ajaxToolKit:FilteredTextBoxExtender>--%>
                                        <asp:RequiredFieldValidator ID="rfvRouteName" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter Route Name."
                                            ValidationGroup="Submit" ControlToValidate="txtRouteName"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Route Number</label>
                                        </div>
                                        <asp:TextBox ID="txtRNumber" CssClass="form-control" ToolTip="Enter Route Number" TabIndex="2" runat="server"
                                            MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRNo" runat="server" ControlToValidate="txtRNumber"
                                            Display="None" ErrorMessage="Please Enter Route Number" SetFocusOnError="true" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Stop Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStopName" runat="server" TabIndex="3" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Stop Name">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblStopSeq" runat="server" Text=""></asp:Label>
                                        <asp:RequiredFieldValidator ID="rfvKM" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please Enter distance in KM."
                                            ValidationGroup="Add" ControlToValidate="txtKM"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvStopName" runat="server" SetFocusOnError="true" Display="None"
                                            ErrorMessage="Please select Stop."
                                            ValidationGroup="Add" ControlToValidate="ddlStopName" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="VS2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Add" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:CheckBox ID="chkSource" runat="server" TabIndex="4" Text="Source Station"
                                            onchange="CheckDetails()" ToolTip="Check for Source Station" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivAddRoute" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Add Below Which Stop ?</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAddStop" runat="server" TabIndex="3" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Stop Name">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>s
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Distance(KM)</label>
                                        </div>
                                        <asp:TextBox ID="txtKM" runat="server" TabIndex="5" ValidationGroup="Add"
                                            ToolTip="Enter KM from Source Station" CssClass="form-control"
                                            onkeypress="return isNumberKey(event)" MaxLength="8"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Fees</label>
                                        </div>
                                        <asp:TextBox ID="txtFees" runat="server" TabIndex="5" ValidationGroup="Add"
                                            ToolTip="Enter Fees" CssClass="form-control" MaxLength="10"
                                            onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12 btn-footer mb-4">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" TabIndex="6"
                                            ValidationGroup="Add" ToolTip="Click here to Add Distance(Km)" CssClass="btn btn-primary" />

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Route Path</label>
                                        </div>
                                        <asp:TextBox ID="txtRoutePath" runat="server" MaxLength="300" ToolTip="Route Path"
                                            CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRoute" runat="server" ControlToValidate="txtRoutePath" Display="None"
                                            ErrorMessage="Please Create Route Path." SetFocusOnError="true" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSeq" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Sequence No </label>
                                        </div>
                                        <asp:TextBox ID="txtSeqNo" runat="server" MaxLength="3" onkeypress="return CheckNumeric(event, this);"
                                            TabIndex="7" CssClass="form-control" ToolTip="Enter Sequence Number">                                
                                        </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                            TargetControlID="txtSeqNo" ValidChars="+- ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvSeqNo" runat="server" ControlToValidate="txtSeqNo" Display="None"
                                            ErrorMessage="Please Enter Sequence No." SetFocusOnError="true" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Starting Time</label>
                                        </div>
                                        <asp:TextBox ID="txtStartimeRoute" CssClass="form-control" ToolTip="Enter Starting Time" TabIndex="8" runat="server">
                                        </asp:TextBox>
                                        <%-- <ajaxToolKit:MaskedEditExtender ID="meeStartimeRoute" runat="server" TargetControlID="txtStartimeRoute"
                                            Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True" />--%>

                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtStartimeRoute"
                                            Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                            CultureTimePlaceholder="" Enabled="True" AutoComplete="False" />

                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txtStartimeRoute"
                                            IsValidEmpty="false" ErrorMessage="Starting Time Is Invalid [Enter HH:MM (AM/PM) Format]" EmptyValueMessage="Please Enter Starting Time"
                                            InvalidValueMessage="Starting Time Is Invalid [Enter HH:MM (AM/PM) Format]" Display="None" SetFocusOnError="true"
                                            Text="*" ValidationGroup="Submit" ViewStateMode="Enabled"></ajaxToolKit:MaskedEditValidator>

<%--                                        <asp:RequiredFieldValidator ID="rfvstartimrRoute" runat="server" ControlToValidate="txtStartimeRoute"
                                            Display="None" ErrorMessage="Please Enter Starting Time" SetFocusOnError="true" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Vehicle Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlvehicletype" runat="server" ToolTip="Select Vehicle Type" TabIndex="9" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="A/C" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Non A/c" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvehicletype" runat="server" ControlToValidate="ddlvehicletype" ErrorMessage="Please Select Vehicle Type." InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                
                                        <asp:Panel ID="lvPanel" runat="server">
                                            <asp:ListView ID="lvRoutePath" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>Stop List for Route</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Delete </th>
                                                                    <th>Stop Name </th>
                                                                    <th>Distance(KM) </th>
                                                                    <th>Fees </th>
                                                                    <%-- <th style="width: 15%">Seq. No</th>--%>
                                                                    <th></th>
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
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>' ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" ToolTip="Delete Record" />
                                                        </td>
                                                        <td><%# Eval("STOPNAME") %></td>
                                                        <td><%# Eval("DISTANCE") %></td>
                                                        <td><%# Eval("ROUTE_FEES") %></td>
                                                        <%--<td style="width: 15%">
                                                                <asp:Label ID="lblSequenceNo" runat="server" Text='<%# Eval("SEQNO")%>'></asp:Label>
                                                            </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                   
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" CausesValidation="true" OnClick="btnSubmit_Click" TabIndex="10"
                                    Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                <%--//OnClientClick="timevalidation();"--%>
                                <asp:Button ID="btnRport" runat="server" OnClick="btnRport_Click" TabIndex="12" Text="Report"
                                    CssClass="btn btn-info" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                                    TabIndex="11" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12 mb-3">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvRoute" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Route Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT </th>
                                                            <th>ROUTE NUMBER</th>
                                                            <th>ROUTE NAME </th>
                                                            <th>ROUTE PATH </th>
                                                            <th>DISTANCE (IN KM)</th>
                                                            <th>VEHICLE TYPE</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("ROUTEID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("ROUTE_NUMBER")%></td>
                                                <td><%# Eval("ROUTENAME")%></td>
                                                <td><%# Eval("ROUTEPATH")%></td>
                                                <td><%# Eval("DISTANCE")%></td>
                                                <td><%#Eval("VEHICLE_TYPE")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                    <div class="vista-grid_datapager d-none">
                                        <div class="text-center">
                                            <%-- <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvRoute" PageSize="10" OnPreRender="dpPager_PreRender">
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
                                            </asp:DataPager>--%>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
      <Triggers>
          <asp:PostBackTrigger ControlID="btnAdd" />
          <asp:PostBackTrigger ControlID="lvRoutePath" />
      </Triggers>
    </asp:UpdatePanel>
<%--</ContentTemplate>  
  </asp:UpdatePanel>--%>


    <script type="text/javascript" language="javascript">



        function myFunction() {
            alert('xxx');
            document.getElementById("chkSource").disabled = true;
            document.getElementById("txtKM").value = 0;
        }

        function CheckDetails() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_chkSource').checked == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_txtKM').value = "0";
                document.getElementById('ctl00_ContentPlaceHolder1_txtKM').disabled = true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_txtKM').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtKM').disabled = false;
            }
        }


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = document.getElementById('ctl00_ContentPlaceHolder1_txtKM').value.length;
                var index = document.getElementById('ctl00_ContentPlaceHolder1_txtKM').value.indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
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


        function timevalidation() {
            if (document.getElementById('<%=txtStartimeRoute.ClientID%>').value != '') {
                // var date_regex = /^(0[1-9]|[1][0-2])[:]" + "(0[0-9]|[1-5][0-9])[ ][A|a|P|p][M|m]/;
                //  var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                var date_regex = /^(1[0-2]|0[1-9]):([0-5][0-9])(\s*)(i?[AP]M])$/;
                if (!(date_regex.test(document.getElementById('<%= txtStartimeRoute.ClientID %>').value))) {
                    alert("Starting Time Is Invalid (Enter In [MM:SS] Format).");
                    return false;
                }
            }
        }
    </script>
</asp:Content>




