<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseActivityMaster.aspx.cs" Inherits="ACADEMIC_MASTERS_GradeMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGrade"
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
    <asp:UpdatePanel ID="updGrade" runat="server">
        <ContentTemplate>
               <asp:HiddenField ID="hfActivity" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDYCourseActivityM" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>

                                    <div class="form-group col-md-3">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                        <asp:Label ID="lblDYActivityName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtActivityName" runat="server" TabIndex="1" placeholder="Enter Activity Name"
                                            MaxLength="50" ToolTip="Please Enter Activity Name" />
                                        <asp:RequiredFieldValidator ID="rfvActivityName" runat="server" ControlToValidate="txtActivityName"
                                            Display="None" ErrorMessage="Please Enter Activity Name" ValidationGroup="submit"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtActivityName" FilterType="Custom" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ/ '!" />

                                    </div>

                                    <div class="form-group col-md-2">
                                         <div class="label-dynamic">
                                    <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true"></asp:Label>
                                              </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="rdActive" name="switch" checked tabindex="2" />
                                        <label data-on="Active" tabindex="2" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                    </div>
                                    </div>
                                  </div>
                                </div>
                                      <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" 
                                    TabIndex="3" OnClick="btnSave_Click" CssClass="btn btn-primary" OnClientClick="return  validateActivity();" />  
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="4" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <div class="col-12">
                                <table class="table table-striped table-bordered display " style="width: 100%">
                                    <asp:Repeater ID="lvActivityType" runat="server">
                                        <HeaderTemplate>
                                            <div class="sub-heading">
                                                <h5>Activity Name List</h5>
                                            </div>
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>
                                                        Action
                                                    </th>
                                                    <%--<th>
                                                        Activity No
                                                    </th>--%>
                                                    <th>
                                                        Activity Name
                                                    </th>
                                                    <th>
                                                        Status
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CRS_ACT_TYPE_NO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEdit_Click" />
                                                </td>
                                               <%-- <td>
                                                    <%# Eval("GRADE_TYPE")%>
                                                </td>--%>
                                                <td>
                                                    <%# Eval("CRS_ACTIVITY_NAME")%>
                                                </td>
                                                <td>
                                                     <asp:Label ID="lblActive" Text='<%# Eval("ISACTIVE")%>' ForeColor='<%# Eval("ISACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

     <%-- Activity Master--%>
    <script>
        function settimeslot(val) {

            $('#rdActive').prop('checked', val);
            // $('#hftimeslot').val($('#rdActivetimeslot').prop('checked'));
        }

        function validateActivity() { 
            debugger;
            $('#hfActivity').val($('#rdActive').prop('checked'));

            var temp = document.getElementById("<%=txtActivityName.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                alert('Please Enter Activity Name', 'Warning!');
                $('txtActivityName').focus();
                return false;
            }
            

            //var Activityname = $("[id$=txtActivityName]").attr("id");
            //var Activityname = document.getElementById(Activityname);
            //if (Activityname.value == '') {
            //    alert('Please Enter Activity Name', 'Warning!');
            //    $(Activityname).focus();
            //    return false;
            //}
        } 
         
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    // alert("hi");
                    validateActivity();
                });
            });
        });

    </script>
</asp:Content>


