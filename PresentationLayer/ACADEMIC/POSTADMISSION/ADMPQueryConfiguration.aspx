<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ADMPQueryConfiguration.aspx.cs" Inherits="ACADEMIC_POSTADMISSION_ADMPQueryConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
  <%--  <asp:UpdatePanel ID="updquerycategory" runat="server" UpdateMode="Conditional">
     <ContentTemplate>--%>
        <style>
        .switch.madtory label {
            width: 140px !important;
        }

        .switch.madtory input:checked + label:after {
            transform: translateX(128px);
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Query Configuration</h3>
                </div>

                <div class="box-body">
                     <div id="Tabs" role="tabpanel">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" >Query Category</a>
                            </li>
                            <%--<li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">User Allocation</a>
                            </li>--%>
                            <%--<li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3">Admin Panel </a>
                            </li>--%>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Query Category</label>
                                            </div>
                                            <asp:TextBox ID="txtQueryCategory" runat="server" CssClass="form-control" TabIndex="1">
                                            </asp:TextBox>
                                           <asp:RequiredFieldValidator ID="rfvtxtQueryCategory" runat="server" ControlToValidate="txtQueryCategory"
                                           Display="None" ErrorMessage="Please Enter Query Category " SetFocusOnError="True"
                                           ValidationGroup="QueryCategory">
                                           </asp:RequiredFieldValidator>
                                           </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Status</label>
                                            </div>
                                            <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                            
                                            </div>
                                           <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQueryCategory"
                                           Display="None" ErrorMessage="Please Enter Query Category " SetFocusOnError="True"
                                           ValidationGroup="QueryCategory">
                                           </asp:RequiredFieldValidator>--%>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" TabIndex="2" OnClick="btnSubmit_Click" ValidationGroup="QueryCategory"  OnClientClick="return validate();" />
                                    <asp:ValidationSummary ID="vsQueryCategogry" ValidationGroup="QueryCategory" runat="server" DisplayMode="List" ShowMessageBox="True"
                                     ShowSummary ="False" > </asp:ValidationSummary>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="3" OnClick="btnCancel_Click" />
                                </div>
                                
                            <div class="col-12">
                        <asp:Panel ID="pnlQueryCategory" runat="server" Visible="true">
                            <asp:ListView ID="lvQueryCategory" runat="server" Visible="true">
                                <LayoutTemplate>

                                    <table class="table table-striped table-bordered nowrap display">
                                        <thead>
                                            <tr class="bg-light-blue" >

                                                <th>Edit
                                                </th>
                                                <th>Query Category
                                                </th>
                                                <th>Status
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
                                            <asp:ImageButton ID="btnEdit" runat="server" OnClick="btnEdit_Click"  CommandArgument='<%# Eval("CATEGORYNO")%>'
                                                ImageUrl="~/Images/edit.png" />
                                        </td>
                                        <td id="participate" runat="server">
                                            <asp:Label runat="server" ID="lblQCategory" Text='<%#Eval("QUERY_CATEGORY_NAME") %>'></asp:Label>
                                        </td>
                                         <td id="Td1" runat="server">
              
                                            <asp:Label runat="server" ID="Label1" Text='<%#  Convert.ToInt32(Eval("ACTIVE_STATUS")) == 1 ? "Active":"Inactive" %>'
                                                 ForeColor='<%# Convert.ToInt32(Eval("ACTIVE_STATUS")) == 1 ?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                            </div>

                           <%-- <div class="tab-pane fade" id="tab_2">
                                 <div class="col-12 mt-3">
                                    <div class="row">
                                    
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Incharge</label>
                                            </div>
                                            <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="form-control" data-select2-enable="true"   TabIndex="5" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvddlIncharge" runat="server" ControlToValidate="ddlIncharge"
                                    Display="None" ErrorMessage="Please Select Incharge" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="querytab2"></asp:RequiredFieldValidator>
                                          </div>
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Team Member</label>
                                            </div>
                                              <asp:ListBox ID="lstbxTeamMember" runat="server" CssClass="form-control multi-select-demo"
                                    AppendDataBoundItems="true" SelectionMode="Multiple" ></asp:ListBox>

                                <asp:RequiredFieldValidator ID="rfvddlTeamMember" runat="server" ControlToValidate="lstbxTeamMember"
                                    Display="None" ErrorMessage="Please Select Team Member " SetFocusOnError="True"
                                    ValidationGroup="querytab2"></asp:RequiredFieldValidator>
                                         </div> 
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Query Category</label>
                                        
                                        </div>
                                            <asp:DropDownList ID="ddlQCategory" runat="server" CssClass="form-control" data-select2-enable="true"    AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvddlstaff" runat="server" ControlToValidate="ddlQCategory"
                                    Display="None" ErrorMessage="Please Select Incharge" SetFocusOnError="True" InitialValue="0"
                                    ValidationGroup="querytab2"></asp:RequiredFieldValidator>
                                          </div>
                                        </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnsubmit2" runat="server" CssClass="btn btn-primary" Text="Submit"  OnClick="btnsubmit2_Click" TabIndex="7" ValidationGroup="querytab2" />
                                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="querytab2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                     ShowSummary ="False" > </asp:ValidationSummary>
                                    <asp:Button ID="btncancel2" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="8" OnClick="btncancel2_Click"/>
                                </div>
                                
                            <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server" Visible="true">
                            <asp:ListView ID="lvUserAllocation" runat="server" Visible="true">
                                <LayoutTemplate>

                                    <table class="table table-striped table-bordered nowrap display">
                                        <thead>
                                            <tr class="bg-light-blue" >

                                                <th>Edit
                                                </th>
                                                <th>Incharge
                                                </th>
                                                <th>Team Member
                                                </th>
                                                <th>Query Category
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
                                            <asp:ImageButton ID="btnEdit" runat="server" OnClick="btnEdit1_Click"  CommandArgument='<%# Eval("QMUSERALLOCATIONID")%>'
                                                ImageUrl="~/Images/edit.png" />
                                        </td>
                                        <td id="participate" runat="server">
                                            <asp:Label runat="server" ID="lblQcategory2" Text='<%#Eval("UA_FULLNAME") %>'></asp:Label>
                                        </td>
                                         <td id="Td1" runat="server">
                                            <asp:Label runat="server" ID="lbluaname" Text='<%#Eval("UNAME") %>'></asp:Label>
                                        </td>
                                         <td id="Td2" runat="server">
                                            <asp:Label runat="server" ID="lbluaname2" Text='<%#Eval("QUERY_CATEGORY") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                            </div>
                             </div>
                            <div class="tab-pane fade" id="tab_3">
                            </div>
                        </div>
                    </div>
                         </div>--%>
                </div>
            </div>
        </div>
    </div>
 <%-- </ContentTemplate>
 </asp:UpdatePanel>--%>
     <script>
         function SetStatActive(val) {
             $('#rdActive').prop('checked', val);
         }

         function validate() {
             // alert('Hii');
             $('#hfdActive').val($('#rdActive').prop('checked'));
             //alert($('#hfdShowStatus').val());
             //if (Page_ClientValidate()) {
             //    alert('Inside');
             //    //$('#hfdShowStatus').val($('#rdActive').prop('checked'));
             //    alert($('#hfdShowStatus').val());
             //}
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

     <script type="text/javascript">
         $(document).ready(function () {
             $('.multi-select-demo').multiselect({
                 includeSelectAllOption: true,
                 maxHeight: 200,
                 enableFiltering: true,
                 filterPlaceholder: 'Search',
             });
         });
         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $(document).ready(function () {
                 $('.multi-select-demo').multiselect({
                     includeSelectAllOption: true,
                     maxHeight: 200,
                     enableFiltering: true,
                     filterPlaceholder: 'Search',
                 });
             });
         });
    </script>
     <script>
         function TabShow(tabName) {
             // var tabName = "tab_2";
             $('#Tabs a[href="#' + tabName + '"]').tab('show');
             $("#Tabs a").click(function () {
                 $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
             });
         }
    </script>
</asp:Content>

