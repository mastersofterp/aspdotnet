<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MM_Lock.aspx.cs"
    Inherits="MEETING_MANAGEMENT_Transaction_MM_Lock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script type="text/javascript" language="javascript">
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
        function validate() {
            var committee = document.getElementById('ctl00_ContentPlaceHolder1_ddlCommitee');
            var committee_code = document.getElementById('ctl00_ContentPlaceHolder1_ddlcode');
            var Chk_Agenda = document.getElementById('ctl00_ContentPlaceHolder1_chkagenda');
            var Chk_Meeting = document.getElementById('ctl00_ContentPlaceHolder1_chkmeeting');
            var Chk_Email = document.getElementById('ctl00_ContentPlaceHolder1_chksendEmail');
            if (Chk_Agenda.checked || Chk_Meeting.checked || Chk_Email.checked) {

                if (committee.options[committee.selectedIndex].value == 0 && committee_code.options[committee_code.selectedIndex].value == 0) {
                    alert('Please Select Committee and  Committee Code ');

                }
                else if (committee.options[committee.selectedIndex].value == 0) {
                    alert('Please Select Committee');

                }
                else if (committee_code.options[committee_code.selectedIndex].value == 0) {
                    alert('Please Select Committee Code');

                }

            }

            else {

            }

        }



    </script>--%>

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
                            <h3 class="box-title">LOCK AGENDA/MEETING</h3>
                        </div>

                        <div class="box-body">
                              <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trCollegeName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Institute</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Institute" AppendDataBoundItems="true" TabIndex="1"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="Submit" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Committee Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCommitee" runat="server" AppendDataBoundItems="true"
                                                AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Committee" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvcommitee" runat="server" ErrorMessage="Please Select Committee"
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlCommitee" Display="None"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Meeting Code</label>
                                            </div>
                                            <asp:DropDownList ID="ddlcode" runat="server" AppendDataBoundItems="true"
                                                AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select Meeting Code" OnSelectedIndexChanged="ddlcode_SelectedIndexChanged" TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvmeetingcode" runat="server" ErrorMessage="Please Select Meeting Code"
                                                ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlcode" Display="None"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:CheckBox runat="server" ID="chkagenda" Text="Lock" AutoPostBack="false" OnCheckedChanged="chkagenda_CheckedChanged" TabIndex="3" onclick="validate()" />
                                            <asp:Label runat="server" Text="Agenda" ID="lblagenda"></asp:Label>

                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:CheckBox runat="server" ID="chkmeeting" Text="Lock" AutoPostBack="false" OnCheckedChanged="chkmeeting_CheckedChanged" TabIndex="4" onclick="validate()" />
                                            <asp:Label runat="server" Text="Meeting" ID="lblmeeting"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12"  id="divUpload" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Upload Files</label>
                                            </div>
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divFile"  runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                              <asp:Button ID="Button1" runat="server" Text="Attach File" OnClick="btnAttachFile_Click" />&nbsp;&nbsp;(Max.Size
                                                <asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)
                                  
                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-12" id="divSendSMS" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                             <asp:CheckBox ID="chksendSms" runat="server" onclick="validate()" TabIndex="5" Text="Send SMS" />
                                 
                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-12"  id="divSendEmail" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                             <asp:CheckBox runat="server" ID="chksendEmail" Text="Send Email" TabIndex="5" onclick="validate()" />
                                  
                                        </div>
                                    </div>
                     
                               </asp:Panel>
                           </div>
                              <div class="col-12 btn-footer">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" TabIndex="5" CausesValidation="true" />
                    <asp:Button ID="btnSendMail" runat="server" Text="Send Agenda Mail" CssClass="btn btn-primary" ToolTip="Send Reminder Mail" ValidationGroup="Submit" OnClick="btnSendMail_Click" TabIndex="7" CausesValidation="true" Visible="true" />
                                   <asp:Button ID="btnSendMeetingMail" runat="server" Text="Send Meeting Mail" CssClass="btn btn-primary" ToolTip="Send Reminder Mail"  OnClick="btnSendMeetingMail_Click" TabIndex="7" CausesValidation="false" />
                    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnReport_Click" TabIndex="8" CausesValidation="true" ValidationGroup="Submit" Visible="false" />
                    <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" CssClass="btn btn-primary" ToolTip="Send Reminder SMS" OnClick="btnSendSMS_Click" TabIndex="9" CausesValidation="false" Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="true" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" TabIndex="6" CausesValidation="false" />
                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" HeaderText="Following Field(s) are mandatory" />
                   </div>
                              <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvfile" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading"><h5>Download files </h5></div>
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
                                            <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete Record" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>' ImageUrl="~/images/delete.png" OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" ToolTip="Delete Record" />
                                        </td>
                                        <td><%#GetFileName(DataBinder.Eval(Container, "DataItem")) %></td>
                                        <td>
                                            <asp:ImageButton ID="imgFile" runat="Server" AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ImageUrl="~/images/action_down.png" OnClick="imgdownload_Click" ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>' />
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

          <%--  </div> --%>
        </ContentTemplate>
        <%--  <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit"  />
          <%--  <asp:PostBackTrigger ControlID=" btnCancel" />--%>
        <%--<asp:PostBackTrigger ControlID=" btnSendMail" />
        </Triggers>--%>
               <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">

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
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46) {
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
</asp:Content>
