<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Receipt_Code.aspx.cs" Inherits="ACADEMIC_ReceiptCode" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />

     <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <div> 
        <asp:UpdateProgress ID="updReceipt" runat="server" AssociatedUpdatePanelID="updReceiptCode"
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
     <asp:UpdatePanel ID="updReceiptCode" runat="server">
        <ContentTemplate>  
              <script>
                  function ValidationName() {
                      var correct_way = /^[A-Za-z]+$/;


                      var a = document.getElementById('<%=txtReceiptCode.ClientID%>').value;

            if (!a.match(correct_way)) {
                alert("Receipt Code Should be alphabetic");
                document.getElementById('<%=txtReceiptCode.ClientID%>').value = '';

                false;
            }
            else if (x == "") {
                alert("Please Enter Receipt Code");
                return false;
            }
            else {
                true;
            }
        }
    </script>        
     <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">

                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reciept Name:</label>
                                        </div>
                                      <asp:TextBox ID="txtReceiptName" runat="server" TabIndex="1" ToolTip="Please Enter Receipt Name" MaxLength="256" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReceiptName" runat="server" ControlToValidate="txtReceiptName"
                                            Display="None" ErrorMessage="Please Enter Receipt Name" ValidationGroup="submit" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                      FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|.:-&&quot;0123456789'" TargetControlID="txtReceiptName" />
                                    </div>
                                   
                                                  <div class="form-group col-lg-4 col-md-6 col-12">

                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reciept Code:</label>
                                        </div>
                                      <asp:TextBox ID="txtReceiptCode" runat="server" TabIndex="2" ToolTip="Please Enter Receipt Code" onkeyup="return ValidationName();" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRecept" runat="server" ControlToValidate="txtReceiptCode" Display="None" ErrorMessage="Please Enter Receipt Code" ValidationGroup="submit" />                                                                            </div>
                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Active Status:</label>
                                        </div>
                                       <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch"  checked />
                                            <label data-on="Active" tabindex="3" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                          <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit"
                                CssClass="btn btn-primary"  ValidationGroup="submit" TabIndex="4" OnClientClick="return validation();" OnClick="btnSave_Click" />

                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                CssClass="btn btn-warning" TabIndex="5" OnClick="btnCancel_Click"/>
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                         <div class="col-12 mt-3">
                       
                        <div class="table-responsive">
                            <asp:Panel ID="PanelReceiptCode" runat="server" Visible="false">
                                 <div class="sub-heading">
                            <h5>Receipt Code List</h5>
                        </div>
                                <asp:ListView ID="lvReceiptCode" runat="server" ItemPlaceholderID="itemPlaceholder">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit</th>

                                                    <th>Receipt Name</th>
                                                    <th>Receipt Code</th>
                                                    <th>Active Status</th>                                                  
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>


                                        <asp:UpdatePanel runat="server" ID="updEventCategory">
                                            <ContentTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                            AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("RCNO")%>' TabIndex="6" OnClick="btnEdit_Click"/>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RC_NAME")%></td>
                                                    <td>
                                                        <%# Eval("RECIEPT_CODE")%></td>
                                                    <td>
                                                           <asp:Label ID="lblStatus" runat="server" CssClass="badge" ToolTip='<%# Eval("ACTIVESTATUS") %>'></asp:Label>
                                                        
                                                    </td>
                                                    
                                                </tr>
                                            </ContentTemplate>

                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>

                        </div>
                    </div>
                        </div>
                    </div>
         </div>
            </ContentTemplate>

         </asp:UpdatePanel>
     <script>
         function SetSetSubjecttype(val) {

             $('#rdActive').prop('checked', val);
         }
         function validation() {
             var alertMsg = "";
             var facuilty = document.getElementById('<%=txtReceiptName.ClientID%>').value;
             var x = document.getElementById('<%=txtReceiptCode.ClientID%>').value;
                if (facuilty == 0) {
                    if (facuilty == "") {
                        alertMsg = alertMsg + 'Please Enter Receipt Name\n';
                    }
                    alert(alertMsg);
                    return false;
                } else if (x == "") {
                    alert("Please Enter Receipt Code");
                   document.getElementById('<%=txtReceiptName.ClientID%>').value = '';
                    return false;
                }
                else {

                    $('#hfdActive').val($('#rdActive').prop('checked'));
                }
            }
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(function () {
                    $('#btnSave').click(function () {
                        validation();
                    });
                });
            });

     </script>
</asp:Content>

