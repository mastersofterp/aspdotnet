<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OBE_Configue.aspx.cs" Inherits="OBE_OBE_Configue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updConfig"
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

      <asp:UpdatePanel ID="updConfig" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
                 <div class="row">
                      <div class="col-md-12 col-sm-12 col-12" id="divBody">
                      <div class="box-header with-border">
                            <h3 class="box-title">OBE Configuration</h3>
                      </div>
                          </div>
                          <div class="col-12 btn-footer" id="dvSubmitData" runat="server" visible="True">
                               <asp:HiddenField ID="txtconformmessageValue" runat="server" />
                              <asp:Button ID="btnExam" runat="server" class="btn btn-primary" Text="Exam Config" TabIndex="1"  Visible="true" OnClientClick="javascript:ConfirmExamMessage();" OnClick="btnExam_Click" />
                               <asp:Button ID="btnUser" runat="server" class="btn btn-primary" Text="User Config"  TabIndex="2"  Visible="true" OnClientClick="javascript:ConfirmUserMessage();" OnClick="btnUser_Click" />
                                <asp:Button ID="btnCourse" runat="server" class="btn btn-primary" Text="Course Config" TabIndex="3" Enabled="true" OnClientClick="javascript:ConfirmCourseMessage();" OnClick="btnCourse_Click"  />
                                       
                
                          </div>

                 </div>
           </ContentTemplate>
      </asp:UpdatePanel>


      <div id="popup" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="modal" id="myModalPopUp" data-backdrop="static">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2">
                                <div class="col-12 mt-3">
                                    <h5 class="heading">Please enter password to access this page.</h5>
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <%--  <label>PASSWORD</label>--%>
                                            <asp:Label ID="lblPass" runat="server" Text="ybc@123" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPass" TextMode="Password" runat="server" TabIndex="1" ToolTip="Please Enter Password" AutoComplete="new-password"
                                                MaxLength="50" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txtPass"
                                                Display="None" ValidationGroup="password"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                        </div>
                                        <div class="btn form-group col-lg-12 col-md-12 col-12">
                                            <asp:Button ID="btnConnect" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="1" CssClass="btn btn-outline-primary"
                                                runat="server" Text="Submit" ValidationGroup="password" OnClick="btnConnect_Click" />
                                            <asp:Button ID="btnCancel1" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="2" CssClass="btn btn-danger"
                                                runat="server" Text="Cancel" OnClick="btnCancel1_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="password" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConnect" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <script>
        function ConfirmCourseMessage() {
            var selectedvalue = confirm("Do you want to Migrates the Course Records?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
          } else {
              document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
          }
        }


        function ConfirmExamMessage() {
            var selectedvalue = confirm("Do you want to Migrates the Exam tables Records?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
            }
        }

        function ConfirmUserMessage() {
            var selectedvalue = confirm("Do you want to Migrates the User Records?");
            if (selectedvalue) {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
            }
        }

    </script>
     <script type="text/javascript">
         $(window).on('load', function () {
             $('#myModalPopUp').modal('show');
         });
    </script>

</asp:Content>

  

