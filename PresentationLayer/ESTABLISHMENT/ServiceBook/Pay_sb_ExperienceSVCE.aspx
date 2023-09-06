<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pay_sb_ExperienceSVCE.aspx.cs" MasterPageFile="~/ServiceBookMaster.master"
    Inherits="ESTABLISHMENT_ServiceBook_Pay_sb_ExperienceSVCE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                         <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Institute Experiences</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="panel panel-info">
                                           <%-- <div class="panel panel-heading">Experiences</div>--%>
                                            <div class="panel panel-body">
                                               <%-- Modified by Saahil Trivedi 24/01/2022--%>
                                                <div class="col-12">
                                                    <div class="row">
                                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    <label><span style="color: red;">*</span>Department :</label>
                                                        </div>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server"
                                                        CssClass="form-control"
                                                        TabIndex="1" data-select2-enable="true"
                                                        AppendDataBoundItems="true" ToolTip="Select Department">
                                                        <%--<asp:ListItem Value="0">--Please Select--</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDepartment" runat="server"
                                                        ControlToValidate="ddlDepartment"
                                                        Display="None"
                                                        ErrorMessage="Please Select Department"
                                                        ValidationGroup="ServiceBook"
                                                        InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    <label><span style="color: red;">*</span>Designation :</label>
                                                        </div>
                                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" TabIndex="2"
                                                        AppendDataBoundItems="true" ToolTip="Select Designation" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDesignation" runat="server"
                                                        ControlToValidate="ddlDesignation"
                                                        Display="None"
                                                        ErrorMessage="Please Select Designation"
                                                        ValidationGroup="ServiceBook"
                                                        InitialValue="0"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    <label>Nature of Appointment :</label>
                                                        </div>
                                                    <asp:DropDownList ID="ddlNatOfAppointment" runat="server" CssClass="form-control" TabIndex="3"
                                                         ToolTip="Select Nature Of  Appointment" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Permanent</asp:ListItem>
                                                        <asp:ListItem Value="2">On-Contract</asp:ListItem>
                                                        <asp:ListItem Value="3">Adhoc</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox ID="txtNatOfApp" runat="server" CssClass="form-control" ToolTip="Enter nature of Appointment" TabIndex="9"></asp:TextBox>--%>
                                                </div>
                                               
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    <label>Is Current :</label>
                                                        </div>
                                                    <asp:CheckBox ID="chkIsCurrent" CssClass="form-control" Text=" Yes" runat="server" TabIndex="4"
                                                        ToolTip="Check for Is Current" OnCheckedChanged="chkIsCurrent_CheckedChanged" AutoPostBack="true"/>
                                                    <%-- --%>
                                                    <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ToolTip="Enter Duration" TabIndex="6"></asp:TextBox>--%>
                                                </div>
                                                </div>
                                                  </div>
                                                 <div class="col-12">
                                                    <div class="row">
                                             <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    <label><span style="color: red;">*</span>Start Date :</label>
                                                        </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" class="fa fa-calendar text-blue" />
                                                        </div>
                                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ToolTip="Enter Start Date" TabIndex="5"
                                                            Style="z-index: 0;" onBlur="CalDuration();" onChange="CalDuration();"></asp:TextBox>
                                                            <%--OnTextChanged="txtStartDate_TextChanged" AutoPostBack="true"--%>
                                                        <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate"
                                                            PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>

                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtStartDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                            ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                                            InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                            Display="None" ErrorMessage="Please Select Start Date in (dd/MM/yyyy Format)"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    <label>End Date :</label> <%--<span style="color: red;">*</span>--%>
                                                        </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image2" runat="server" class="fa fa-calendar text-blue" />
                                                        </div>
                                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ToolTip="Enter End Date"
                                                            TabIndex="6" Style="z-index: 0;" onBlur="CalDuration();" onChange="CalDuration();" OnTextChanged="txtEndDate_TextChanged1"></asp:TextBox>
                                                        <%-- AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged"--%>
                                                        <ajaxToolKit:CalendarExtender ID="ceEndDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate"
                                                            PopupButtonID="Image2" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>

                                                        <ajaxToolKit:MaskedEditExtender ID="meEdate" runat="server" TargetControlID="txtEndDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevEDate" runat="server" ControlExtender="meEdate"
                                                            ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter End Date"
                                                            InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter End Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                                            Display="None" ErrorMessage="Please Select End Date in (dd/MM/yyyy Format)"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                   <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">

                                                    <label>Duration : </label>
                                                        </div>
                                                    <asp:TextBox ID="txtDuration" AutoCompleteType="None" runat="server" AutoComplete="off" CssClass="form-control" 
                                                        ToolTip="Enter Duration" MaxLength="50"
                                                        onkeydown="return EditControl(event,this);" onkeypress="return EditControl(event,this);" 
                                                        onclick="return EditControl(event,this);" TabIndex="7"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                    <label>Promotion/Dept-Transfer Order :</label>
                                                        </div>
                                                    <asp:FileUpload ID="flupld" runat="server" ToolTip="Enter Promotion/Dept-Transfer Order" TabIndex="8" />
                                                    <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg,.doc) upto 5MB" ForeColor="Red"></asp:Label>
                                                </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                                        </div>
                                                        </div>
                                                     </div>
                                                <div class="form-group col-md-12">
                                                    <p class="text-center">

                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="9"
                                                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook" TabIndex="10"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlExperiences" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvExperiences" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows  Experience Employee"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <h4 class="box-title">Experience Details
                                                    </h4>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">  

                                                   <%-- <table class="table table-bordered table-hover">--%>
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Action
                                                                </th>
                                                                <th>Department
                                                                </th>
                                                                <th>Designation
                                                                </th>
                                                                <th>Nature of App
                                                                </th>
                                                                <th>IsCurrent
                                                                </th>
                                                                <th>Duration
                                                                </th>
                                                                <th>Start Date
                                                                </th>
                                                                <th>End Date
                                                                </th>
                                                                <th id="divFolder" runat="server">Attachment
                                                                </th>
                                                                <th id="divBlob" runat="server">Attachment
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
                                                    <%-- Modified by Saahil Trivedi 24/01/2022--%>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SVCNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("SVCNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBDEPT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBSDESIG")%>       
                                                    </td>
                                                    <td>
                                                        <%# Eval("NatureofAppointment")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Iscurrent")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Duration")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("START_DATE", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EndDate", "{0:dd/MM/yyyy}")%>
                                                    </td>
                                                    <td id="tdFolder" runat="server">
                                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("SVCNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <div class="col-md-12">
        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
            <div class="text-center">
                <div class="modal-content">
                    <div class="modal-body">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
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

        //function Validate() {

        //var StartDate = document.getElementById('<%=txtStartDate.ClientID%>').value;
        // var EndDate = document.getElementById('<%=txtEndDate.ClientID%>').value;
        // var counter = 1;
        // var msg = '';

        // if (document.getElementById('<%=ddlDepartment.ClientID%>').selectedIndex == 0)
        //  { msg = 'Please select Department \n'; counter = counter + 1; }
        //  if (document.getElementById('<%=ddlDesignation.ClientID%>').selectedIndex == 0)
        //  { msg += 'Please select Designation \n'; counter = counter + 1; }
        //  if (StartDate == '')
        //{ msg += 'Please select Start Date \n'; counter = counter + 1; }
        //if (EndDate == '')
        // { msg += 'Please select End Date \n'; counter = counter + 1; }
        // if (StartDate > EndDate)
        // { msg += 'Start Date should not be greater than End Date \n'; counter = counter + 1; }

        // if (counter > 1)
        // { alert(msg); return false; } return true;

        //if (document.getElementById('<%=ddlDepartment.ClientID%>').selectedIndex == 0)
        //{ alert('Please select Department'); return false; }
        //else if (document.getElementById('<%=ddlDesignation.ClientID%>').selectedIndex == 0)
        //{ alert('Please select Designation'); return false; }
        //else if (document.getElementById('<%=txtStartDate.ClientID%>').value == '')
        //{ alert('Please select Start Date'); return false; }
        //else if (document.getElementById('<%=txtEndDate.ClientID%>').value == '')
        //{ alert('Please select End Date'); return false; }
        //    return true
        // }

    </script>

    <script type="text/javascript">
        function EditControl(event, obj) {
            obj.style.backgroundColor = "LightGray";

            document.getElementById('<%=txtDuration.ClientID%>').contentEditable.replace = false;
            document.getElementById('<%=txtDuration.ClientID%>').contentEditable = false;
        //alert("Not Editable");
        return false;
    }
    </script>
    <script type="text/javascript">

        function CalDuration() {
            debugger;
            var datejoing = document.getElementById('<%=txtStartDate.ClientID%>').value;
            var dateleaving = document.getElementById('<%=txtEndDate.ClientID%>').value;
            if (datejoing != '' && dateleaving != '') {

                var dateElements = datejoing.split("/");
                var outputDateString = dateElements[2] + "/" + dateElements[1] + "/" + dateElements[0];
                var dateElementsnew = dateleaving.split("/");
                var outputDateStringnew = dateElementsnew[2] + "/" + dateElementsnew[1] + "/" + dateElementsnew[0];

                var date1 = new Date(outputDateString);
                var date2 = new Date(outputDateStringnew);

                if (Object.prototype.toString.call(date2) === "[object Date]") {                 
                    if (isNaN(date2.getTime())) {  
                        document.getElementById('<%=txtDuration.ClientID%>').value = '';
                    } else {
                        
                    }
                } else {                   
                }
                if (Object.prototype.toString.call(date1) === "[object Date]") {
                   
                    if (isNaN(date1.getTime())) {  
                        document.getElementById('<%=txtDuration.ClientID%>').value = '';
                          return;
                      } else {
                          
                      }
                  } else {
                      
                  }

                  if (date1 > date2) {
                      alert("To date should be greater than equal to from date.");
                      document.getElementById('<%=txtDuration.ClientID%>').value = '';
                    document.getElementById('<%=txtEndDate.ClientID%>').value = '';
                    document.getElementById('<%=chkIsCurrent.ClientID%>').checked = false;
                    return;
                }

                  else if (date1 > new Date() || date2 > new Date()) {
                    alert("Future date should not be accepted.");
                    document.getElementById('<%=txtDuration.ClientID%>').value = '';
                    document.getElementById('<%=txtEndDate.ClientID%>').value = '';
                    document.getElementById('<%=chkIsCurrent.ClientID%>').checked = false;
                    return;
                }
                  //else if ((date1 < new Date() || date2 < new Date()) && date1 >= date2) {
                    dateDiff(date1, date2);
               // }
            }
            else
                document.getElementById('<%=txtDuration.ClientID%>').value = '';
        }


        function dateDiff(startingDate, endingDate) {
            var startDate = new Date(new Date(startingDate).toISOString().substr(0, 10));
            if (!endingDate) {
                endingDate = new Date().toISOString().substr(0, 10);    // need date in YYYY-MM-DD format
            }
            var endDate = new Date(endingDate);
            if (startDate > endDate) {
                var swap = startDate;
                startDate = endDate;
                endDate = swap;
            }
            var startYear = startDate.getFullYear();
            var february = (startYear % 4 === 0 && startYear % 100 !== 0) || startYear % 400 === 0 ? 29 : 28;
            var daysInMonth = [31, february, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

            var yearDiff = endDate.getFullYear() - startYear;
            var monthDiff = endDate.getMonth() - startDate.getMonth();
            if (monthDiff < 0) {
                yearDiff--;
                monthDiff += 12;
            }
            var dayDiff = endDate.getDate() - startDate.getDate();
            if (dayDiff < 0) {
                if (monthDiff > 0) {
                    monthDiff--;
                } else {
                    yearDiff--;
                    monthDiff = 11;
                }
                dayDiff += daysInMonth[startDate.getMonth()];
            }
            document.getElementById('<%=txtDuration.ClientID%>').value = yearDiff + ' ' + 'Years' + ' ' + monthDiff + ' ' + 'Months' + ' ' + dayDiff + ' ' + 'Days';
            return yearDiff + 'Y ' + monthDiff + 'M ' + dayDiff + 'D';
        }

  


        

    </script>

</asp:Content>
