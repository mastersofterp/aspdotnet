<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PHD_DocumentMapping.aspx.cs" Inherits="ACADEMIC_PHD_DocumentMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updDocument" runat="server" AssociatedUpdatePanelID="updDocumentMapping"
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
           <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStart" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="updDocumentMapping" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                                            <label>Document Name:</label>
                                        </div>
                                        <asp:TextBox ID="txtDocumentName" runat="server" TabIndex="1" ToolTip="Please Enter Document Name" MaxLength="256" Width="250px" CssClass="form-control"  AutoComplete="OFF" ValidationGroup="submit"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReceiptName" runat="server" ControlToValidate="txtDocumentName"
                                            Display="None" ErrorMessage="Please Enter Document Name" ValidationGroup="submit" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                            FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|.:-&&quot;0123456789'" TargetControlID="txtDocumentName" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Active Status:</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" tabindex="2" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Mandatory:</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActiveMandatory" name="switch" checked />
                                            <label data-on="Yes" tabindex="3" class="newAddNew Tab" data-off="No" for="rdActiveMandatory"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit"
                                CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="4" OnClick="btnSave_Click" OnClientClick="return validate()"/>

                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" OnClick="btnCancel_Click"
                                CssClass="btn btn-warning" TabIndex="5" />
                        </div>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                                    <div class="col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Document Mapping List</h5>
                                                </div>
                                                <div class="table-responsive">
                                                    <asp:Panel ID="PanelDocument" runat="server">
                                                        <asp:ListView ID="lvDocumentMapping" runat="server" ItemPlaceholderID="itemPlaceholder">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th> Document Name
                                                                            </th>
                                                                            <th>Status
                                                                            </th>
                                                                            <th>Mandatory</th>
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
                                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" 
                                                                                    AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("DOCUMENTNO")%>' TabIndex="6" OnClick="btnEdit_Click"/>
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("DOCUMENTNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblStatus" runat="server"  Text='<%# Eval("ACTIVE_STATUS") %>'  ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>

                                                                            </td>
                                                                             <td>
                                                                                <asp:Label ID="lblMandatory" runat="server"  Text='<%# Eval("MANDATORY") %>'  ForeColor='<%# Eval("MANDATORY").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>

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
        </ContentTemplate>
    </asp:UpdatePanel>
     <script>
         function SetStatActive(val) {
             $('#rdActive').prop('checked', val);
         }
         function SetStatStart(val) {
             //alert("in");
             $('[id*=rdActiveMandatory]').prop('checked', val);
         }
         function validate() {

             $('#hfdActive').val($('#rdActive').prop('checked'));
             $('#hfdStart').val($('#rdActiveMandatory').prop('checked'));

             var idtxtweb = $("[id$=txtDocumentName]").attr("id");
             var txtweb = document.getElementById(idtxtweb);
             if (txtweb.value.length == 0) {
                 alert('Please enter document name.', 'Warning!');
                 //$(txtweb).css('border-color', 'red');
                 $(txtweb).focus();
                 return false;
             }

         }
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(function () {
             $(function () {
                 $('#btnSave').click(function () {
                     validate();
                 });
             });
         });
    </script>
</asp:Content>

