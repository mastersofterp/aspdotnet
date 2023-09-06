<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Faculty_DisciplineMaster.aspx.cs" Inherits="ACADEMIC_Faculty_DisciplineMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

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
             <script>
                 function ValidationName() {
                     var correct_way = /^[A-Za-z]+$/;

                     var a = document.getElementById('<%=txtFaculty.ClientID%>').value;
                    if (!a.match(correct_way)) {
                        alert("Faculty/Discipline Should be alphabetic");
                        document.getElementById('<%=txtFaculty.ClientID%>').value = '';

                false;
            }
            else {
                true;
            }
        }
    </script>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                            <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Attendance Not Filled By Faculty</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty/Discipline</label>
                                        </div>
                                        <asp:TextBox ID="txtFaculty" runat="server" MaxLength="256" AutoComplete="off" CssClass="form-control" TabIndex="1" ToolTip="Please Enter Faculty/Discipline"/>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                      FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|.:-&&quot;0123456789'" TargetControlID="txtFaculty" />
                                            <%--<asp:RequiredFieldValidator ID="rfvtxtFaculty" runat="server" ControlToValidate="txtFaculty"
                                            Display="None" ErrorMessage="Please Enter Faculty/Discipline" SetFocusOnError="True"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                            </div>
                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                       <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch"  checked />
                                            <label data-on="Active" tabindex="2" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                    </div>
                            </div>
                              <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" 
                                    CssClass="btn btn-primary" ValidationGroup="submit"  TabIndex="3" OnClientClick="return validation();" OnClick="btnSave_Click" />
                             
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" TabIndex="4" OnClick="btnCancel_Click" />
                               
                            </div>
                             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <%--<panel ID="Panellv" runat="server" Visible="false">--%>
                                    <div class="col-12 mt-3">
                                                        <div class="sub-heading">
                                                            <h5>Faculty/Discipline List</h5>
                                                        </div>
                                                        <div class="table-responsive">
                                                            <asp:Panel ID="PanelFaculty" runat="server" Visible="false">
                                                                <asp:ListView ID="lvFacultyMaster" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvFacultyMaster_ItemEditing">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                 <th>Edit</th>
<%--                                                                                 <th>Faculty No.
                                                                                 </th>--%>
                                                                                 <th>Faculty Name
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
                                                            AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("FACULTY_NO")%>' TabIndex="5" OnClick="btnEdit_Click" />
                                                    </td>
<%--                                                    <td>
                                                        <%# Eval("FACULTY_NO")%>
                                                    </td>--%>
                                                    <td>
                                                        <%# Eval("FACULTY_NAME")%>
                                                    </td>
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
                            <%--</panel>--%>
                        </div>

                        </ContentTemplate>
    </asp:UpdatePanel>
        <script>
            function SetSetSubjecttype(val) {

                $('#rdActive').prop('checked', val);
            }
            function validation() {
                var alertMsg = "";
                var facuilty = document.getElementById('<%=txtFaculty.ClientID%>').value;
                if (facuilty == 0) {
                    if (facuilty == "") {
                         alertMsg = alertMsg + 'Please Enter Faculty/Discipline\n';
                     }
                     alert(alertMsg);
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

