<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="QuestionPaper_Unlock.aspx.cs" Inherits="OBE_QuestionPaper_Unlock" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updEdit"
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

    <asp:UpdatePanel ID="updEdit" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
             <div class="row">
                 <div class="box box-primary">
                  <div class="box-header with-border">
                    <h3 class="box-title">Unlock Question Paper</h3>
                </div>
                  <div class="box-body">
                          <div runat="server" id="divMain">
                               <div class="col-12">
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divSession" runat="server">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Session Name</label>
                                </div>
                  <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RFVSESSION" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session Name" InitialValue="0" ValidationGroup="Submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator> 
                            </div>
                                    <div class="form-group col-lg-10 col-md-6 col-12" id="divScheme" runat="server">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Scheme Name</label>
                                </div>
                  <asp:DropDownList ID="ddlscheme" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVSCHEME" runat="server" ControlToValidate="ddlscheme"
                                            Display="None" ErrorMessage="Please Select Scheme Name" InitialValue="0" ValidationGroup="Submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator> 
                            </div>

                               </div>

                               <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Show" ValidationGroup="Submit" OnClick="btnShow_Click"  />
                                     <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Unlock" Visible="false" ValidationGroup="Submit"  onClick="btnUpdate_Click" OnClientClick="javascript:ConfirmMessage();" />
                                   <asp:HiddenField ID="txtconformmessageValue" runat="server" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel"  OnClick="btnCancel_Click"/>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                </div>
                              
                          </div>

                         <div class="col-12">
                              <asp:Panel ID="pnlPaper" runat="server" Visible="false">
                                     <asp:ListView ID="lvStudent" runat="server">
                                          <LayoutTemplate>
                                               <div id="listViewGrid">
                                                    <div class="sub-heading">
                                                    <h5>Question Paper List</h5>
                                                </div>
                                                   </div>
                                               <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: left;">Select All
                                                           <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" ToolTip='<%# Eval("QuestionPaperId") %>' Checked="false" />
                                                            </th>
                                                            <th>Exam Name </th>
                                                            <th>SchemeMappingName</th>
                                                            <th>Course Code</th>
                                                            <th>Total Marks </th>
                                                            <th>Created By</th>
                                                            <th>Status </th>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                           </LayoutTemplate>
                                         <ItemTemplate>
                                          <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkStudent" runat="server" ToolTip='<%# Eval("QuestionPaperId") %>' Enabled='<%# Convert.ToString(Eval("Status"))=="In-Progress" ? false: true%>'/>
                                                     <asp:HiddenField ID="hfqp" runat="server" Value='<%# Eval("QuestionPaperId")%>' />
                                                </td>
                                                <td><%# Eval("ExamName")%></td>
                                                
                                                <td>
                                                 <%# Eval("SchemeMappingName")%>
                                                </td>
                                                 <td>
                                                 <%# Eval("CCODE")%>
                                                </td>
                                                <td><%# Eval("TotalMaxMarks")%></td>
                                                <td><%# Eval("CreatedBy")%></td>
                                                <td>
                                                    
                                                    <asp:Label runat="server" ID="lblFaculty" Text=<%# Eval("Status")%>
                                                            ForeColor='<%# Convert.ToString(Eval("Status"))=="Lock" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' ></asp:Label> 
                                                   
                                            </tr>
                                        </ItemTemplate>
                                         </ItemTemplate>
                                      </asp:ListView>
                               </asp:Panel>
                       </div>
                       </div>
                 </div>
             </div>
         </ContentTemplate>
    </asp:UpdatePanel>
     <script language="javascript" type="text/javascript">
         function totAll(headchk) {
             var frm = document.forms[0]
             for (i = 0; i < document.forms[0].elements.length; i++) {
                 var e = frm.elements[i];
                 if (e.type == 'checkbox') {
                     if (headchk.checked == true)
                         e.checked = true;
                     else
                         e.checked = false;
                 }
             }
         }
    </script>

      <script language="javascript" type="text/javascript">
          function totAll1(headchk) {
              var frm = document.forms[0]
              for (i = 0; i < document.forms[0].elements.length; i++) {
                  var e = frm.elements[i];
                  if (e.type == 'checkbox') {
                      if (headchk.checked == true)
                          e.checked = true;
                      else
                          e.checked = false;
                  }
              }
          }
    </script>
      <script>
          function ConfirmMessage() {
              var selectedvalue = confirm("This will also delete the Marks entry (if done)..!! Do you still want to Unlock This Question Paper?");
              if (selectedvalue) {
                  document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "Yes";
            } else {
                document.getElementById('<%=txtconformmessageValue.ClientID %>').value = "No";
            }
        }

    </script>
</asp:Content>

