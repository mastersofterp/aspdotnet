<%@ Page Language="C#" AutoEventWireup="true" CodeFile="State_District_Mapping.aspx.cs" Inherits="ACADEMIC_MASTERS_State_District_Mapping" MasterPageFile="~/SiteMasterPage.master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function Validate() {
            var stateno = document.getElementById('<%=ddlState.ClientID%>');
            var district = document.getElementById('<%=txtDistrict.ClientID%>');
            var msg = "";
            if (stateno.value == 0 || district.value == "") {
                if (stateno.value == 0) {
                    msg = 'Please select state.\n';
                }
                if (district.value == "") {
                    msg += 'Please select district.';
                }
                alert(msg);
                return false;
            }
            $('#hfdDist').val($('#rdActive').prop('checked'));
        }
    </script>
        <script>
            function SetActive(val) {
                $('#rdActive').prop('checked', val);
            }
               </script> 
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
    <asp:UpdatePanel ID="updDist" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfdDist" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
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
                                            <label>State</label>
                                        </div>
                                        <asp:DropDownList ID="ddlState" runat="server" ToolTip="Please select state." TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" AutoPostBack="true" OnTextChanged="ddlState_TextChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>District</label>
                                        </div>
                                        <asp:TextBox ID="txtDistrict" runat="server" ToolTip="Please enter district." TabIndex="1" CssClass="form-control" MaxLength="100" AutoComplete="off"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="txtFilterDist" runat="server" TargetControlID="txtDistrict" FilterMode="InvalidChars" InvalidChars="~`!@#$%^&*()_-+={[}]:;<,>.?|\'0123654789"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" data-off="Inactive" tabindex="7" class="newAddNew Tab" for="rdActive"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Click to submit." TabIndex="1" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="return Validate(this);" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Click to cancel." TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                </p>
                               
                            </div>
                             <div class="col-12">
                                    <asp:Panel ID="pnlDistrict" runat="server" Visible="false">
                                        <asp:ListView ID="lvDistrict" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>State District Mapping</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th style="text-align: center;">Edit
                                                            </th>
                                                            <th>State
                                                            </th>
                                                            <th>District
                                                            </th>
                                                            <th>Active Status
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
                                                    <td style="text-align: center;">
                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png"
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="1" CommandArgument='<%#Eval("DISTRICTNO")%>' />
                                                    </td>       
                                                    <td>
                                                        <%#Eval("STATENAME")%>
                                                    </td>     
                                                      <td>
                                                        <%#Eval("DISTRICTNAME")%>
                                                    </td>  
                                                      <td>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ACTIVESTATUS")%>' ForeColor='<%#Eval("ACTIVESTATUS").ToString().Equals("Active") ? System.Drawing.Color.Green : System.Drawing.Color.Red%>'></asp:Label>
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
</asp:Content>
