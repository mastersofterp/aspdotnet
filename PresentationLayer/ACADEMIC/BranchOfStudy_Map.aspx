<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchOfStudy_Map.aspx.cs" Inherits="ACADEMIC_BranchOfStudy_Map" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlListView .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBranchStudy"
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
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="updBranchStudy" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select Degree." data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Qualifying Degree"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlQualDegree" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select Qualifying Degree." data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblBranchStudy" runat="server" Font-Bold="true" Text="Study Of Branch"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtBranchStudy" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Enter Branch Of Study." MaxLength="100"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="tbexBranchStudy" runat="server" TargetControlID="txtBranchStudy" InvalidChars="~`!@#$%^&*_-+={[}]|\:;'<,>?|\'" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                        <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYStatus" runat="server" Font-Bold="true" Text="Status"></asp:Label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="switch"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="1" ToolTip="Click To Submit." OnClientClick="return validate();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="1" ToolTip="Click To Cancel." CausesValidation="false" />
                            </div>
                            
                            <div class="col-md-12">
                                <asp:Panel ID="pnlListView" runat="server">
                                    <asp:ListView ID="lvBranchStudy" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Branch Study Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divQualDetails">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">Edit
                                                        </th>
                                                        <th>Degree
                                                        </th>
                                                        <th>
                                                            Qualifying Degree
                                                        </th>
                                                        <th>Branch Of Study
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
                                            <tr class="item">
                                                <td style="text-align: center;">
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit1.gif" OnClick="btnEdit_Click"
                                                        CommandArgument='<%# Eval("STUDY_NO")%>' AlternateText="Edit Record" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <%#Eval("DEGREENAME") %>
                                                </td>
                                                  <td>
                                                    <%#Eval("QUAL_DEGREENAME") %>
                                                </td>
                                                <td>
                                                    <%#Eval("STUDY_OF_BRANCH") %>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>' ForeColor='<%#  Convert.ToInt32(Eval("ACTIVE_STATUS"))==1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
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
        function SetStat(val) {
            $('#switch').prop('checked', val);
        }

        function validate() {
            var degree = document.getElementById('<%=ddlDegree.ClientID%>').value;
                     var branchStudy = document.getElementById('<%=txtBranchStudy.ClientID%>').value;
                     var summary = "";
                     if (degree == "0") {
                         summary += "Please Select Degree.\n";
                     }
                     if (branchStudy == "") {
                         summary += "Please Enter Branch Of Study.\n";
                     }
                     if (summary != "") {
                         alert(summary);
                         summary = "";
                         return false;
                     }
                     $('#hfdStat').val($('#switch').prop('checked'));
                 }

                 var prm = Sys.WebForms.PageRequestManager.getInstance();
                 prm.add_endRequest(function () {
                     $(function () {
                         $('#btnSave').click(function () {
                             //$('#hfdStat').val($('#switch').prop('checked'));
                             validate();
                         });
                     });
                 });
    </script>
</asp:Content>
