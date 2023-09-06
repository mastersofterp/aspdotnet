<%@ Page Language="C#" MasterPageFile="~/ServiceBookMaster.master"  AutoEventWireup="true" CodeFile="Pay_SB_Avishkar.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_SB_Avishkar" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <asp:UpdatePanel ID="updAviPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                 

                    <div class="col-md-12">
                        
                                          <div class="col-12">
	                                    <div class="row">
		                                    <div class="col-12">
		                                    <div class="sub-heading">
				                                    <h5>Avishkar</h5>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>

                        <asp:Panel ID="pnlavishkar" runat="server">
                           
                            <div class="panel panel-info">
                              
                                <div class="panel panel-body">
                                      <div class="col-12">
                                            <div class="row">
                                                <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                          <label>Employee Name :</label>
                                                     </div>
                                                    <asp:DropDownList ID="ddlemp" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                     ToolTip="Select Employee Name" TabIndex="1"></asp:DropDownList>
                                                 </div>--%>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label> <span style="color: #FF0000">*</span> Level :</label>
                                                     </div>
                                        <asp:DropDownList ID="ddlLevel" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" ToolTip="Select Award Level" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">State</asp:ListItem>
                                            <asp:ListItem Value="2">District</asp:ListItem>
                                            <asp:ListItem Value="3">University</asp:ListItem>
                                        </asp:DropDownList> 
                                                
                                    
                                       <asp:RequiredFieldValidator ID="rfvlevel" runat="server" ControlToValidate="ddlLevel"
                                        Display="None" ErrorMessage="Please Select Level" ValidationGroup="ServiceBook"
                                        InitialValue="0"></asp:RequiredFieldValidator>                                                  
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                          <label> <span style="color: #FF0000">*</span> College(University) :</label>
                                                     </div>
                                        <asp:DropDownList ID="ddlUniversity" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                             ToolTip="Select University Name" TabIndex="2"></asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfvuniversity" runat="server" ControlToValidate="ddlUniversity"
                                        Display="None" ErrorMessage="Please Select College(University)" ValidationGroup="ServiceBook"
                                        InitialValue="0"></asp:RequiredFieldValidator> 
                                    </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                       <label> <span style="color: #FF0000">*</span> Title of Poster/Model Paper</label>
                                                        </div>
                                                    <asp:TextBox ID="txtTitle" runat="server"  CssClass="form-control"  ToolTip="Enter Title of Poster/Model Paper" TabIndex="3"></asp:TextBox>
                                                   
                                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" Display="None"
                                                     ErrorMessage="Please Enter  Title of Poster/Model Paper" SetFocusOnError="true" ValidationGroup="ServiceBook">
                                                                        </asp:RequiredFieldValidator>
                      

                                                     </div>

                                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Venue :</label>
                                                        </div>
                                                     <asp:TextBox ID="txtVenue" runat="server"  CssClass="form-control"  ToolTip="Enter Title of Poster/Model Paper" TabIndex="4"></asp:TextBox>
                                                </div>

                                                

                                  <div class="form-group col-lg-3 col-md-6 col-12">
                                       <div class="label-dynamic">
                                          <label>Date Received : </label>
                                         </div>

                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgcal" runat="server"  class="fa fa-calendar text-blue"/>
                                            </div>
                                            <asp:TextBox ID="txtdatereceived" runat="server" CssClass="form-control" ToolTip="Enter Date Received"
                                                TabIndex="5" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdatereceived"
                                                PopupButtonID="imgcal" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>                                          
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtdatereceived"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtdatereceived" EmptyValueMessage="Please Enter Date of talk"
                                                InvalidValueMessage="Date of Received is Invalid (Enter dd/mm/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Date of talk" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                             
                                        </div>
                                    </div>

                                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label><span style="color: #FF0000">*</span>Award</label>
                                                        </div>
                                                     <asp:DropDownList ID="ddlAward" runat="server" AppendDataBoundItems="true"
                                                          CssClass="form-control" ToolTip="Select Award Level" TabIndex="6">
                                                         <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                         <asp:ListItem Value="1">1st Award</asp:ListItem>
                                                         <asp:ListItem Value="2">2nd Award</asp:ListItem>
                                                         <asp:ListItem Value="3">3rd/Mentor</asp:ListItem>
                                                         <asp:ListItem Value="4">Participation</asp:ListItem>
                                                     </asp:DropDownList>

                                                     
                                      <asp:RequiredFieldValidator ID="rfvaward" runat="server" ControlToValidate="ddlAward"
                                        Display="None" ErrorMessage="Please Select Award" ValidationGroup="ServiceBook"
                                        InitialValue="0"></asp:RequiredFieldValidator> 

                                                    </div>
                                                </div>
                                          </div>
                                    
                        </asp:Panel>
                    </div>
                

                   

                    <div class="form-group col-md-12">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="7"
                                 CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click"  />&nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="8"
                                  CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click"  />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </p>
                    </div>

                  <div class="col-md-12">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvInfo" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Avishkar Details"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Award Details
                                        </h4>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                     
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Title of Paper
                                                    </th>
                                                    <th>Venue
                                                    </th>
                                                    <th>Date Received
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("AVNO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("AVNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("PAPERTITLE")%>
                                        </td>
                                        <td>
                                            <%# Eval("VENUE")%>
                                        </td>
                                        <td>
                                            <%# Eval("DOR", "{0:dd/MM/yyyy}")%>
                                        </td>
                                       
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
           

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

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

        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
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

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }

        function numericdotOnly(eventRef, elementRef) {
            var keyCodeEntered = (eventRef) ? eventRef.keyCode : (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

            if (keyCodeEntered == 46) {
                // Allow only 1 decimal point ('.')...
                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                    return false;
                else
                    return true;
            }
        }
    </script>
</asp:Content>

