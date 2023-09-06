<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SubjectType.aspx.cs" Inherits="ACADEMIC_MASTERS_SubjectType" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />

     <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBatch"
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

    <asp:UpdatePanel ID="updBatch" runat="server">
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject Type</label>
                                        </div>
                                        <asp:TextBox ID="txtSubjectType" runat="server" MaxLength="256" AutoComplete="off" CssClass="form-control"/>
                                       
                                            <asp:RequiredFieldValidator ID="rfvtxtSubjectType" runat="server" ControlToValidate="txtSubjectType"
                                            Display="None" ErrorMessage="Please Enter Subject Type" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Is consider Theory/Practical</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCondition" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" 
                                            AppendDataBoundItems="True" ToolTip="Please Select Is Condition Theory/Practical" OnSelectedIndexChanged="ddlCondition_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Theory</asp:ListItem>
                                            <asp:ListItem Value="2">Practical</asp:ListItem>
                                            <asp:ListItem Value="3">Theory & Practical</asp:ListItem>
                                             </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCondition"
                                                Display="None" ErrorMessage="Please Select Is Condition Theory/Practical" InitialValue="0" ValidationGroup="Submit" />
                                       
                                    </div>                                 

                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Course Teacher Allotment</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAllotement" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" 
                                            AppendDataBoundItems="True" ToolTip="Please Select Course Teacher Allotment">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Section</asp:ListItem>
                                            <asp:ListItem Value="2">Batch</asp:ListItem>
                                        </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAllotement"
                                                Display="None" ErrorMessage="Please Select Course Teacher Allotment" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                  
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                       <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" tabindex="7" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                   
                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divchk" >
                                        <div class="label-dynamic">
                                           <%-- <sup>* </sup>--%>
                                            <label>Is Tutorial</label>
                                        </div>
                                            <asp:CheckBox ID="chkTutorial" runat="server" />
                                      
                                    </div>
                             
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit"  
                                    CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick="return validation();" TabIndex="3" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" onclick="btnCancel_Click" TabIndex="5" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" Style="text-align: center" />
                            </div>

                             <div class="col-12 mt-3">
                                                        <div class="sub-heading">
                                                            <h5>Subject Type List</h5>
                                                        </div>
                                                        <div class="table-responsive">
                                                            <asp:Panel ID="PanelSubjectType" runat="server" Visible="false">
                                                                <asp:ListView ID="lvSubjecttype" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvSubjecttype_ItemEditing">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                 <th>Edit</th>
                                                                                 <th>Subject Type
                                                                                 </th>
                                                                                 <th>Is consider Theory/Practical
                                                                                 </th>
                                                                                  <th>Section/Batch
                                                                                  </th>
                                                                                    <th>IsTutorial
                                                                                  </th>
                                                                                   <th>Status
                                                                                    </th>
                                                                                 </tr> </thead>
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
                                                            AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("SUBID")%>' TabIndex="6" OnClick="btnEdit_Click" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TH_PR")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEC_BATCH")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblistutorial" runat="server" CssClass="badge" Text='<%# Eval("ISTUTORIAL") %>' ForeColor='<%#Eval("ISTUTORIAL").ToString().Equals("NA")?System.Drawing.Color.Red : System.Drawing.Color.Green %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                           <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVESTATUS") %>'></asp:Label>
                                                        
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
                                                </ContentTemplate>
                                               
                               </asp:UpdatePanel>  
      <script>
          function SetSetSubjecttype(val)
          {
              $('#rdActive').prop('checked', val);
          }
          function validation()
          {
              var alertMsg = "";
              var subjectType = document.getElementById('<%=txtSubjectType.ClientID%>').value;
              var condition = document.getElementById('<%=ddlCondition.ClientID%>').value;
              var allotment = document.getElementById('<%=ddlAllotement.ClientID%>').value;
              if (subjectType == 0 || condition == 0 || allotment == 0)// || branch == 0)
              {
                  if (subjectType == 0) {
                      alertMsg = alertMsg + 'Please Enter Subject Type.\n';
                  }
                  if (condition == 0) {
                      alertMsg = alertMsg + 'Please Select Condition.\n';
                  }
                  if (allotment == 0) {
                      alertMsg = alertMsg + 'Please Select Course Teacher Allotment\n';
                  }  
                  alert(alertMsg);
                  return false;
              }
              else
              {
                 // alert('in');
                  $('#hfdActive').val($('#rdActive').prop('checked'));
              }
          }
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          prm.add_endRequest(function ()
          {
              $(function ()
              {
                  $('#btnSave').click(function ()
                  {
                      validation();
                  });
              });
          });
          function Validate()
          {

          }
       
     </script>
</asp:Content>

