<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventCreation.aspx.cs" Inherits="ACADEMIC_EVENT_EventCreation" MasterPageFile="~/SiteMasterPage.master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div5" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Event Creation</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                <span style="color: red">* </span>
                                <label>Event Title</label>
                                <asp:TextBox ID="txtEventTitle" runat="server" TabIndex="1" CssClass="form-control" ValidationGroup="Submit" MaxLength="128"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEvent" runat="server" ValidationGroup="Submit" ControlToValidate="txtEventTitle" Display="None"
                                    ErrorMessage="Please Enter Event Title"></asp:RequiredFieldValidator>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" InvalidChars="`~!@#$%^&*()_-++{[}}:;'|\,>?/"
                                    FilterMode="InvalidChars" TargetControlID="txtEventTitle">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                <span style="color: red">* </span>
                                <label>Event Type</label>
                                <asp:DropDownList ID="ddlEventType" runat="server" TabIndex="2" CssClass="form-control" ValidationGroup="Submit" AppendDataBoundItems="true" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Submit" ControlToValidate="ddlEventType" Display="None"
                                    ErrorMessage="Please Select Event Type" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                <span style="color: red">* </span>
                                <label>Event Start Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon" id="icon">
                                        <div class="fa fa-calendar"></div>
                                    </div>

                                    <asp:TextBox ID="txtEventStart" runat="server" TabIndex="3" CssClass="form-control" ValidationGroup="Submit"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Submit" ControlToValidate="txtEventStart" Display="None"
                                        ErrorMessage="Please Enter Event Start Date"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:CalendarExtender ID="ceAdmDt" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtEventStart" PopupButtonID="icon" Enabled="True">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="meeAdmDt" runat="server" TargetControlID="txtEventStart"
                                        Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                </div>
                            </div>


                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                <span style="color: red">* </span>
                                <label>Event End Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon" id="Div2">
                                        <div class="fa fa-calendar"></div>
                                    </div>

                                    <asp:TextBox ID="txtEventEnd" runat="server" TabIndex="4" CssClass="form-control" onchange="return EndDateVlidation();" ValidationGroup="Submit" OnTextChanged="txtEventEnd_TextChanged" AutoPostBack="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Submit" ControlToValidate="txtEventEnd" Display="None"
                                        ErrorMessage="Please Enter Event End Date"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtEventEnd" PopupButtonID="Div2" Enabled="True">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEventEnd"
                                        Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                <span style="color: red">* </span>
                                <label>Event Registration Start Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon" id="Div3">
                                        <div class="fa fa-calendar"></div>
                                    </div>

                                    <asp:TextBox ID="txtRegStart" runat="server" TabIndex="5" CssClass="form-control" onchange="return RegistarStartDateValidation();" ValidationGroup="Submit" OnTextChanged="txtRegStart_TextChanged" AutoPostBack="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Submit" ControlToValidate="txtRegStart" Display="None"
                                        ErrorMessage="Please Enter Event Registration Start Date"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtRegStart" PopupButtonID="Div3" Enabled="True">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtRegStart"
                                        Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                </div>
                            </div>
                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                <span style="color: red">* </span>
                                <label>Event Registration End Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon" id="Div4">
                                        <div class="fa fa-calendar"></div>
                                    </div>

                                    <asp:TextBox ID="txtRegEnd" runat="server" TabIndex="6" CssClass="form-control" onchange="return RegistarEndDateValidation();" ValidationGroup="Submit" OnTextChanged="txtRegEnd_TextChanged" AutoPostBack="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Submit" ControlToValidate="txtRegEnd" Display="None"
                                        ErrorMessage="Please Enter Event Registration End Date"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtRegEnd" PopupButtonID="Div4" Enabled="True">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtRegEnd"
                                        Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                <span style="color: red">* </span>
                                <label>Event Venue</label>
                                <asp:TextBox ID="txtVenue" runat="server" CssClass="form-control" ValidationGroup="Submit" TabIndex="7" MaxLength="128"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="Submit" ControlToValidate="txtVenue" Display="None"
                                    ErrorMessage="Please Enter Event Venue"></asp:RequiredFieldValidator>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" InvalidChars="`~!@#$%^&*()_-++{[}}:;'|\,>?/"
                                    FilterMode="InvalidChars" TargetControlID="txtVenue">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="col-lg-3 col-md-6 col-12 form-group">
                                <span style="color: red">* </span>
                                <label>Event Description</label>
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" ValidationGroup="Submit" TabIndex="8" MaxLength="256" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Submit" ControlToValidate="txtDesc" Display="None"
                                    ErrorMessage="Please Enter Event Description"></asp:RequiredFieldValidator>
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" InvalidChars="`~!@#$%^&*()_-++{[}}:;'|\,>?/"
                                    FilterMode="InvalidChars" TargetControlID="txtDesc">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="col-lg-2 col-md-6 col-12 form-group mt-3" >
                                <%--<span style="color: red">* </span>--%>
                                <label>Is Paid</label>
                                <asp:CheckBox ID="chkPaid" runat="server" TabIndex="9" OnCheckedChanged="chkPaid_CheckedChanged" AutoPostBack="true" ValidationGroup="Submit" onchange="" />

                            </div>

                            <div class="col-lg-7" id="divPaid" runat="server" visible="true">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12 form-group">
                                        <span style="color: red">* </span>
                                        <label>Participant Type</label>
                                        <asp:DropDownList ID="ddlParticipant" runat="server" CssClass="fomr-control" TabIndex="10" AppendDataBoundItems="true" ValidationGroup="Add" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvParticipant" runat="server" ValidationGroup="Add" ControlToValidate="ddlParticipant" Display="None" InitialValue="0"
                                            ErrorMessage="Please Select Participant Type"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-lg-5 col-md-6 col-12 form-group">
                                        <span style="color: red">* </span>
                                        <label>Registration Fees</label>
                                        <asp:TextBox ID="txtRegFee" runat="server" CssClass="fomr-control" TabIndex="11" AppendDataBoundItems="true" MaxLength="6">                                           
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Add" ControlToValidate="txtRegFee" Display="None"
                                            ErrorMessage="Please Enter Registration Fees"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" ValidChars="0123456789"
                                            FilterMode="ValidChars" TargetControlID="txtRegFee">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="col-lg-2 col-md-6 col-12 form-group mt-3">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click" TabIndex="12" ValidationGroup="Add" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-6 col-12 form-group">

                                <div class="label-dynamic">
                                    Event Leaflet/Brochure &nbsp <span style="color: red; font-size: 10px; font: bold;">(Select only .doc,.docx or .pdf file)                 
                                    </span>
                                </div>
                                <div id="Div1" class="logoContainer" runat="server">
                                    <img src="../../IMAGES/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" tabindex="6" />

                                </div>
                                <div class="fileContainer sprite pl-1">
                                    <span runat="server" id="ufFile"
                                        cssclass="form-control" tabindex="7">Upload File</span>
                                    <asp:FileUpload ID="fuEvent" runat="server" ToolTip="Select File To Upload"
                                        CssClass="form-control" onkeypress="" />
                                    <asp:Label ID="lblBrochure" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlRegFee" runat="server" Visible="true">
                            <asp:ListView ID="lvRegistrationFee" runat="server" Visible="true">
                                <LayoutTemplate>

                                    <table class="table table-striped table-bordered nowrap display">
                                        <thead>
                                            <tr class="bg-light-blue" >
                                                <th>Edit
                                                </th>
                                                <th>Participant
                                                </th>
                                                <th>Registration Fees
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
                                            <asp:ImageButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CommandArgument='<%# Eval("PARTICIPANT_ID")%>'
                                                ImageUrl="~/Images/edit.png" />
                                        </td>
                                        <td id="participate" runat="server">
                                            <asp:Label runat="server" ID="lblParticipant" Text='<%#Eval("PARTICIPANT_TYPE") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblContactPersonName" Text='<%#Eval("REG_FEE") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" ValidationGroup="Submit" TabIndex="14" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClear_Click" TabIndex="15" />
                        <asp:ValidationSummary ID="vsSubmit" runat="server" ValidationGroup="Submit" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                        <asp:ValidationSummary ID="vsAdd" runat="server" ValidationGroup="Add" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlEvent" runat="server" Visible="true">
                            <asp:ListView ID="lvEventDetails" runat="server" Visible="true">
                                <LayoutTemplate>
                                    <div>
                                        <h3>
                                            <div class="label label-default">Event Details</div>
                                        </h3>
                                        <table class="table table-striped table-bordered nowrap display" id="tbllist">
                                            <thead>
                                                <tr class="bg-light-blue" >
                                                    <th>Edit
                                                    </th>
                                                    <th>Event Title
                                                    </th>
                                                    <th>Event Type
                                                    </th>
                                                    <th>Event Start Date
                                                    </th>
                                                    <th>Event End Date
                                                    </th>
                                                    <th>Event Registration Start Date
                                                    </th>
                                                    <th>Event Registration End Date
                                                    </th>
                                                    <th style="width: 10%">Download Event Leaflet/Brochure
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
                                            <asp:ImageButton ID="btnEditEvent" runat="server" OnClick="btnEditEvent_Click" CommandArgument='<%# Eval("EVENT_TITLE_ID") %>'
                                                ImageUrl="~/Images/edit.png" />
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblEventTitle" runat="server" Text='<%# Eval("EVENT_TITLE") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEventType" runat="server" Text='<%# Eval("EVENT_TYPE") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEventStart" runat="server" Text='<%# Eval("EVENT_START_DATE") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEventEnd" runat="server" Text='<%# Eval("EVENT_END_DATE") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEventRegStart" runat="server" Text='<%# Eval("EVENT_REG_START_DATE") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEventRegEnd" runat="server" Text='<%# Eval("EVENT_REG_END_DATE") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center; width: 10%">
                                            <asp:ImageButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" Visible='<%# Convert.ToString(Eval("EVENT_BROCHURE"))== string.Empty ? false:true %>' ImageUrl="~/IMAGES/down-arrow.png" CommandArgument='<%# Eval("EVENT_BROCHURE") %>' />
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

    <script>
        function EndDateVlidation() {
            var StartDate = document.getElementById('<%=txtEventStart.ClientID%>').value.split("/");
            var EndDate = document.getElementById('<%=txtEventEnd.ClientID%>').value.split("/");
            var StartDate1 = new Date(StartDate[2], StartDate[1] - 1, StartDate[0]);
            var EndDate1 = new Date(EndDate[2], EndDate[1] - 1, EndDate[0]);
            //var PrasentDate = new Date();
            if (EndDate1 < StartDate1) {
                alert("Event End Date Should be greater than or equal to Event Start Date.");
                document.getElementById('<%=txtEventEnd.ClientID%>').value = '';
                document.getElementById('<%=txtEventEnd.ClientID%>').focus();
                return false;
            }
        }

        function RegistarStartDateValidation() {
            var StartDate = document.getElementById('<%=txtEventStart.ClientID%>').value.split("/");
                var RegisterStartDate = document.getElementById('<%=txtRegStart.ClientID%>').value.split("/");
                var StartDate1 = new Date(StartDate[2], StartDate[1] - 1, StartDate[0]);
                var RegisterStartDate1 = new Date(RegisterStartDate[2], RegisterStartDate[1] - 1, RegisterStartDate[0]);
                if (StartDate1 == RegisterStartDate1) {
                    return true;
                }
                else if (StartDate1 < RegisterStartDate1) {
                    alert('Registration Start Date Should be Smaller than or equal to Event Start Date');
                    document.getElementById('<%=txtRegStart.ClientID%>').value = '';
                    document.getElementById('<%=txtRegStart.ClientID%>').focus();
                   return false;
               }

       }

       function RegistarEndDateValidation() {
           var RegStartDate = document.getElementById('<%=txtRegStart.ClientID%>').value.split("/");
                var EndDate = document.getElementById('<%=txtEventEnd.ClientID%>').value.split("/");
                var RegStartDate1 = new Date(RegStartDate[2], RegStartDate[1] - 1, RegStartDate[0]);
                var RegisterEndDate = document.getElementById('<%=txtRegEnd.ClientID%>').value.split("/");
                var EndDate1 = new Date(EndDate[2], EndDate[1] - 1, EndDate[0]);
                var RegisterEndDate1 = new Date(RegisterEndDate[2], RegisterEndDate[1] - 1, RegisterEndDate[0]);

                if (EndDate1 == RegisterEndDate1) {
                    return true;
                }
                else if (EndDate1 < RegisterEndDate1) {
                    alert('Registration End Date Should be Smaller than or equal to Event End Date');
                    document.getElementById('<%=txtRegEnd.ClientID%>').value = '';
                    document.getElementById('<%=txtRegEnd.ClientID%>').focus();
                return false;
            }
            else if (RegStartDate1 > RegisterEndDate1) {
                alert('Registration End Date Should be greater than or equal to Event Registration Start Date');
                document.getElementById('<%=txtRegEnd.ClientID%>').value = '';
                    document.getElementById('<%=txtRegEnd.ClientID%>').focus();
                return false;
            }

}

    </script>

    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_fuEvent").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_fuEvent").click();
                }
            });
        });
    </script>
    <script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>
</asp:Content>




