<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DistrictStateMapping.aspx.cs" Inherits="ACADEMIC_MASTERS_DistrictStateMapping" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlListView .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDist"
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
    <asp:UpdatePanel ID="updDist" runat="server">
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label  id="lblState" runat="server" Font-Bold="true" Text="State"></asp:Label>
                                        </div>
                                          <asp:DropDownList ID="ddlState" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select State." data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState"
                                            Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select State."></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label  id="lblDistrict" runat="server" Font-Bold="true" Text="District"></asp:Label>
                                        </div>
                                          <asp:TextBox ID="txtDistrict" runat="server" TabIndex="1" CssClass="form-control" ToolTip="Please Select District." MaxLength="50">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDistrict"
                                            Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select District."></asp:RequiredFieldValidator>
                                         <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDistrict" runat="server" TargetControlID="txtDistrict" InvalidChars="~`!@#$%^&*()_-+={[}]:;<,>?/|\'1234567890" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>
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
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" ToolTip="Click To Submit." TabIndex="1" OnClientClick="return validate();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="1" ToolTip="Click to Cancel." CausesValidation="false" />
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlListView" runat="server">
                                    <asp:ListView ID="lvDistrict" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>State District Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divDistrict">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">Edit
                                                        </th>
                                                        <th>State
                                                        </th>
                                                        <th>
                                                            District
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
                                                        CommandArgument='<%# Eval("DISTRICTNO")%>' AlternateText="Edit Record" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <%#Eval("STATE_NAME") %>
                                                </td>
                                                  <td>
                                                    <%#Eval("DISTRICTNAME") %>
                                                </td>                                               
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Active/Inactive") %>' ForeColor='<%#  Convert.ToInt32(Eval("ACTIVESTATUS"))==1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
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
             var State = document.getElementById('<%=ddlState.ClientID%>').value;
            var district = document.getElementById('<%=txtDistrict.ClientID%>').value;
            var summary = "";
            if (State == "0") {
                summary += "Please Select State.\n";
            }
            if (district == "") {
                summary += "Please Enter District.\n";
            }
            if (summary != "") {
                alert(summary);
                summary = "";
                return false;
            }
            $('#hfdStat').val($('#switch').prop('checked'));
        }

        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_endRequest(function () {
        //    $(function () {
        //        $('#btnSave').click(function () {
        //            //$('#hfdStat').val($('#switch').prop('checked'));
        //            validate();
        //        });
        //    });
        //});
    </script>
</asp:Content>
