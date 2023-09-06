<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Reciept_Code_Creation.aspx.cs" Inherits="ACADEMIC_Reciept_Code_Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <style>
        #ctl00_ContentPlaceHolder1_pnlSession .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStart" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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
     <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
                 <div class="row">
                   <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                             <h3 class="box-title">RECEIPT CODE MASTER MANAGEMENT</h3>
                            </div>
                          <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Receipt Name :</label>
                                        </div>
                                        <asp:TextBox ID="txtReceiptName" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="2"
                                            ToolTip="Please Enter Session Long Name" placeholder="Enter Receipt Name" />
                                        <%--<asp:RequiredFieldValidator ID="rfvReceiptName" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Receipt Name" ControlToValidate="txtReceiptName"
                                            Display="None" ValidationGroup="Submit" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Receipt Code :</label>
                                        </div>
                                        <asp:TextBox ID="txtRecieptCode" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="2"
                                            ToolTip="Please Enter Session Long Name" placeholder="Enter Receipt Code" />
                                        <%--<asp:RequiredFieldValidator ID="rfvRecieptCode" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Receipt Code" ControlToValidate="txtRecieptCode"
                                            Display="None" ValidationGroup="Submit" />--%>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                         <asp:HiddenField ID="hdnDate" runat="server" />
                                    </div>
                                </div>
                                  <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return validate();" 
                                        TabIndex="11" ValidationGroup="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        TabIndex="13" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="valReceiptCode" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                </div>
                                 <div class="col-12">
                                    <asp:Panel ID="pnlSession" runat="server">
                                        <asp:ListView ID="lvRecieptCode" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>ID
                                                            </th>
                                                            <th> Receipt Name
                                                            </th>
                                                            <th>Receipt Code
                                                            </th>
                                                            <th>Is Active
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
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RC_NAME")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("RECIEPT_CODE") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
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
                 </div>
          </ContentTemplate>
      </asp:UpdatePanel>
    
    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatStart(val) {
            $('#rdStart').prop('checked', val);
        }
        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdStart').val($('#rdStart').prop('checked'));

            var idtxtweb = $("[id$=txtReceiptName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Receipt Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }
            var idtxtweb = $("[id$=txtRecieptCode]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Reciept Code', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>

</asp:Content>

