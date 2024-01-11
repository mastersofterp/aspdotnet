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

</asp:Content>

  

